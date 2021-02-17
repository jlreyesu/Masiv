using AutoMapper;
using Masiv.Core.DTOs;
using Masiv.Core.Entities;
using Masiv.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Masiv.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouletteController : ControllerBase
    {
        private readonly IRouletteRepository _rouletteRepository;
        private readonly IMapper _mapper;
        public RouletteController(IRouletteRepository rouletteRepository, IMapper mapper)
        {
            this._rouletteRepository = rouletteRepository;
            this._mapper = mapper;
        }

        [HttpGet]      
        [Route("List/{state}")]
        public IActionResult List(string state)
        {
            try
            {
                var oRoulette = _rouletteRepository.ListRoulette(state);
                var rouletteDto = _mapper.Map<IEnumerable<RouletteDTO>>(oRoulette);

                return Ok(rouletteDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }                               
        }
        
        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RouletteDTO rouletteDTO)
        {
            try
            {
                int RouletteId = 0;
                var oRoulette = _mapper.Map<Roulette>(rouletteDTO);
                RouletteId = _rouletteRepository.RegisterRoulette(oRoulette);
                return Ok(RouletteId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }           
        }

        [HttpPost]
        [Route("Open/{id}")]
        public IActionResult Open(int id)
        {
            try
            {
                var vsMessage = _rouletteRepository.OpenRoulette(id);
                return Ok(vsMessage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        [HttpGet]
        [Route("Close/{id}")]
        public IActionResult Close(int id)
        {
            try
            {
                var oBet = _rouletteRepository.CloseRoulette(id);
                var betDTO = _mapper.Map<IEnumerable<BetDTO>>(oBet);

                return Ok(betDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}