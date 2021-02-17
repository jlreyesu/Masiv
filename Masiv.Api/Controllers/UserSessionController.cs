using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Masiv.Core.Entities;
using Masiv.Infraestructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Masiv.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSessionController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetPost()
        {
            var post = new UserSessionRepository().ListUserSession();
            return Ok(post);
        }
    }
}