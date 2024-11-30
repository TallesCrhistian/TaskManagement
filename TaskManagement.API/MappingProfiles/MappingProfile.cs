using AutoMapper;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.ViewModels.Task;
using TaskManagement.Domain.Entities;

namespace TaskManagement.API.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TaskEntity, TaskDTO>()
                .ReverseMap();

            CreateMap<TaskEntity, TaskViewModel>();
            CreateMap<TaskDTO, TaskViewModel>();
            CreateMap<TaskCreateViewModel, TaskDTO>();
            CreateMap<TaskUpdateViewModel, TaskDTO>();
            CreateMap<TaskFilterViewModel, TaskEntity>();
        }
    }
}