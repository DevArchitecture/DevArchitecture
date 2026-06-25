using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Core.CrossCuttingConcerns.Exceptions;

namespace Tests.Business.Handlers
{
    [TestFixture]
    public class ExceptionHandlingTests
    {
        private DefaultHttpContext _context;
        private Mock<ILogger<GlobalExceptionMiddleware>> _logger;

        [SetUp]
        public void Setup()
        {
            _context = new DefaultHttpContext();
            _logger = new Mock<ILogger<GlobalExceptionMiddleware>>();
        }

        [Test]
        public async Task Middleware_UnauthorizedAccess_Returns401()
        {
            var middleware = new GlobalExceptionMiddleware(
                (inner) => throw new UnauthorizedAccessException(), _logger.Object);
            await middleware.InvokeAsync(_context);
            _context.Response.StatusCode.Should().Be(401);
        }

        [Test]
        public async Task Middleware_ArgumentException_Returns400()
        {
            var middleware = new GlobalExceptionMiddleware(
                (inner) => throw new ArgumentException("invalid"), _logger.Object);
            await middleware.InvokeAsync(_context);
            _context.Response.StatusCode.Should().Be(400);
        }

        [Test]
        public async Task Middleware_KeyNotFound_Returns404()
        {
            var middleware = new GlobalExceptionMiddleware(
                (inner) => throw new KeyNotFoundException(), _logger.Object);
            await middleware.InvokeAsync(_context);
            _context.Response.StatusCode.Should().Be(404);
        }

        [Test]
        public async Task Middleware_GeneralException_Returns500()
        {
            var middleware = new GlobalExceptionMiddleware(
                (inner) => throw new Exception("unexpected"), _logger.Object);
            await middleware.InvokeAsync(_context);
            _context.Response.StatusCode.Should().Be(500);
        }

        [Test]
        public async Task Middleware_InvalidOperation_Returns409()
        {
            var middleware = new GlobalExceptionMiddleware(
                (inner) => throw new InvalidOperationException("conflict"), _logger.Object);
            await middleware.InvokeAsync(_context);
            _context.Response.StatusCode.Should().Be(409);
        }

        [Test]
        public void ErrorResponse_HasRequiredProperties()
        {
            var response = new ErrorResponse
            {
                StatusCode = 400,
                Message = "Bad request",
                Timestamp = DateTime.UtcNow,
                TraceId = "trace-123"
            };
            response.StatusCode.Should().Be(400);
            response.Message.Should().Be("Bad request");
            response.TraceId.Should().Be("trace-123");
        }
    }
}
