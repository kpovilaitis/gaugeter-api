using AutoMapper;
using Gaugeter.Api.Authentication.Models.Data;
using Gaugeter.Api.Authentication.Models.Dto;
using Gaugeter.Api.Devices.Models.Data;
using Gaugeter.Api.Devices.Models.Dto;
using Gaugeter.Api.Jobs.Models.Data;
using Gaugeter.Api.Jobs.Models.Dto;
using Gaugeter.Api.Users.Models.Data;
using Gaugeter.Api.Users.Models.Dto;

namespace Gaugeter.Api.Helpers.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>()
                .IncludeAllDerived()
                .AfterMap((src, dest) =>
                {
                    dest.Password = null;
                });

            CreateMap<Login, LoginDto>();
            
            CreateMap<DeviceDto, Device>(MemberList.Source);
            
            CreateMap<JobDto, Job>(MemberList.Source);
            CreateMap<TelemDataDto, Jobs.Models.Data.TelemData>(MemberList.Source);
        }
    }
}
