# DevArchitecture Incremental Improvement Design

**Date:** 2026-06-24
**Approach:** Incremental Improvement (Low-risk, backwards-compatible)
**Version Strategy:** Semantic Versioning (MAJOR.MINOR.PATCH)

---

## Executive Summary

This design outlines incremental improvements to the DevArchitecture framework, focusing on backend (API) and frontend (client) enhancements without breaking existing functionality. The improvements include rate limiting, pagination integration, response caching, global error handling, health checks, API client wrapper, form validation, shared components, and responsive design optimization.

---

## Backend Improvements

### 1. Rate Limiting

**Purpose:** Protect API from abuse and ensure fair usage.

**Implementation:**
- Use ASP.NET Core built-in rate limiting (`Microsoft.AspNetCore.RateLimiting`)
- Configure in `WebAPI/Startup.cs`

**Rate Limits:**
| Endpoint Type | Limit | Window |
|---------------|-------|--------|
| Auth (login, register) | 10 requests | 1 minute |
| CRUD operations | 100 requests | 1 minute |
| Read operations | 200 requests | 1 minute |

**Configuration:**
\`\`\`csharp
// WebAPI/Startup.cs
services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    
    options.AddFixedWindowLimiter("auth", limiterOptions =>
    {
        limiterOptions.PermitLimit = 10;
        limiterOptions.Window = TimeSpan.FromMinutes(1);
        limiterOptions.QueueLimit = 0;
    });
    
    options.AddFixedWindowLimiter("crud", limiterOptions =>
    {
        limiterOptions.PermitLimit = 100;
        limiterOptions.Window = TimeSpan.FromMinutes(1);
    });
    
    options.AddFixedWindowLimiter("read", limiterOptions =>
    {
        limiterOptions.PermitLimit = 200;
        limiterOptions.Window = TimeSpan.FromMinutes(1);
    });
});
\`\`\`

**Controller Usage:**
\`\`\`csharp
[HttpPost("login")]
[EnableRateLimiting("auth")]
public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
{
    // ...
}
\`\`\`

### 2. Pagination Integration

**Purpose:** Enable paginated responses for all list queries.

**Current State:**
- `PaginatedResult<T>` exists in `Core/Utilities/Results/PaginatedResult.cs`
- `PaginationHelper` exists in `Business/Helpers/PaginationHelper.cs`
- But not integrated into queries

**Implementation:**

**Base Query Class:**
\`\`\`csharp
// Core/Entities/BasePaginatedQuery.cs
public abstract class BasePaginatedQuery<T> : IRequest<IDataResult<PaginatedResult<IEnumerable<T>>>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    
    protected BasePaginatedQuery(int pageNumber = 1, int pageSize = 10)
    {
        PageNumber = pageNumber <= 0 ? 1 : pageNumber;
        PageSize = pageSize <= 0 ? 10 : Math.Min(pageSize, 100); // Max 100
    }
}
\`\`\`

**Updated GetUsersQuery:**
\`\`\`csharp
public class GetUsersQuery : BasePaginatedQuery<UserDto>
{
    public GetUsersQuery(int pageNumber = 1, int pageSize = 10) 
        : base(pageNumber, pageSize) { }
    
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IDataResult<PaginatedResult<IEnumerable<UserDto>>>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [SecuredOperation(Priority = 1)]
        [PerformanceAspect(5)]
        [CacheAspect(10)]
        [LogAspect(typeof(FileLogger))]
        public async Task<IDataResult<PaginatedResult<IEnumerable<UserDto>>> Handle(
            GetUsersQuery request, CancellationToken cancellationToken)
        {
            var userList = await _userRepository.GetListAsync();
            var totalCount = userList.Count();
            
            var paginatedUsers = userList
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(user => _mapper.Map<UserDto>(user))
                .ToList();

            var result = new PaginatedResult<IEnumerable<UserDto>>(
                paginatedUsers, 
                request.PageNumber, 
                request.PageSize);
            
            result.TotalRecords = totalCount;
            result.TotalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize);

            return new SuccessDataResult<PaginatedResult<IEnumerable<UserDto>>>(result);
        }
    }
}
\`\`\`

### 3. Response Caching

**Purpose:** Improve API performance by caching frequent responses.

**Implementation:**
\`\`\`csharp
// WebAPI/Startup.cs
services.AddResponseCaching(options =>
{
    options.MaximumBodySize = 1024 * 1024; // 1 MB
    options.SizeLimit = 100 * 1024 * 1024; // 100 MB
});

// Configure in Configure method
app.UseResponseCaching();
\`\`\`

**Controller Usage:**
\`\`\`csharp
[HttpGet("languages")]
[ResponseCache(Duration = 60)] // Cache for 1 minute
public async Task<IActionResult> GetLanguages()
{
    // ...
}
\`\`\`

### 4. Global Exception Handling

**Purpose:** Standardize error responses across all endpoints.

**Implementation:**
\`\`\`csharp
// Core/CrossCuttingConcerns/Exceptions/GlobalExceptionMiddleware.cs
public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var (statusCode, message) = exception switch
        {
            UnauthorizedAccessException => (StatusCodes.Status401Unauthorized, "Unauthorized"),
            ArgumentException => (StatusCodes.Status400BadRequest, exception.Message),
            KeyNotFoundException => (StatusCodes.Status404NotFound, "Resource not found"),
            InvalidOperationException => (StatusCodes.Status409Conflict, exception.Message),
            _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred")
        };

        context.Response.StatusCode = statusCode;

        var response = new ErrorResponse
        {
            StatusCode = statusCode,
            Message = message,
            Timestamp = DateTime.UtcNow
        };

        await context.Response.WriteAsJsonAsync(response);
    }
}
\`\`\`

### 5. Health Checks

**Purpose:** Monitor application health and dependencies.

**Implementation:**
\`\`\`csharp
// WebAPI/Startup.cs
services.AddHealthChecks()
    .AddSqlServer(
        Configuration.GetConnectionString("DefaultConnection"),
        name: "sqlserver",
        timeout: TimeSpan.FromSeconds(5),
        tags: new[] { "db", "sql", "ready" })
    .AddRedis(
        Configuration.GetConnectionString("RedisConnection"),
        name: "redis",
        timeout: TimeSpan.FromSeconds(5),
        tags: new[] { "cache", "redis", "ready" })
    .AddHangfire(
        options => options.MaximumJobsFailed = 5,
        name: "hangfire",
        timeout: TimeSpan.FromSeconds(5),
        tags: new[] { "scheduler", "hangfire", "ready" })
    .AddCheck("self", () => HealthCheckResult.Healthy());

// Configure in Configure method
app.MapHealthChecks("/healthz", new HealthCheckOptions
{
    ResponseWriter = async (context, report) =>
    {
        context.Response.ContentType = "application/json";
        var response = new
        {
            status = report.Status.ToString(),
            checks = report.Entries.Select(e => new
            {
                name = e.Key,
                status = e.Status.ToString(),
                duration = e.Duration.TotalMilliseconds,
                description = e.Description,
                exception = e.Exception?.Message
            }),
            totalDuration = report.TotalDuration.TotalMilliseconds
        };
        await context.Response.WriteAsJsonAsync(response);
    }
});
\`\`\`

**Endpoints:**
- `/healthz` - Liveness check (is app running?)
- `/health` - Readiness check (is app ready to serve?)

---

## Frontend Improvements

### 1. API Client Wrapper

**Purpose:** Centralize API communication and handle common concerns.

**Implementation (Vue):**
\`\`\`typescript
// clients/vue-admin/src/api/client.ts
import axios, { AxiosInstance, AxiosError } from 'axios';
import { useAuthStore } from '@/stores/auth';
import { useNotificationStore } from '@/stores/notification';

const apiClient: AxiosInstance = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL || 'https://localhost:5101/api/v1',
  timeout: 10000,
  headers: {
    'Content-Type': 'application/json',
  },
});

// Request interceptor
apiClient.interceptors.request.use(
  (config) => {
    const authStore = useAuthStore();
    if (authStore.token) {
      config.headers.Authorization = \`Bearer \${authStore.token}\`;
    }
    return config;
  },
  (error) => Promise.reject(error)
);

// Response interceptor
apiClient.interceptors.response.use(
  (response) => response,
  async (error: AxiosError) => {
    const authStore = useAuthStore();
    const notificationStore = useNotificationStore();

    if (error.response?.status === 401) {
      authStore.logout();
      window.location.href = '/login';
    } else if (error.response?.status === 403) {
      notificationStore.showError('You do not have permission to perform this action');
    } else if (error.response?.status === 429) {
      notificationStore.showError('Too many requests. Please try again later.');
    } else if (error.response?.status >= 500) {
      notificationStore.showError('Server error. Please try again later.');
    }

    return Promise.reject(error);
  }
);

export default apiClient;
\`\`\`

### 2. Error Handling Pattern

**Purpose:** Consistent error display across all clients.

**Implementation (Vue):**
\`\`\`typescript
// clients/vue-admin/src/composables/useErrorHandler.ts
import { useNotificationStore } from '@/stores/notification';
import { AxiosError } from 'axios';

export function useErrorHandler() {
  const notificationStore = useNotificationStore();

  const handleError = (error: any, context?: string) => {
    const message = getErrorMessage(error);
    const title = context || 'Error';
    
    notificationStore.showError(message, title);
    
    if (import.meta.env.DEV) {
      console.error(\`[\${context}]\`, error);
    }
  };

  const getErrorMessage = (error: any): string => {
    if (error.response?.data?.message) {
      return error.response.data.message;
    }
    
    if (error.message) {
      return error.message;
    }
    
    return 'An unexpected error occurred';
  };

  return { handleError };
}
\`\`\`

### 3. Form Validation

**Purpose:** Standardize form validation across all forms.

**Implementation (Vue):**
\`\`\`typescript
// clients/vue-admin/src/composables/useFormValidation.ts
import { ref, computed } from 'vue';

export function useFormValidation() {
  const errors = ref<Record<string, string>>({});

  const validateEmail = (email: string): boolean => {
    const re = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!re.test(email)) {
      errors.value.email = 'Invalid email address';
      return false;
    }
    delete errors.value.email;
    return true;
  };

  const validateRequired = (value: any, fieldName: string): boolean => {
    if (!value || (typeof value === 'string' && !value.trim())) {
      errors.value[fieldName] = \`\${fieldName} is required\`;
      return false;
    }
    delete errors.value[fieldName];
    return true;
  };

  const validateMinLength = (value: string, minLength: number, fieldName: string): boolean => {
    if (value.length < minLength) {
      errors.value[fieldName] = \`\${fieldName} must be at least \${minLength} characters\`;
      return false;
    }
    delete errors.value[fieldName];
    return true;
  };

  const isValid = computed(() => Object.keys(errors.value).length === 0);

  const resetErrors = () => {
    errors.value = {};
  };

  return {
    errors,
    isValid,
    validateEmail,
    validateRequired,
    validateMinLength,
    resetErrors
  };
}
\`\`\`

### 4. Shared Component Library

**Purpose:** Reusable UI components for consistency.

**Components to Create:**

\`\`\`
clients/vue-admin/src/components/shared/
├── DataTable.vue           # Generic data table with sorting, pagination
├── FormField.vue           # Form field wrapper with validation
├── ConfirmationDialog.vue  # Confirmation modal
├── LoadingSpinner.vue      # Loading indicator
├── EmptyState.vue          # Empty state display
├── ErrorBoundary.vue       # Error boundary wrapper
└── index.ts                # Export all components
\`\`\`

### 5. Responsive Design

**Purpose:** Optimize UI for all screen sizes.

**Breakpoints:**
\`\`\`css
:root {
  --breakpoint-mobile: 768px;
  --breakpoint-tablet: 1024px;
  --breakpoint-desktop: 1280px;
}
\`\`\`

**Sidebar Behavior:**
- Desktop: Fixed sidebar (250px)
- Tablet: Collapsible sidebar (60px icons only)
- Mobile: Hidden sidebar (toggle with hamburger menu)

---

## Versioning Strategy

### Semantic Versioning

Format: \`MAJOR.MINOR.PATCH\`

| Change Type | Version Bump | Example |
|-------------|--------------|---------|
| Breaking changes | MAJOR | 1.0.0 → 2.0.0 |
| New features | MINOR | 1.0.0 → 1.1.0 |
| Bug fixes | PATCH | 1.0.0 → 1.0.1 |

**Commit Message Convention:**
- \`feat:\` → minor bump
- \`fix:\` → patch bump
- \`feat!:\` or \`BREAKING CHANGE:\` footer → major bump

---

## Changelog Generation

### git-cliff Configuration

Mevcut \`cliff.toml\` yapılandırması kullanılacak.

**Commands:**
\`\`\`bash
# Generate CHANGELOG.md
git cliff -o CHANGELOG.md

# Generate for specific tag range
git cliff -o CHANGELOG.md v1.0.0..v1.1.0
\`\`\`

---

## Implementation Order

### Phase 1: Backend Foundation (Week 1-2)
1. Rate limiting
2. Global exception handling
3. Health checks

### Phase 2: Backend Features (Week 3-4)
4. Pagination integration
5. Response caching

### Phase 3: Frontend Foundation (Week 5-6)
6. API client wrapper
7. Error handling pattern
8. Form validation

### Phase 4: Frontend Features (Week 7-8)
9. Shared component library
10. Responsive design optimization

### Phase 5: Versioning & Release (Week 9)
11. Semantic versioning setup
12. Changelog automation
13. GitHub release workflow

---

## Success Metrics

- API response time improved by 20% (with caching)
- Error rate reduced by 50% (with global error handling)
- Frontend bundle size reduced by 15% (with shared components)
- User satisfaction improved (with responsive design)
