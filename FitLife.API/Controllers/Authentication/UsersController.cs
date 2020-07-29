using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitLife.Contracts.Request.Command.Authentication;
using FitLife.Contracts.Response.Authentication;
using FitLife.DB.Models.Authentication;
using FitLife.Shared.Infrastucture.CommandHandler;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FitLife.API.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
       
        private SignInManager<AppUser> _signInManager;
        private readonly IAsyncCommandHandler<RegisterUserCommand, RegisterUserResponse> _registerUserCommandHandler;

        public UsersController(SignInManager<AppUser> signInManager, IAsyncCommandHandler<RegisterUserCommand, RegisterUserResponse> registerUserCommandHandler)
        {
            _signInManager = signInManager;
            _registerUserCommandHandler = registerUserCommandHandler;
        }

        [HttpPost]
        [Route("Register")]
        public Task<RegisterUserResponse> Register(RegisterUserCommand command)
        {
            return _registerUserCommandHandler.Handle(command);
        }
    }
}
