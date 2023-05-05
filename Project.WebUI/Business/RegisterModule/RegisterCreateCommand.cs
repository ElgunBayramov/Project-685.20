using MediatR;
using Microsoft.Extensions.Hosting;
using Project.WebUI.Models.DataContexts;
using Project.WebUI.Models.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Project.WebUI.Business.RegisterModule
{
    public class RegisterCreateCommand : IRequest<Register>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FinCode { get; set; }
        public DateTime RegisterDate { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public int ProfessionId { get; set; }


        public class RegisterCreateCommandHandler : IRequestHandler<RegisterCreateCommand, Register>
        {
            private readonly ProjectDbContext db;
            private readonly IHostEnvironment env;

            public RegisterCreateCommandHandler(ProjectDbContext db,IHostEnvironment env)
            {
                this.db = db;
                this.env = env;
            }
            public async Task<Register> Handle(RegisterCreateCommand request, CancellationToken cancellationToken)
            {
                var register = new Register();
                register.Name = request.Name;
                register.Surname = request.Surname;
                register.FinCode = request.FinCode;
                register.UserName = request.UserName;
                register.UserPassword = request.UserPassword;
                register.RegisterDate = (DateTime)request.RegisterDate;
                register.ProfessionId = request.ProfessionId;

                await db.Registers.AddAsync(register,cancellationToken);
                await db.SaveChangesAsync(cancellationToken);
                return register;


            }
        }
    }
}
