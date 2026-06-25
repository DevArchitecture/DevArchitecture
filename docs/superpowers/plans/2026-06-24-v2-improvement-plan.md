# DevArchitecture v2.0 Improvement Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development or superpowers:executing-plans to implement this plan task-by-task.

**Goal:** Fix security vulnerabilities, add comprehensive test coverage, improve React client, and enhance API documentation.

**Architecture:** Backend (.NET 10) improvements to package versions and tests, frontend (Vue/Angular/React) test coverage and API client consistency.

**Tech Stack:** NUnit, Moq, FluentAssertions, Vitest, Jest, React, Swashbuckle

---

## File Structure

### Security Fixes
- Modify: `Directory.Packages.props` - Update HealthChecks package versions

### Backend Tests (Create)
- `Tests/Business/Handlers/RateLimitTests.cs`
- `Tests/Business/Handlers/ExceptionHandlingTests.cs`
- `Tests/Business/Handlers/HealthCheckTests.cs`

### Frontend Tests (Create)
- `clients/vue-admin/src/api/__tests__/client.test.ts`
- `clients/vue-admin/src/composables/__tests__/useErrorHandler.test.ts`
- `clients/vue-admin/src/composables/__tests__/useFormValidation.test.ts`
- `clients/angular-admin/src/app/core/services/__tests__/error-handler.service.spec.ts`

### React Client (Create/Modify)
- `clients/react-admin/src/api/client.ts` - Modify
- `clients/react-admin/src/hooks/useErrorHandler.ts` - Create
- `clients/react-admin/src/hooks/index.ts` - Create

### Swagger (Modify)
- `WebAPI/Startup.cs` - Enhance Swagger configuration

---

## Task 1: Fix Package Versions and Security Vulnerabilities

**Files:**
- Modify: `Directory.Packages.props`

- [ ] **Step 1: Update HealthChecks and vulnerability package versions**

In `Directory.Packages.props`, update/add:
```xml
<PackageVersion Include="AspNetCore.HealthChecks.Hangfire" Version="9.0.0" />
<PackageVersion Include="AspNetCore.HealthChecks.Redis" Version="9.0.0" />
<PackageVersion Include="SharpCompress" Version="0.36.0" />
<PackageVersion Include="Snappier" Version="1.1.6" />
```

- [ ] **Step 2: Restore and verify**

Run: `dotnet restore DevArchitecture.sln && dotnet build DevArchitecture.sln`
Expected: Success, fewer vulnerability warnings

- [ ] **Step 3: Commit**

```bash
git add Directory.Packages.props
git commit -m "fix(security): update vulnerable package versions"
```

---

## Task 2: Backend Rate Limiting Tests

**Files:**
- Create: `Tests/Business/Handlers/RateLimitTests.cs`

- [ ] **Step 1: Create test file**

Create `Tests/Business/Handlers/RateLimitTests.cs` with tests for GetUsersQuery pagination defaults and constraint validation.

- [ ] **Step 2: Run tests**

Run: `dotnet test Tests/Tests.csproj --filter "RateLimitTests" --verbosity normal`
Expected: Tests pass

- [ ] **Step 3: Commit**

```bash
git add Tests/Business/Handlers/RateLimitTests.cs
git commit -m "test(backend): add rate limiting and pagination tests"
```

---

## Task 3: Backend Exception Handling Tests

**Files:**
- Create: `Tests/Business/Handlers/ExceptionHandlingTests.cs`

- [ ] **Step 1: Create test file**

Create `Tests/Business/Handlers/ExceptionHandlingTests.cs` with tests for GlobalExceptionMiddleware and ErrorResponse.

- [ ] **Step 2: Run tests**

Run: `dotnet test Tests/Tests.csproj --filter "ExceptionHandlingTests" --verbosity normal`
Expected: Tests pass

- [ ] **Step 3: Commit**

```bash
git add Tests/Business/Handlers/ExceptionHandlingTests.cs
git commit -m "test(backend): add exception handling middleware tests"
```

---

## Task 4: Backend Health Check Tests

**Files:**
- Create: `Tests/Business/Handlers/HealthCheckTests.cs`

- [ ] **Step 1: Create test file**

Create `Tests/Business/Handlers/HealthCheckTests.cs` with basic health check verification tests.

- [ ] **Step 2: Run tests**

Run: `dotnet test Tests/Tests.csproj --filter "HealthCheckTests" --verbosity normal`
Expected: Tests pass

- [ ] **Step 3: Commit**

```bash
git add Tests/Business/Handlers/HealthCheckTests.cs
git commit -m "test(backend): add health check tests"
```

---

## Task 5: Vue Frontend Tests

**Files:**
- Create: `clients/vue-admin/src/api/__tests__/client.test.ts`
- Create: `clients/vue-admin/src/composables/__tests__/useErrorHandler.test.ts`
- Create: `clients/vue-admin/src/composables/__tests__/useFormValidation.test.ts`

- [ ] **Step 1-3: Create test files**

Create Vue unit tests for API client, error handler composable, and form validation composable using Vitest.

- [ ] **Step 4: Run Vue tests**

Run: `cd clients/vue-admin && npx vitest run --reporter verbose 2>&1 | head -40`
Expected: Tests pass

- [ ] **Step 5: Commit**

```bash
git add clients/vue-admin/src/api/__tests__/ clients/vue-admin/src/composables/__tests__/
git commit -m "test(frontend): add Vue unit tests"
```

---

## Task 6: Angular Error Handler Tests

**Files:**
- Create: `clients/angular-admin/src/app/core/services/__tests__/error-handler.service.spec.ts`

- [ ] **Step 1: Create test file**

Create Angular unit tests for ErrorHandlerService.

- [ ] **Step 2: Run Angular tests**

Run: `cd clients/angular-admin && npx ng test --watch=false --browsers=ChromeHeadless 2>&1 | tail -20`
Expected: Tests pass

- [ ] **Step 3: Commit**

```bash
git add clients/angular-admin/src/app/core/services/__tests__/
git commit -m "test(frontend): add Angular error handler tests"
```

---

## Task 7: React API Client Improvement

**Files:**
- Modify: `clients/react-admin/src/api/client.ts`
- Create: `clients/react-admin/src/hooks/useErrorHandler.ts`
- Create: `clients/react-admin/src/hooks/index.ts`

- [ ] **Step 1: Update React API client**

Update `clients/react-admin/src/api/client.ts` to add error handling interceptors.

- [ ] **Step 2: Create useErrorHandler hook**

Create `clients/react-admin/src/hooks/useErrorHandler.ts` with React hook for consistent error handling.

- [ ] **Step 3: Create hooks index**

Create `clients/react-admin/src/hooks/index.ts` with barrel exports.

- [ ] **Step 4: Build and verify**

Run: `cd clients/react-admin && npm run build`
Expected: Success

- [ ] **Step 5: Commit**

```bash
git add clients/react-admin/src/api/client.ts clients/react-admin/src/hooks/
git commit -m "feat(frontend): improve React API client and add error handling hook"
```

---

## Task 8: Swagger Enhancement

**Files:**
- Modify: `WebAPI/Startup.cs`

- [ ] **Step 1: Update Swagger configuration**

Update `WebAPI/Startup.cs` to enhance Swagger with OpenAPI info and JWT Bearer authentication.

- [ ] **Step 2: Build and verify**

Run: `dotnet build WebAPI/WebAPI.csproj`
Expected: Success

- [ ] **Step 3: Commit**

```bash
git add WebAPI/Startup.cs
git commit -m "docs(api): enhance Swagger documentation with JWT auth"
```

---

## Task 9: Version Bump to 2.0.0

**Files:**
- Modify: `WebAPI/WebAPI.csproj`
- Modify: `CHANGELOG.md`

- [ ] **Step 1: Update WebAPI version**

Open `WebAPI/WebAPI.csproj` and update to 2.0.0.

- [ ] **Step 2: Update CHANGELOG.md**

Add v2.0.0 entry with all changes.

- [ ] **Step 3: Build and verify**

Run: `dotnet build WebAPI/WebAPI.csproj`
Expected: Success

- [ ] **Step 4: Commit**

```bash
git add WebAPI/WebAPI.csproj CHANGELOG.md
git commit -m "chore(release): bump version to 2.0.0"
```

---

## Testing Checklist

- [ ] `dotnet test` passes with all new backend tests
- [ ] Vue tests pass: `npx vitest run`
- [ ] Angular tests pass: `ng test --watch=false`
- [ ] React build succeeds: `npm run build`
- [ ] Swagger UI shows JWT authentication
- [ ] No vulnerability warnings remain
