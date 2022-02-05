using Microsoft.AspNetCore.Mvc;
using System;
using VendingMachine.Domain;
using VendingMachine.Domain.Results;
using VendingMachine.EntitiesCore.Repositories.Interfaces;

namespace VendingMachine.API.Areas.VendingMachines.Controllers
{
    public class VendingMachinesController : Controller
    {
        private readonly IVendingMachineRepository _vendingMachineRepository;

        public VendingMachinesController(IVendingMachineRepository vendingMachineRepository)
        {
            _vendingMachineRepository = vendingMachineRepository;
        }

        [HttpGet("VendingMachines/GetById")]
        public VendingMachineDomain GetVendingMachine(Guid vendingMachineId)
        {
            return _vendingMachineRepository.GetVendingMachine(vendingMachineId);
        }

        [HttpPost("VendingMachines/Login")]
        public Result Login([FromBody] String password, Guid vendingMachineId)
        {
            Boolean result = _vendingMachineRepository.Login(vendingMachineId, password);
            return result ? Result.Success() : Result.Fail("Неверный секретный код");
        }
    }
}
