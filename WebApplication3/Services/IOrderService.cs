using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models;

namespace WebApplication3.Services
{
    public interface IOrderService
    {
        public Task<IList<Order>> GetAll();
        public Task Update(Order order);

    }
}
