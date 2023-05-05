using MediatR;
using Microsoft.EntityFrameworkCore;
using Project.WebUI.AppCode.Infrastructure;
using Project.WebUI.Models.DataContexts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Project.WebUI.Business.RegisterModule
{
    public class RegisterRemoveCommand : PageableModel, IRequest<JsonResponse>
    {
        public int Id { get; set; }
        public class RegisterRemoveCommandHandler : IRequestHandler<RegisterRemoveCommand, JsonResponse>
        {
            private readonly ProjectDbContext db;

            public RegisterRemoveCommandHandler(ProjectDbContext db)
            {
                this.db = db;
            }

            public async Task<JsonResponse> Handle(RegisterRemoveCommand request, CancellationToken cancellationToken)
            {
                var entity = await db.Registers
               .FirstOrDefaultAsync(m => m.Id == request.Id && m.DeletedDate == null, cancellationToken);


                if (entity == null)
                {
                    return new JsonResponse
                    {
                        Error = true,
                        Message = "Qeyd tapilmadi"
                    };
                }

                entity.DeletedDate = DateTime.UtcNow.AddHours(4);
                await db.SaveChangesAsync(cancellationToken);

                return new JsonResponse
                {
                    Error = false,
                    Message = "Ugurludur"
                };
            }
        }
    }
}
