using System.Collections.Generic;

namespace Core.Entities.Dtos
{
    public class ShowcasePageDto : IEntity
    {
        public IReadOnlyList<ShowcaseRowDto> Items { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }
        public double ServerProcessingMs { get; set; }
        public int ActualGeneratedRows { get; set; }
        public double ActualGenerationMs { get; set; }
        public double ActualNsPerRow { get; set; }
        public long SimulatedTotalRecords { get; set; }
        public double EstimatedServerMsForSimulatedTotal { get; set; }
        public double EstimatedRowsPerSecond { get; set; }
    }
}
