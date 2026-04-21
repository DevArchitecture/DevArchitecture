using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Dtos;
using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Showcase.Queries
{
    public class GetShowcaseRowsQuery : IRequest<IDataResult<ShowcasePageDto>>
    {
        private const int MaxPageSize = 200;
        private const int MinPageSize = 10;
        private static readonly DateTime SeedDate = new(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 50;

        public class GetShowcaseRowsQueryHandler : IRequestHandler<GetShowcaseRowsQuery, IDataResult<ShowcasePageDto>>
        {
            [SecuredOperation(Priority = 1)]
            [PerformanceAspect(5)]
            [LogAspect(typeof(FileLogger))]
            public Task<IDataResult<ShowcasePageDto>> Handle(GetShowcaseRowsQuery request, CancellationToken cancellationToken)
            {
                const int totalRecords = 1_000_000;
                const long simulatedTotalRecords = 1_000_000_000;
                var stopwatch = Stopwatch.StartNew();

                var page = request.Page < 1 ? 1 : request.Page;
                var pageSize = request.PageSize < MinPageSize
                    ? MinPageSize
                    : request.PageSize > MaxPageSize
                        ? MaxPageSize
                        : request.PageSize;

                var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
                if (page > totalPages)
                {
                    page = totalPages;
                }

                var startId = ((page - 1) * pageSize) + 1;
                var endExclusive = Math.Min(startId + pageSize, totalRecords + 1);
                var items = new List<ShowcaseRowDto>(Math.Max(0, endExclusive - startId));
                var generationStopwatch = Stopwatch.StartNew();

                for (var id = startId; id < endExclusive; id++)
                {
                    var amountSeed = ((long)id * 37L) % 100000L;
                    items.Add(new ShowcaseRowDto
                    {
                        Id = id,
                        Code = $"ROW-{id:0000000}",
                        Amount = Math.Round(amountSeed / 100m, 2),
                        CreatedAt = SeedDate.AddMinutes(id % 525600),
                        Category = $"C{((id % 12) + 1):00}",
                        Status = id % 2 == 0
                    });
                }
                generationStopwatch.Stop();

                stopwatch.Stop();
                var elapsedMs = Math.Max(stopwatch.Elapsed.TotalMilliseconds, 0.01d);
                var actualGenerationMs = Math.Max(generationStopwatch.Elapsed.TotalMilliseconds, 0.01d);
                var producedRows = Math.Max(items.Count, 1);
                var rowsPerMs = producedRows / actualGenerationMs;
                var actualNsPerRow = Math.Round((actualGenerationMs * 1_000_000d) / producedRows, 2);
                var estimatedMsForSimulatedTotal = Math.Round(rowsPerMs <= 0d ? 0d : simulatedTotalRecords / rowsPerMs, 2);
                var estimatedRowsPerSecond = Math.Round(rowsPerMs * 1000d, 2);

                var pageData = new ShowcasePageDto
                {
                    Items = items,
                    PageNumber = page,
                    PageSize = pageSize,
                    TotalRecords = totalRecords,
                    TotalPages = totalPages,
                    ServerProcessingMs = Math.Round(elapsedMs, 2),
                    ActualGeneratedRows = items.Count,
                    ActualGenerationMs = Math.Round(actualGenerationMs, 2),
                    ActualNsPerRow = actualNsPerRow,
                    SimulatedTotalRecords = simulatedTotalRecords,
                    EstimatedServerMsForSimulatedTotal = estimatedMsForSimulatedTotal,
                    EstimatedRowsPerSecond = estimatedRowsPerSecond
                };

                IDataResult<ShowcasePageDto> result = new SuccessDataResult<ShowcasePageDto>(pageData);
                return Task.FromResult(result);
            }
        }
    }
}
