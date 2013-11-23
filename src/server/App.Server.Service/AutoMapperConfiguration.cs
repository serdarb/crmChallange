﻿using App.Domain;
using App.Domain.Contracts;
using AutoMapper;

namespace App.Server.Service
{
    public class AutoMapperConfiguration
    {
        public static void CreateMaps()
        {
            Mapper.CreateMap(typeof (UserDto), typeof (User));
            Mapper.CreateMap(typeof (User), typeof (UserDto));
        }
    }
}
