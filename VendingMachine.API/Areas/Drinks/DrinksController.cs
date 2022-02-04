using Microsoft.AspNetCore.Mvc;
using System;
using VendingMachine.Domain.Drinks;
using VendingMachine.Domain.Results;
using VendingMachine.EntitiesCore.Repositories.Interfaces;

namespace VendingMachine.API.Areas.Drinks
{
    [Area("Drinks")]
    public class DrinksController : Controller
    {
        // не хорошо обращаться в репозиторий без отдельного слоя валидации - Service, но из-за сроков можно опустить это ньюанс
        private readonly IDrinksRepository _drinksRepository;

        public DrinksController(IDrinksRepository drinksRepository)
        {
            _drinksRepository = drinksRepository;
        }

        [HttpPost("Drinks/Save")]
        public Result SaveDrink([FromBody] VMDrinkBlank blank)
        {
            if (blank.Id is null) blank.Id = Guid.NewGuid();

            _drinksRepository.SaveDrink(blank);
            return Result.Success();
        }

        [HttpGet("Drinks/GetAll")]
        public VMDrink[] GetAllDrinks(Guid vendingMachineId)
        {
            return _drinksRepository.GetAllDrinks(vendingMachineId);
        }
    }
}
