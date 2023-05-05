using MediatR;
using Microsoft.EntityFrameworkCore;
using Project.WebUI.Models.DataContexts;
using Project.WebUI.Models.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Project.WebUI.Business.RegisterModule
{
    public class RegisterSingleQuery : IRequest<Register>
    {
        public int Id { get; set; }
        public class RegisterSingleQueryHandler : IRequestHandler<RegisterSingleQuery, Register>
        {
            private readonly ProjectDbContext db;

            public RegisterSingleQueryHandler(ProjectDbContext db)
            {
                this.db = db;
            }
            public async Task<Register> Handle(RegisterSingleQuery request, CancellationToken cancellationToken)
            {
                var entity = await db.Registers.FirstOrDefaultAsync(m => m.Id == request.Id && m.DeletedDate == null, cancellationToken);
                return entity;
            }
        }
    }
}
