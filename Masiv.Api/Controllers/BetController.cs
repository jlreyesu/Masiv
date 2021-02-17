using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Masiv.Core.DTOs;
using Masiv.Core.Entities;
using Masiv.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Masiv.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BetController : ControllerBase
    {
        private readonly IBetRepository _betRepository;
        private readonly IMapper _mapper;
        public BetController(IBetRepository betRepository, IMapper mapper)
        {
            this._betRepository = betRepository;
            this._mapper = mapper;
        }

        [HttpPost]
        [Route("RegisterBet")]
        public IActionResult RegisterBet(BetDTO betDTO)
        {
            try
            {
                int BetId = 0;
                var oBet = _mapper.Map<Bet>(betDTO);
                BetId = _betRepository.RegisterBet(oBet);
                return Ok(BetId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }      
    }
}