using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Anegrului_Cosmina_Lab2.Controllers
{
    public class CustomersGrpcController : Controller
    {
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var client = new CustomerService.CustomerServiceClient(channel);
            Customer customer = client.Get(new CustomerId() { Id = (int)id });
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }
        [HttpPost]
        public IActionResult Edit(int id, Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var client = new CustomerService.CustomerServiceClient(channel);
                Customer response = client.Update(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }
    }
}
