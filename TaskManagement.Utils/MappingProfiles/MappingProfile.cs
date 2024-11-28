using AutoMapper;
using TaskManagement.Domain.DTOs;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Utils.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TaskEntity, TaskDTO>()
                .ReverseMap();
        }
    }
}