using MediatR;
using Microsoft.EntityFrameworkCore;
using Project.WebUI.Models.DataContexts;
using Project.WebUI.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Project.WebUI.Business.RegisterModule
{
    public class RegistersAllQuery : IRequest<List<Register>>
    {
        public class RegistersAllQueryHandler : IRequestHandler<RegistersAllQuery, List<Register>>
        {
            private readonly ProjectDbContext db;

            public RegistersAllQueryHandler(ProjectDbContext db)
            {
                this.db = db;
            }
            public async Task<List<Register>> Handle(RegistersAllQuery request, CancellationToken cancellationToken)
            {
                var data = await db.Registers
                    .Where(m => m.DeletedDate == null)
                    .ToListAsync(cancellationToken);

                return data;
            }
        }
    }
}
