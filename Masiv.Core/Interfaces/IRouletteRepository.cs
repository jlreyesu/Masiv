using Masiv.Core.Entities;
using System.Collections.Generic;
using System.Data;

namespace Masiv.Core.Interfaces
{
    public interface IRouletteRepository
    {
        IEnumerable<Roulette> ListRoulette(string pState);
        int RegisterRoulette(Roulette oRoulette);

        string OpenRoulette(int RouletteId);
        IEnumerable<Bet> CloseRoulette(int RouletteId);
    }
}
