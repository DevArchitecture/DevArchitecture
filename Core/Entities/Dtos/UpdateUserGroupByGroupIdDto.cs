namespace Core.Entities.Dtos
{
    public class UpdateUserGroupByGroupIdDto : IDto
    {
        public int GroupId { get; set; }
        public int[] UserIds { get; set; }
    }
}
