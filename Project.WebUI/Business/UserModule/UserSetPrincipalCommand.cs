﻿using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project.WebUI.AppCode.Infrastructure;
using Project.WebUI.AppCode.Providers;
using Project.WebUI.Models.DataContexts;
using Project.WebUI.Models.Entities.Membership;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Project.WebUI.Business.UserModule
{
    public class UserSetPrincipalCommand : IRequest<JsonResponse>
    {
        public int UserId { get; set; }
        public string PrincipalName { get; set; }
        public bool Selected { get; set; }

        public class UserSetPrincipalCommandHandler : IRequestHandler<UserSetPrincipalCommand, JsonResponse>
        {
            private readonly UserManager<ProjectUser> userManager;
            private readonly ProjectDbContext db;

            public UserSetPrincipalCommandHandler(UserManager<ProjectUser> userManager, ProjectDbContext db)
            {
                this.userManager = userManager;
                this.db = db;
            }
            public async Task<JsonResponse> Handle(UserSetPrincipalCommand request, CancellationToken cancellationToken)
            {
                var response = new JsonResponse
                {
                    Error = false
                };

                var user = await userManager.Users.FirstOrDefaultAsync(m => m.Id == request.UserId, cancellationToken);


                if (user == null)
                {
                    response.Error = true;
                    response.Message = "İstifadəçi mövcud deyil";
                    goto end;
                }
                else if (user.EmailConfirmed == false)
                {
                    response.Error = true;
                    response.Message = "İstifadəçi hesabı təsdiq etməyib";
                    goto end;
                }



                if (Array.IndexOf((AppClaimProvider.principals ?? new string[] { }), request.PrincipalName) == -1)
                {
                    response.Error = true;
                    response.Message = $"{request.PrincipalName} mövcud deyil";
                    goto end;
                }

                var hasClaim = await db.UserClaims.AnyAsync(m => m.UserId == request.UserId && m.ClaimType.Equals(request.PrincipalName)
                && m.ClaimValue.Equals("1"), cancellationToken);

                if (request.Selected && hasClaim)
                {
                    response.Error = true;
                    response.Message = $"`{user.Name} {user.Surname}` artiq {request.PrincipalName} selahiyyete sahibdir";
                    goto end;
                }
                else if (!request.Selected && hasClaim == false)
                {
                    response.Error = true;
                    response.Message = $"`{user.Name} {user.Surname}` {request.PrincipalName} selahiyyetde deyil";
                    goto end;
                }


                if (request.Selected)
                {
                    await userManager.AddClaimAsync(user, new Claim(request.PrincipalName, "1"));

                    response.Message = $"`{user.Name} {user.Surname}` {request.PrincipalName}`a əlavə edildi";
                }
                else
                {
                    await userManager.RemoveClaimAsync(user, new Claim(request.PrincipalName, "1"));

                    response.Message = $"`{user.Name} {user.Surname}` {request.PrincipalName}`dan çıxarıldı";
                }
            end:
                return response;
            }
        }
    }
}
