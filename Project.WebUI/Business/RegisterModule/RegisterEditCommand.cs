using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Project.WebUI.Models.DataContexts;
using Project.WebUI.Models.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Project.WebUI.Business.RegisterModule
{
    public class RegisterEditCommand : IRequest<Register>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FinCode { get; set; }
        public DateTime RegisterDate { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }

        public class RegisterEditCommandHandler : IRequestHandler<RegisterEditCommand, Register>
        {
            private readonly ProjectDbContext db;
            private readonly IHostEnvironment env;

            public RegisterEditCommandHandler(ProjectDbContext db,IHostEnvironment env)
            {
                this.db = db;
                this.env = env;
            }
            public async Task<Register> Handle(RegisterEditCommand request, CancellationToken cancellationToken)
            {
                var model = await db.Registers
                    .FirstOrDefaultAsync(b => b.Id == request.Id && b.DeletedDate == null, cancellationToken);
                if (model == null)
                {
                    return null;
                }
                model.Name = request.Name;
                model.Surname = request.Surname;
                model.FinCode = request.FinCode;
                model.UserName = request.UserName;
                model.UserPassword = request.UserPassword;
                model.RegisterDate = (DateTime)request.RegisterDate;
   
                await db.SaveChangesAsync(cancellationToken);
                return model;

            }
        }
    }
}
