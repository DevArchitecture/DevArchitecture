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
