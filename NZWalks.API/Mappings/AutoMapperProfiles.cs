﻿using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region,RegionDto>().ReverseMap();
            CreateMap<Walk,WalkDTO>().ReverseMap();
            CreateMap<Walk,WalkDTOCreateandUpdate>().ReverseMap();
            CreateMap<Difficulty,DifficultyDTO>().ReverseMap();

        }
    }
}
