using MediatR;
using Project.WebUI.AppCode.Infrastructure;
using Project.WebUI.Models.DataContexts;
using Project.WebUI.Models.Entities;
using Project.WebUI.Models.ViewModels;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Project.WebUI.Business.RegisterModule
{
    public class RegisterPagedQuery : PageableModel, IRequest<PagedViewModel<Register>>
    {
        public override int PageSize
        {
            get
            {
                if (base.PageSize < 6)
                    base.PageSize = 6;

                return base.PageSize;
            }
        }

        public class RegisterPagedQueryHandler : IRequestHandler<RegisterPagedQuery, PagedViewModel<Register>>
        {
            private readonly ProjectDbContext db;

            public RegisterPagedQueryHandler(ProjectDbContext db)
            {
                this.db = db;
            }

            public async Task<PagedViewModel<Register>> Handle(RegisterPagedQuery request, CancellationToken cancellationToken)
            {
                var query = db.Registers
                 .Where(m => m.DeletedDate == null)
                 .AsQueryable();

                query = query.OrderByDescending(m => m.Id);

                var data = new PagedViewModel<Register>(query, request.PageIndex, request.PageSize);

                return data;
            }
        }
    }
}
