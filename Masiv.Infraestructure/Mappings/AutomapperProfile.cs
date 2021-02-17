using AutoMapper;
using Masiv.Core.DTOs;
using Masiv.Core.Entities;

namespace Masiv.Infraestructure.Mappings
{
    public  class AutomapperProfile:Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Roulette, RouletteDTO>();
            CreateMap<RouletteDTO, Roulette>();

            CreateMap<Bet, BetDTO>();
            CreateMap<BetDTO, Bet>();
        }
    }
}
