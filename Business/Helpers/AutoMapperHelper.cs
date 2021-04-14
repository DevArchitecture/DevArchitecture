namespace Business.Helpers
{
    using AutoMapper;
    using Core.Entities.Concrete;
    using Core.Entities.Dtos;

    public class AutoMapperHelper : Profile
    {
        public AutoMapperHelper()
        {
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
