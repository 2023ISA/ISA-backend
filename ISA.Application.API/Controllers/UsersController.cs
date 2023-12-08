﻿using ISA.Application.API.Models.Requests;
using ISA.Core.Domain.Entities.Token;
using ISA.Core.Domain.UseCases.User;
using ISA.Core.Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ISA.Application.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;
    private readonly IHttpContextAccessor _contextAccessor;
    public UsersController(UserService userService, IHttpContextAccessor contextAccessor)
    {
        _userService= userService;
        _contextAccessor= contextAccessor;
    }

    [HttpPost("Register")]
    public async Task RegisterUser([FromBody] CustomerRegistrationRequestModel registrationRequestModel)
    => await _userService.AddAsync(registrationRequestModel.Email,
                                   registrationRequestModel.Password,
                                   registrationRequestModel.Firstname,
                                   registrationRequestModel.Lastname,
                                   registrationRequestModel.City,
                                   registrationRequestModel.Country,
                                   registrationRequestModel.DateOfBirth,
                                   registrationRequestModel.PhoneNumber,
                                   registrationRequestModel.Profession,
                                   registrationRequestModel.CompanyInfo,
                                   IdentityRoles.CUSTOMER);

    [HttpPost("Login")]
    public async Task<IActionResult> LoginUser([FromBody] LoginRequestModel loginRequestModel)
    {
        var token = new LoginCookie();
        try
        {
            token = await _userService.LoginAsync(loginRequestModel.Email, loginRequestModel.Password);
        }catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }

        var cookieOptions = new CookieOptions
        {
            Path = "/",
            HttpOnly = true,
            IsEssential = true, 
            Expires = token.RefreshToken.ExpirationDate 
        };

        Response.Cookies.Append("RefreshToken", token.RefreshToken.Id.ToString(), cookieOptions);

        return Ok(token.AuthToken);
    }

    [HttpGet("VerifyEmail")]
    public async Task<IActionResult> VerifyEmail(string email, string token)
    {   
        await _userService.VerifyEmail(email, token);
        return Ok();
    }

    [HttpPost("EditProfile")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(Policy = "allowAllPolicy")]
    public async Task<IActionResult> EditProfile([FromBody] EditProfileRequestModel editProfileRequestModel)
    {
        Guid id = Guid.Parse(User.Claims.First(x => x.Type == "id").Value); // TO DO: Make as exstension method

        await _userService.UpdateUserAsync(id,
                                           editProfileRequestModel.Name,
                                           editProfileRequestModel.Lastname,
                                           editProfileRequestModel.PhoneNumber,
                                           editProfileRequestModel.DateOfBirth);
        return Ok();
    }


};