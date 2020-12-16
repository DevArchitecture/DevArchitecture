using AutoMapper;
using Core.Entities.Concrete;
using Core.Entities.Concrete.Dtos;



namespace Business.Helpers
{
    public class AutoMapperHelper : Profile
    {
        public AutoMapperHelper()
        {
            CreateMap<User, UserDto>().ReverseMap();


        }
    }
}
