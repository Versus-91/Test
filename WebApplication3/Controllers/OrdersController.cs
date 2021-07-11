using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models;
using WebApplication3.Services;

namespace WebApplication3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController:Controller
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet]
        public async Task<IList<Order>> GetAll()
        {
            return await _orderService.GetAll();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Order orderInput)
        {
            await _orderService.Update(orderInput);
            return NoContent();
        }
    }
}
