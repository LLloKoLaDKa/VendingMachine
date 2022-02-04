using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using VendingMachine.Domain.Coins;
using VendingMachine.Domain.Results;
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

        [HttpPost("Coins/Save")]
        public Result SaveCoins([FromBody] VMCoinBlank[] blanks)
        {
            // проверка на 0 0 0 0
            if (!blanks.Any(blanks => blanks.IsActive)) return Result.Fail("Хотя бы одна монета должна быть не заблокированной");

            _coinsRepository.SaveCoins(blanks);
            return Result.Success();
        }

        [HttpGet("Coins/GetAll")]
        public VMCoin[] GetCoins(Guid vendingMachineId)
        {
            return _coinsRepository.GetAllCoins(vendingMachineId);
        }
    }
}
