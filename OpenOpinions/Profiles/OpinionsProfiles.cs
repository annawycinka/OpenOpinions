using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OpenOpinions.Dtos;
using OpenOpinions.Models;

namespace OpenOpinions.Profiles
{
    public class OpinionsProfiles :Profile
    {
        public OpinionsProfiles()
        {
            CreateMap<Opinion, ReadOpinionDto>();
            CreateMap<CreateOpinionDto, Opinion>();
        }
    }
}
