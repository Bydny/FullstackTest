using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FullstackTest.API.Models;
using FullstackTest.Application.Abstractions.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FullstackTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        /// <summary>
        /// Returns authorization bearer token.
        /// </summary>
        /// <param name="model">User's credentials.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] AuthUserModel model)
        {
            try
            {
                var token = await authenticationService.GenerateTokenAsync(model.Email, model.Password);
                return Ok(token);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}