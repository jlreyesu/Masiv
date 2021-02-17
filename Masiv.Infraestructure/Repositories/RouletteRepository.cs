using Masiv.Core.Entities;
using Masiv.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Masiv.Infraestructure.Repositories
{
    public class RouletteRepository :IRouletteRepository
    {
        private readonly IConfiguration _configuration;
        public RouletteRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

       
        public IEnumerable<Roulette> ListRoulette(string pState)
        {
            try
            {
                List<Roulette> lstRoulette = null;
                Roulette olRoulette = null;


                var connectionString = _configuration.GetConnectionString("conexion");
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("UspListRoulette", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@peState", SqlDbType.VarChar, 10).Value = pState;

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        lstRoulette = new List<Roulette>();
                        while (dr.Read())
                        {
                            olRoulette = new Roulette()
                            {
                                RouletteId = dr.IsDBNull(dr.GetOrdinal("RouletteId")) ? -1 : dr.GetInt32(dr.GetOrdinal("RouletteId")),
                                Names = dr.IsDBNull(dr.GetOrdinal("Names")) ? "" : dr.GetString(dr.GetOrdinal("Names")),
                                State = dr.IsDBNull(dr.GetOrdinal("State")) ? "" : dr.GetString(dr.GetOrdinal("State")),
                                Active = dr.IsDBNull(dr.GetOrdinal("Active")) ? false : dr.GetBoolean(dr.GetOrdinal("Active"))
                            };
                            lstRoulette.Add(olRoulette);
                        }
                    }
                    else
                    {
                        lstRoulette=null;
                    }

                    return lstRoulette.ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
          

        }
        public int RegisterRoulette(Roulette oRoulette)
        {
            try
            {
                int RouletteId = 0;
                var connectionString = _configuration.GetConnectionString("conexion");
                using (var cn = new SqlConnection(connectionString))
                {
                    cn.Open();
                    var cmd = new SqlCommand("UspRegisterRoulette", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@peRouletteId", SqlDbType.Int).Value = oRoulette.RouletteId;
                    cmd.Parameters.Add("@peNames", SqlDbType.VarChar, 50).Value = oRoulette.Names;
                    //cmd.Parameters.Add("@peAmountMax", SqlDbType.Decimal).Value = oRoulette.AmountMax;                    
                    cmd.Parameters.Add("@psRouletteId", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    RouletteId = (int)cmd.Parameters["@psRouletteId"].Value;
                }

                return RouletteId;
            }
            catch (Exception)
            {
                throw;
            }            
        }
        public string OpenRoulette(int RouletteId)
        {
            try
            {
                string vsMensaje = "";
                var connectionString = _configuration.GetConnectionString("conexion");
                using (SqlConnection cn = new SqlConnection(connectionString))
                {

                    cn.Open();
                    SqlCommand cmd = new SqlCommand("UspOpenRoulette", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@peRouletteId", SqlDbType.Int).Value = RouletteId;
                    cmd.Parameters.Add("@psMessage", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    vsMensaje = (string)cmd.Parameters["@psMessage"].Value;
                }
                return vsMensaje;
            }
            catch (Exception)
            {
                throw;
            }            
        }
        public IEnumerable<Bet> CloseRoulette(int RouletteId)
        {
            try
            {
                List<Bet> lstBet = null;
                Bet oBet = null;

                var connectionString = _configuration.GetConnectionString("conexion");
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("UspCloseRoulette", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@peRouletteId", SqlDbType.Int).Value = RouletteId;

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        lstBet = new List<Bet>();
                        while (dr.Read())
                        {
                            oBet = new Bet()
                            {
                                BetId = dr.IsDBNull(dr.GetOrdinal("BetId")) ? 0 : dr.GetInt32(dr.GetOrdinal("BetId")),
                                Number = dr.IsDBNull(dr.GetOrdinal("Number")) ? 0 : dr.GetInt32(dr.GetOrdinal("Number")),
                                Color = dr.IsDBNull(dr.GetOrdinal("Color")) ? "" : dr.GetString(dr.GetOrdinal("Color")),
                                BetAmount = dr.IsDBNull(dr.GetOrdinal("BetAmount")) ? 0 : dr.GetDecimal(dr.GetOrdinal("BetAmount")),
                                DateAmount = dr.IsDBNull(dr.GetOrdinal("DateAmount")) ? DateTime.MinValue : dr.GetDateTime(dr.GetOrdinal("DateAmount")),
                                Winner = dr.IsDBNull(dr.GetOrdinal("Winner")) ? false : dr.GetBoolean(dr.GetOrdinal("Winner")),
                                PaymentxN = dr.IsDBNull(dr.GetOrdinal("PaymentxN")) ?0 : dr.GetDecimal(dr.GetOrdinal("PaymentxN")),
                                PaymentxColor = dr.IsDBNull(dr.GetOrdinal("PaymentxColor")) ? 0 : dr.GetDecimal(dr.GetOrdinal("PaymentxColor")),
                                TotalxN = dr.IsDBNull(dr.GetOrdinal("TotalxN")) ? 0 : dr.GetDecimal(dr.GetOrdinal("TotalxN")),
                                TotalxColor = dr.IsDBNull(dr.GetOrdinal("TotalxColor")) ? 0 : dr.GetDecimal(dr.GetOrdinal("TotalxColor")),
                                Total = dr.IsDBNull(dr.GetOrdinal("Total")) ? 0: dr.GetDecimal(dr.GetOrdinal("Total")),                                
                                State = dr.IsDBNull(dr.GetOrdinal("State")) ? "" : dr.GetString(dr.GetOrdinal("State")),
                                ClientId = dr.IsDBNull(dr.GetOrdinal("ClientId")) ? 0 : dr.GetInt32(dr.GetOrdinal("ClientId")),
                                UserId = dr.IsDBNull(dr.GetOrdinal("UserId")) ? 0 : dr.GetInt32(dr.GetOrdinal("UserId")),
                                RouletteId = dr.IsDBNull(dr.GetOrdinal("RouletteId")) ? 0 : dr.GetInt32(dr.GetOrdinal("RouletteId"))
                            };
                            lstBet.Add(oBet);
                        }
                    }

                    return lstBet.ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
           
        }
    }
}
