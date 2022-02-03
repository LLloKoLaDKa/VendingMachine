using Microsoft.AspNetCore.Mvc;
using System;
using VendingMachine.Domain.Coins;
using VendingMachine.EntitiesCore.Repositories.Interfaces;

namespace VendingMachine.API.Areas.Coins
{
    public class CoinsController : Controller
    {
        // не хорошо обращаться в репозиторий без отдельного слоя валидации - Service, но из-за сроков можно опустить это ньюанс
        private readonly ICoinsRepository _coinsRepository;

        public CoinsController(ICoinsRepository coinsRepository)
        {
            _coinsRepository = coinsRepository;
        }

        [HttpGet("Coins/GetAll")]
        public VMCoin[] GetCoins(Guid vendingMachineId)
        {
            return _coinsRepository.GetAllCoins(vendingMachineId);
        }
    }
}
