﻿using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Project.WebUI.Business.AccountModule;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Project.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMediator mediator;

        public AccountController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        public async Task<IActionResult> Signin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signin(SigninCommand command)
        {
            var user = await mediator.Send(command);

            if (!ModelState.IsValid)
            {
                return View(command);
            }

            var claims = new[] { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            var props = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(20),
            };


            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(principal), props);

            var callbackUrl = Request.Query["ReturnUrl"].ToString();

            if (!string.IsNullOrWhiteSpace(callbackUrl))
            {
                return Redirect(callbackUrl);
            }

            return RedirectToAction("Index", "Home", new { area = "Admin" });

        }


       
    }
}
