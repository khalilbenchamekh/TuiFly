using System;
using System.Threading.Tasks;
using TuiFly.Domain.Interfaces;
using TuiFly.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;
using TuiFly.Domain.Models.Entities;

namespace TuiFly.Infra.Data.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        protected readonly TuiFlyContext Db;
        protected readonly DbSet<CustomerEntity> DbSet;

        public CustomerRepository(TuiFlyContext context)
        {
            Db = context;
            DbSet = Db.Set<CustomerEntity>();
        }

        public IUnitOfWork UnitOfWork => Db;

        public async Task<CustomerEntity> GetById(Guid id)
        {
            return await DbSet.FindAsync(id);
        }
        
        public void Add(CustomerEntity customer)
        {
           DbSet.Add(customer);
        }

        public void Update(CustomerEntity customer)
        {
            DbSet.Update(customer);
        }

        public void Dispose()
        {
            Db.Dispose();
        }
    }
}
