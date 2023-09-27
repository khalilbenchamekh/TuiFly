using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetDevPack.Data;
using TuiFly.Domain.Models.Entities;

namespace TuiFly.Domain.Interfaces
{
    public interface ICustomerRepository : IRepository<CustomerEntity>
    {
        Task<CustomerEntity> GetById(Guid id);
        void Add(CustomerEntity customer);
        void Update(CustomerEntity customer);
    }
}