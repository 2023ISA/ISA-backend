﻿using FluentResults;
using ISA.Application.API.Models.Requests;
using ISA.Core.Domain.Dtos;
using ISA.Core.Domain.Entities.Token;
using ISA.Core.Domain.Entities.User;
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
        
        var authToken = await _userService.LoginAsync(loginRequestModel.Email, loginRequestModel.Password);
        
        var cookieOptions = new CookieOptions
        {
            Path = "/",
            HttpOnly = true,
            IsEssential = true, 
            Expires = authToken.RefreshToken.ExpirationDate 
        };

        Response.Cookies.Append("RefreshToken", authToken.RefreshToken.Id.ToString(), cookieOptions);
        return Ok(authToken.AuthToken);
    }

    [HttpGet("VerifyEmail")]
    public async Task VerifyEmail(string email, string token)
    {
        await _userService.VerifyEmail(email, token);
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


    [HttpPost("RegisterNewAdmin")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(Policy = "adminsPolicy")]
    public async Task RegisterCompanyAdmin([FromBody] CorpAdminRegistrationDto corpAdmin)
=> await _userService.AddNewCorpAdmin(corpAdmin);


    [HttpGet("CompanyAdmins")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(Policy = "adminsPolicy")]
    public async Task<IEnumerable<CompanyAdmin>> GettAllCompanyAdmins(int page)
    {
        Guid adminId = Guid.Parse(User.Claims.First(x => x.Type == "id").Value);
        return await _userService.GetAllCompanyAdmins(adminId, page);
    }




};