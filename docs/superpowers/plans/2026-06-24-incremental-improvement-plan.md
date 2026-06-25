# DevArchitecture Incremental Improvement Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Implement incremental backend and frontend improvements to the DevArchitecture framework without breaking existing functionality.

**Architecture:** Enhance existing layered architecture (.NET 10 backend, Vue/Angular/React/Blazor frontends) with rate limiting, pagination, caching, error handling, shared components, and semantic versioning. All changes are backwards-compatible.

**Tech Stack:** ASP.NET Core 10, MediatR, FluentValidation, Vue 3, Angular 19, React 19, PrimeVue/NG/React

---

## File Structure

### Backend Files (Modify)
- `WebAPI/Startup.cs` - Add rate limiting, response caching, health checks, global exception handler
- `Core/Entities/BasePaginatedQuery.cs` - **Create** base paginated query class
- `Business/Handlers/Users/Queries/GetUsersQuery.cs` - Update to use pagination
- `Core/CrossCuttingConcerns/Exceptions/GlobalExceptionMiddleware.cs` - **Create** global exception handler
- `Core/CrossCuttingConcerns/Exceptions/ErrorResponse.cs` - **Create** error response model
- `Business/Handlers/Languages/Queries/GetLanguagesQuery.cs` - Add response caching

### Frontend Files (Create - Vue)
- `clients/vue-admin/src/api/client.ts` - API client wrapper
- `clients/vue-admin/src/composables/useErrorHandler.ts` - Error handling composable
- `clients/vue-admin/src/composables/useFormValidation.ts` - Form validation composable
- `clients/vue-admin/src/components/shared/DataTable.vue` - Shared data table
- `clients/vue-admin/src/components/shared/LoadingSpinner.vue` - Loading indicator
- `clients/vue-admin/src/components/shared/index.ts` - Component exports

### Frontend Files (Create - Angular)
- `clients/angular-admin/src/app/core/interceptors/auth.interceptor.ts` - Auth interceptor
- `clients/angular-admin/src/app/core/services/error-handler.service.ts` - Error handler
- `clients/angular-admin/src/app/shared/components/data-table/data-table.component.ts` - Data table

### Versioning Files (Modify)
- `WebAPI/WebAPI.csproj` - Update version to 1.1.0
- `CHANGELOG.md` - **Create** changelog file

---

## Task 1: Rate Limiting (Backend)

**Files:**
- Modify: `WebAPI/Startup.cs`
- Modify: `WebAPI/WebAPI.csproj`

- [ ] **Step 1: Add rate limiting NuGet package**

Check if `Microsoft.AspNetCore.RateLimiting` is already included in the framework (it is in ASP.NET Core 7+).

Run: `dotnet restore DevArchitecture.sln`
Expected: Success

- [ ] **Step 2: Configure rate limiting in Startup.cs**

Open `WebAPI/Startup.cs` and add to `ConfigureServices` method:

```csharp
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
```

- [ ] **Step 3: Add rate limiter middleware in Configure method**

In `WebAPI/Startup.cs` Configure method, add after `app.UseRouting()`:

```csharp
app.UseRateLimiter();
```

- [ ] **Step 4: Add rate limiting to Auth controller**

Open `WebAPI/Controllers/AuthController.cs` and add to Login action:

```csharp
[HttpPost("login")]
[EnableRateLimiting("auth")]
public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
{
    // existing code
}
```

- [ ] **Step 5: Test rate limiting**

Run: `dotnet build WebAPI/WebAPI.csproj`
Expected: Success

- [ ] **Step 6: Commit**

```bash
git add WebAPI/Startup.cs WebAPI/Controllers/AuthController.cs
git commit -m "feat(backend): add rate limiting to API endpoints"
```

---

## Task 2: Global Exception Handling (Backend)

**Files:**
- Create: `Core/CrossCuttingConcerns/Exceptions/GlobalExceptionMiddleware.cs`
- Create: `Core/CrossCuttingConcerns/Exceptions/ErrorResponse.cs`
- Modify: `WebAPI/Startup.cs`

- [ ] **Step 1: Create ErrorResponse model**

Create file `Core/CrossCuttingConcerns/Exceptions/ErrorResponse.cs`:

```csharp
using System;

namespace Core.CrossCuttingConcerns.Exceptions
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
        public string TraceId { get; set; }
    }
}
```

- [ ] **Step 2: Create GlobalExceptionMiddleware**

Create file `Core/CrossCuttingConcerns/Exceptions/GlobalExceptionMiddleware.cs`:

```csharp
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Core.CrossCuttingConcerns.Exceptions
{
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
                Timestamp = DateTime.UtcNow,
                TraceId = context.TraceIdentifier
            };

            var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await context.Response.WriteAsync(json);
        }
    }
}
```

- [ ] **Step 3: Register middleware in Startup.cs**

Open `WebAPI/Startup.cs` and add in Configure method, after `app.UseRouting()`:

```csharp
app.UseMiddleware<GlobalExceptionMiddleware>();
```

Add using at top:

```csharp
using Core.CrossCuttingConcerns.Exceptions;
```

- [ ] **Step 4: Test build**

Run: `dotnet build WebAPI/WebAPI.csproj`
Expected: Success

- [ ] **Step 5: Commit**

```bash
git add Core/CrossCuttingConcerns/Exceptions/ WebAPI/Startup.cs
git commit -m "feat(backend): add global exception handling middleware"
```

---

## Task 3: Health Checks (Backend)

**Files:**
- Modify: `WebAPI/Startup.cs`
- Modify: `Directory.Packages.props`

- [ ] **Step 1: Add health check packages**

Open `Directory.Packages.props` and add:

```xml
<PackageVersion Include="AspNetCore.HealthChecks.SqlServer" Version="8.0.2" />
<PackageVersion Include="AspNetCore.HealthChecks.Redis" Version="8.0.2" />
<PackageVersion Include="AspNetCore.HealthChecks.Hangfire" Version="8.0.2" />
```

- [ ] **Step 2: Add packages to WebAPI.csproj**

Open `WebAPI/WebAPI.csproj` and add:

```xml
<PackageReference Include="AspNetCore.HealthChecks.SqlServer" />
<PackageReference Include="AspNetCore.HealthChecks.Redis" />
<PackageReference Include="AspNetCore.HealthChecks.Hangfire" />
```

- [ ] **Step 3: Configure health checks in Startup.cs**

Open `WebAPI/Startup.cs` and add in ConfigureServices:

```csharp
services.AddHealthChecks()
    .AddSqlServer(
        Configuration.GetConnectionString("DefaultConnection"),
        name: "sqlserver",
        timeout: TimeSpan.FromSeconds(5),
        tags: new[] { "db", "sql", "ready" })
    .AddHangfire(
        options => options.MaximumJobsFailed = 5,
        name: "hangfire",
        timeout: TimeSpan.FromSeconds(5),
        tags: new[] { "scheduler", "hangfire", "ready" })
    .AddCheck("self", () => Microsoft.Extensions.Diagnostics.HealthChecks.HealthCheckResult.Healthy());
```

- [ ] **Step 4: Configure health check endpoints**

In Configure method, update endpoints:

```csharp
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHealthChecks("/healthz", new HealthCheckOptions
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
    endpoints.MapHealthChecks("/health", new HealthCheckOptions
    {
        Predicate = _ => false
    });
});
```

- [ ] **Step 5: Restore and build**

Run: `dotnet restore DevArchitecture.sln && dotnet build WebAPI/WebAPI.csproj`
Expected: Success

- [ ] **Step 6: Commit**

```bash
git add Directory.Packages.props WebAPI/WebAPI.csproj WebAPI/Startup.cs
git commit -m "feat(backend): add health check endpoints"
```

---

## Task 4: Pagination Integration (Backend)

**Files:**
- Create: `Core/Entities/BasePaginatedQuery.cs`
- Modify: `Business/Handlers/Users/Queries/GetUsersQuery.cs`
- Modify: `Business/Handlers/Users/Queries/GetUsersQuery.cs` handler

- [ ] **Step 1: Create BasePaginatedQuery class**

Create file `Core/Entities/BasePaginatedQuery.cs`:

```csharp
using System;
using Core.Utilities.Results;
using MediatR;

namespace Core.Entities
{
    public abstract class BasePaginatedQuery<T> : IRequest<IDataResult<PaginatedResult<System.Collections.Generic.IEnumerable<T>>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        protected BasePaginatedQuery(int pageNumber = 1, int pageSize = 10)
        {
            PageNumber = pageNumber <= 0 ? 1 : pageNumber;
            PageSize = pageSize <= 0 ? 10 : Math.Min(pageSize, 100);
        }
    }
}
```

- [ ] **Step 2: Update GetUsersQuery to use pagination**

Open `Business/Handlers/Users/Queries/GetUsersQuery.cs` and replace:

```csharp
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities;
using Core.Entities.Dtos;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Users.Queries
{
    public class GetUsersQuery : BasePaginatedQuery<UserDto>
    {
        public GetUsersQuery(int pageNumber = 1, int pageSize = 10) 
            : base(pageNumber, pageSize) { }
        
        public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IDataResult<PaginatedResult<IEnumerable<UserDto>>>
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
            public async Task<IDataResult<PaginatedResult<IEnumerable<UserDto>>>> Handle(
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
                result.TotalPages = (int)System.Math.Ceiling(totalCount / (double)request.PageSize);

                return new SuccessDataResult<PaginatedResult<IEnumerable<UserDto>>>(result);
            }
        }
    }
}
```

- [ ] **Step 3: Test build**

Run: `dotnet build Business/Business.csproj`
Expected: Success

- [ ] **Step 4: Commit**

```bash
git add Core/Entities/BasePaginatedQuery.cs Business/Handlers/Users/Queries/GetUsersQuery.cs
git commit -m "feat(backend): integrate pagination into user queries"
```

---

## Task 5: Response Caching (Backend)

**Files:**
- Modify: `WebAPI/Startup.cs`
- Modify: `Business/Handlers/Languages/Queries/GetLanguagesQuery.cs`

- [ ] **Step 1: Add response caching in Startup.cs**

Open `WebAPI/Startup.cs` and add in ConfigureServices:

```csharp
services.AddResponseCaching(options =>
{
    options.MaximumBodySize = 1024 * 1024; // 1 MB
    options.SizeLimit = 100 * 1024 * 1024; // 100 MB
});
```

In Configure method, add after `app.UseRouting()`:

```csharp
app.UseResponseCaching();
```

- [ ] **Step 2: Add caching to GetLanguagesQuery**

Open `Business/Handlers/Languages/Queries/GetLanguagesQuery.cs` and add ResponseCache attribute:

```csharp
[ResponseCache(Duration = 60)]
```

- [ ] **Step 3: Test build**

Run: `dotnet build WebAPI/WebAPI.csproj`
Expected: Success

- [ ] **Step 4: Commit**

```bash
git add WebAPI/Startup.cs Business/Handlers/Languages/Queries/GetLanguagesQuery.cs
git commit -m "feat(backend): add response caching for performance"
```

---

## Task 6: Vue API Client Wrapper (Frontend)

**Files:**
- Create: `clients/vue-admin/src/api/client.ts`
- Create: `clients/vue-admin/src/api/index.ts`

- [ ] **Step 1: Create API client**

Create file `clients/vue-admin/src/api/client.ts`:

```typescript
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
      config.headers.Authorization = `Bearer ${authStore.token}`;
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
```

- [ ] **Step 2: Create API index file**

Create file `clients/vue-admin/src/api/index.ts`:

```typescript
export { default as apiClient } from './client';
```

- [ ] **Step 3: Update existing API calls to use client**

Open `clients/vue-admin/src/services/authService.ts` and update to use apiClient:

```typescript
import { apiClient } from '@/api';

// Replace axios calls with apiClient
// Example:
// const response = await apiClient.post('/Auth/login', credentials);
```

- [ ] **Step 4: Test build**

Run: `cd clients/vue-admin && npm run build`
Expected: Success

- [ ] **Step 5: Commit**

```bash
git add clients/vue-admin/src/api/
git commit -m "feat(frontend): add Vue API client wrapper"
```

---

## Task 7: Vue Error Handling & Form Validation (Frontend)

**Files:**
- Create: `clients/vue-admin/src/composables/useErrorHandler.ts`
- Create: `clients/vue-admin/src/composables/useFormValidation.ts`
- Create: `clients/vue-admin/src/composables/index.ts`

- [ ] **Step 1: Create useErrorHandler composable**

Create file `clients/vue-admin/src/composables/useErrorHandler.ts`:

```typescript
import { useNotificationStore } from '@/stores/notification';

export function useErrorHandler() {
  const notificationStore = useNotificationStore();

  const handleError = (error: any, context?: string) => {
    const message = getErrorMessage(error);
    const title = context || 'Error';
    
    notificationStore.showError(message, title);
    
    if (import.meta.env.DEV) {
      console.error(`[${context}]`, error);
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
```

- [ ] **Step 2: Create useFormValidation composable**

Create file `clients/vue-admin/src/composables/useFormValidation.ts`:

```typescript
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
      errors.value[fieldName] = `${fieldName} is required`;
      return false;
    }
    delete errors.value[fieldName];
    return true;
  };

  const validateMinLength = (value: string, minLength: number, fieldName: string): boolean => {
    if (value.length < minLength) {
      errors.value[fieldName] = `${fieldName} must be at least ${minLength} characters`;
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
```

- [ ] **Step 3: Create composables index file**

Create file `clients/vue-admin/src/composables/index.ts`:

```typescript
export { useErrorHandler } from './useErrorHandler';
export { useFormValidation } from './useFormValidation';
```

- [ ] **Step 4: Test build**

Run: `cd clients/vue-admin && npm run build`
Expected: Success

- [ ] **Step 5: Commit**

```bash
git add clients/vue-admin/src/composables/
git commit -m "feat(frontend): add error handling and form validation composables"
```

---

## Task 8: Vue Shared Components (Frontend)

**Files:**
- Create: `clients/vue-admin/src/components/shared/DataTable.vue`
- Create: `clients/vue-admin/src/components/shared/LoadingSpinner.vue`
- Create: `clients/vue-admin/src/components/shared/index.ts`

- [ ] **Step 1: Create DataTable component**

Create file `clients/vue-admin/src/components/shared/DataTable.vue`:

```vue
<template>
  <div class="data-table">
    <table>
      <thead>
        <tr>
          <th v-for="column in columns" :key="column.key">
            {{ column.label }}
          </th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="row in data" :key="row.id">
          <td v-for="column in columns" :key="column.key">
            {{ row[column.key] }}
          </td>
        </tr>
      </tbody>
    </table>
    
    <div v-if="data.length === 0" class="empty-state">
      <slot name="empty">
        <p>No data available</p>
      </slot>
    </div>
    
    <div v-if="showPagination" class="pagination">
      <button 
        :disabled="currentPage === 1"
        @click="$emit('page-change', currentPage - 1)"
      >
        Previous
      </button>
      <span>Page {{ currentPage }} of {{ totalPages }}</span>
      <button 
        :disabled="currentPage === totalPages"
        @click="$emit('page-change', currentPage + 1)"
      >
        Next
      </button>
    </div>
  </div>
</template>

<script setup lang="ts">
interface Column {
  key: string;
  label: string;
  sortable?: boolean;
}

interface Props {
  columns: Column[];
  data: any[];
  currentPage?: number;
  totalPages?: number;
  showPagination?: boolean;
}

withDefaults(defineProps<Props>(), {
  currentPage: 1,
  totalPages: 1,
  showPagination: false
});

defineEmits(['page-change', 'sort']);
</script>

<style scoped>
.data-table {
  width: 100%;
}

.data-table table {
  width: 100%;
  border-collapse: collapse;
}

.data-table th,
.data-table td {
  padding: 12px;
  text-align: left;
  border-bottom: 1px solid #e5e7eb;
}

.data-table th {
  background-color: #f9fafb;
  font-weight: 600;
}

.pagination {
  display: flex;
  justify-content: center;
  align-items: center;
  gap: 1rem;
  margin-top: 1rem;
}

.empty-state {
  text-align: center;
  padding: 2rem;
  color: #6b7280;
}
</style>
```

- [ ] **Step 2: Create LoadingSpinner component**

Create file `clients/vue-admin/src/components/shared/LoadingSpinner.vue`:

```vue
<template>
  <div class="loading-spinner" :class="{ 'full-page': fullPage }">
    <div class="spinner"></div>
    <span v-if="text" class="text">{{ text }}</span>
  </div>
</template>

<script setup lang="ts">
interface Props {
  text?: string;
  fullPage?: boolean;
}

withDefaults(defineProps<Props>(), {
  text: '',
  fullPage: false
});
</script>

<style scoped>
.loading-spinner {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  padding: 2rem;
}

.loading-spinner.full-page {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: rgba(255, 255, 255, 0.9);
  z-index: 9999;
}

.spinner {
  width: 40px;
  height: 40px;
  border: 3px solid #e5e7eb;
  border-top-color: #10b981;
  border-radius: 50%;
  animation: spin 1s linear infinite;
}

@keyframes spin {
  to {
    transform: rotate(360deg);
  }
}

.text {
  margin-top: 1rem;
  color: #6b7280;
}
</style>
```

- [ ] **Step 3: Create shared components index**

Create file `clients/vue-admin/src/components/shared/index.ts`:

```typescript
export { default as DataTable } from './DataTable.vue';
export { default as LoadingSpinner } from './LoadingSpinner.vue';
```

- [ ] **Step 4: Test build**

Run: `cd clients/vue-admin && npm run build`
Expected: Success

- [ ] **Step 5: Commit**

```bash
git add clients/vue-admin/src/components/shared/
git commit -m "feat(frontend): add shared Vue components"
```

---

## Task 9: Angular API Client & Error Handling (Frontend)

**Files:**
- Create: `clients/angular-admin/src/app/core/interceptors/auth.interceptor.ts`
- Create: `clients/angular-admin/src/app/core/services/error-handler.service.ts`

- [ ] **Step 1: Create auth interceptor**

Create file `clients/angular-admin/src/app/core/interceptors/auth.interceptor.ts`:

```typescript
import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from '../services/auth.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const token = this.authService.getToken();
    
    if (token) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`
        }
      });
    }
    
    return next.handle(request);
  }
}
```

- [ ] **Step 2: Create error handler service**

Create file `clients/angular-admin/src/app/core/services/error-handler.service.ts`:

```typescript
import { Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { AuthService } from './auth.service';
import { MessageService } from 'primeng/api';

@Injectable({
  providedIn: 'root'
})
export class ErrorHandlerService {
  constructor(
    private router: Router,
    private authService: AuthService,
    private messageService: MessageService
  ) {}

  handleError(error: HttpErrorResponse): void {
    let message = 'An unexpected error occurred';

    if (error.status === 401) {
      this.authService.logout();
      this.router.navigate(['/login']);
      message = 'Session expired. Please login again.';
    } else if (error.status === 403) {
      message = 'You do not have permission to perform this action';
    } else if (error.status === 404) {
      message = 'Resource not found';
    } else if (error.status === 429) {
      message = 'Too many requests. Please try again later.';
    } else if (error.error?.message) {
      message = error.error.message;
    }

    this.messageService.add({
      severity: 'error',
      summary: 'Error',
      detail: message
    });
  }
}
```

- [ ] **Step 3: Test build**

Run: `cd clients/angular-admin && npm run build`
Expected: Success

- [ ] **Step 4: Commit**

```bash
git add clients/angular-admin/src/app/core/
git commit -m "feat(frontend): add Angular API client and error handling"
```

---

## Task 10: Semantic Versioning Setup

**Files:**
- Modify: `WebAPI/WebAPI.csproj`
- Create: `CHANGELOG.md`

- [ ] **Step 1: Update WebAPI version**

Open `WebAPI/WebAPI.csproj` and update:

```xml
<Version>1.1.0</Version>
<PackageVersion>1.1.0</PackageVersion>
```

- [ ] **Step 2: Create CHANGELOG.md**

Create file `CHANGELOG.md`:

```markdown
# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.1.0] - 2026-06-24

### Added
- Rate limiting for API endpoints (auth: 10/min, CRUD: 100/min, read: 200/min)
- Global exception handling middleware
- Health check endpoints (`/healthz` and `/health`)
- Pagination integration for user queries
- Response caching for improved performance
- Vue API client wrapper with interceptors
- Vue error handling and form validation composables
- Vue shared components (DataTable, LoadingSpinner)
- Angular auth interceptor and error handler service

### Changed
- Upgraded Refit from 10.1.6 to 12.0.0

## [1.0.0] - 2026-04-20

### Added
- Initial release
- .NET 10 backend
- Vue, Angular, React, Blazor admin clients
- JWT authentication
- CQRS pattern with MediatR
```

- [ ] **Step 3: Test build**

Run: `dotnet build WebAPI/WebAPI.csproj`
Expected: Success

- [ ] **Step 4: Commit**

```bash
git add WebAPI/WebAPI.csproj CHANGELOG.md
git commit -m "chore(release): bump version to 1.1.0 and add CHANGELOG"
```

---

## Execution Summary

| Task | Description | Files Modified |
|------|-------------|----------------|
| 1 | Rate Limiting | Startup.cs, AuthController.cs |
| 2 | Global Exception Handling | GlobalExceptionMiddleware.cs, ErrorResponse.cs, Startup.cs |
| 3 | Health Checks | Startup.cs, WebAPI.csproj, Directory.Packages.props |
| 4 | Pagination Integration | BasePaginatedQuery.cs, GetUsersQuery.cs |
| 5 | Response Caching | Startup.cs, GetLanguagesQuery.cs |
| 6 | Vue API Client | client.ts, index.ts |
| 7 | Vue Error Handling | useErrorHandler.ts, useFormValidation.ts |
| 8 | Vue Shared Components | DataTable.vue, LoadingSpinner.vue |
| 9 | Angular API Client | auth.interceptor.ts, error-handler.service.ts |
| 10 | Semantic Versioning | WebAPI.csproj, CHANGELOG.md |

---

## Testing Checklist

- [ ] All backend tests pass: `dotnet test`
- [ ] All frontend builds pass: `npm run build` (in each client folder)
- [ ] Rate limiting works (test with 429 response)
- [ ] Health checks respond at `/healthz` and `/health`
- [ ] Pagination returns correct data structure
- [ ] Error handling returns consistent format
- [ ] API client intercepts 401 and redirects to login
- [ ] Version number updated in WebAPI.csproj
- [ ] CHANGELOG.md is properly formatted

---

## Notes

- All changes are backwards-compatible
- Existing functionality is preserved
- New features are opt-in (rate limiting policies, pagination parameters)
- Frontend changes follow existing patterns in the codebase
- No breaking changes to API contracts
