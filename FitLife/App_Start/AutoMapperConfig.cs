using FitLife.Models;
using FitLife.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using FitLife.Models.DBModels;

namespace FitLife.App_Start
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<AppMappingProfile>();
            });
        }
    }

    public class AppMappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<ApplicationUser, UserProfileDTO>();

            CreateMap<UserProfileDTO, ApplicationUser>()
                .ForMember(dst => dst.Id, map => map.DoNotUseDestinationValue());

            CreateMap<Plan, PlanDTO>()
                .ForMember(d => d.Url, d => d.MapFrom(m => "api/Plans/" + m.ID));
                
           CreateMap<Workout, WorkoutDTO>();
           CreateMap<Set, SetDTO>();
                
                 
           
        }
    }
}