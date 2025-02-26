using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Persistence;

public interface IUnitOfWork
{
    IAuthRepository Auths { get; set; }
    //IUserRepository<ApplicationUser> Users { get; }
    //IRoleRepository<ApplicationRole> Roles { get; }
    //ICategoryRepository Categories { get; }
    //IProductRepository Products { get; }
    //IItemRepository Items { get; }
    //IOrderRepository Orders { get; }
    //ILogisticRepository Logistics { get; }
    //IDriverRepository Drivers { get; }
    //IReviewRepository Reviews { get; }
    //IVendorRepository Vendors { get; }
    Task CompleteAsync();
}
