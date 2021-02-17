using Masiv.Core.Entities;
using Masiv.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Masiv.Infraestructure.Repositories
{
    public class BetRepository : IBetRepository
    {
        private readonly IConfiguration _configuration;
        public BetRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public int RegisterBet(Bet oBet)
        {
            try
            {
                int BetId = 0;
                var connectionString = _configuration.GetConnectionString("conexion");
                using (var cn = new SqlConnection(connectionString))
                {
                    cn.Open();
                    var cmd = new SqlCommand("UspRegisterBet", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@peBetId", SqlDbType.Int).Value = oBet.BetId;
                    cmd.Parameters.Add("@peNumber", SqlDbType.Int).Value = oBet.Number;
                    cmd.Parameters.Add("@peBetAmount", SqlDbType.Decimal).Value = oBet.BetAmount;
                    cmd.Parameters.Add("@peDateAmount", SqlDbType.DateTime).Value = oBet.DateAmount;
                    cmd.Parameters.Add("@peUserId", SqlDbType.Int).Value = oBet.UserId;
                    cmd.Parameters.Add("@peRouletteId", SqlDbType.Int).Value = oBet.RouletteId;
                    cmd.Parameters.Add("@psBetId", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    BetId = (int)cmd.Parameters["@psBetId"].Value;
                }

                return BetId;
            }
            catch (Exception)
            {
                throw;
            }
        }     
    }

}
