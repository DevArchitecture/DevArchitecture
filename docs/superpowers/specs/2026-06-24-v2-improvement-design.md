# DevArchitecture v2.0 Improvement Design

**Date:** 2026-06-24
**Version:** 2.0.0
**Focus Areas:** Security, Test Coverage, React Client, API Documentation

---

## 1. Security Fixes

### 1.1 SharpCompress Vulnerability
- **Issue:** SharpCompress 0.30.1 moderate severity vulnerability (GHSA-6c8g-7p36-r338)
- **Fix:** Update transitive dependency or add explicit reference with fixed version

### 1.2 Snappier Vulnerability
- **Issue:** Snappier 1.0.0 high severity vulnerability (GHSA-pggp-6c3x-2xmx)
- **Fix:** Update transitive dependency or add explicit reference with fixed version

### 1.3 HealthChecks Package Versions
- **Issue:** NU1603 warnings for AspNetCore.HealthChecks.Hangfire (8.0.2) and AspNetCore.HealthChecks.Redis (8.0.2) - version 9.0.0 resolved instead
- **Fix:** Update Directory.Packages.props to use 9.0.0 for both

---

## 2. Test Coverage

### 2.1 Backend Unit Tests
- Rate limiting tests (429 response)
- Exception handling tests (validation, security, unauthorized)
- Health check tests (/healthz, /health)
- Pagination tests

### 2.2 Frontend Tests
- Vue: API client, composables, shared components
- Angular: Interceptor, error handler service
- React: API client, hooks

---

## 3. React Client Improvements

### 3.1 API Client Wrapper
- Consistent with Vue/Angular implementation
- Request interceptor (auth token)
- Response interceptor (error handling)

### 3.2 Error Handling Hook
- React hook for consistent error handling
- Toast notification integration

---

## 4. API Documentation

### 4.1 Swagger Enhancement
- Add OpenAPI info (title, description, contact)
- Add JWT Bearer authentication scheme
- Add security requirement

---

## Implementation Order

1. Security fixes (SharpCompress, Snappier, HealthChecks versions)
2. Backend unit tests
3. Frontend tests (Vue, Angular)
4. React API client and error handling
5. Swagger enhancement
6. Version bump to 2.0.0
