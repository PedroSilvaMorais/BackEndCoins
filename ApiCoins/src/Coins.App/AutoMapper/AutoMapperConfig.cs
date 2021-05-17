using AutoMapper;
using Coins.App.ViewModels;
using Coins.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coins.App.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Coin, CoinViewModel>().ReverseMap();
        }
    }
}
