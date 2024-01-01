﻿using AutoMapper;
using Business.Dtos.Requests.CreateRequests;
using Business.Dtos.Requests.DeleteRequests;
using Business.Dtos.Requests.UpdateRequests;
using Business.Dtos.Responses.CreatedResponses;
using Business.Dtos.Responses.DeletedResponses;
using Business.Dtos.Responses.GetListResponses;
using Business.Dtos.Responses.UpdatedResponses;
using Core.DataAccess.Paging;
using Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Profiles
{
    public class DistrictProfile : Profile
    {
        public DistrictProfile()
        {

            CreateMap<District, CreateDistrictRequest>().ReverseMap();
            CreateMap<District, CreatedDistrictResponse>().ReverseMap();

            CreateMap<District, UpdateDistrictRequest>().ReverseMap();
            CreateMap<District, UpdatedDistrictResponse>().ReverseMap();

            CreateMap<District, DeleteDistrictRequest>().ReverseMap();
            CreateMap<District, DeletedDistrictResponse>().ReverseMap();

            CreateMap<District, GetListDistrictResponse>()

                .ForMember(destinationMember: response => response.CityName,
                memberOptions: opt => opt.MapFrom(d => d.City.Name))
                .ReverseMap();

            CreateMap<IPaginate<District>, Paginate<GetListDistrictResponse>>().ReverseMap();
            
        }
    }
}
