using Microsoft.AspNetCore.Mvc;
using System;
using VendingMachine.Domain.Drinks;
using VendingMachine.Domain.Reports;
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
            if (String.IsNullOrWhiteSpace(blank.Name)) return Result.Fail("Не указано название напитка");
            if (blank.Nominal is null || blank.Nominal <= 0) return Result.Fail("Стоимость должна быть больше 0 руб.");
            if (blank.Count is null || blank.Count < 0) return Result.Fail("Количество в автомате не должно быть меньше 0");
            if (blank.Image.Length == 0) return Result.Fail("Не указано изображение напитка");

            _drinksRepository.SaveDrink(blank);
            return Result.Success();
        }


        [HttpPost("Drinks/SaveDrinks")]
        public Result SaveDrinks([FromBody] VMDrinkBlank[] blanks)
        {
            foreach (VMDrinkBlank blank in blanks)
            {
                if (blank.Id is null) blank.Id = Guid.NewGuid();
                if (String.IsNullOrWhiteSpace(blank.Name)) return Result.Fail("Не указано название напитка");
                if (blank.Nominal is null || blank.Nominal <= 0) return Result.Fail("Стоимость должна быть больше 0 руб.");
                if (blank.Count is null || blank.Count < 0) return Result.Fail("Количество в автомате не должно быть меньше 0");
                if (blank.Image.Length == 0) return Result.Fail("Не указано изображение напитка");

            }

            _drinksRepository.PurchaseFixation(blanks);
            return Result.Success();
        }

        [HttpGet("Drinks/GetAll")]
        public VMDrink[] GetAllDrinks(Guid vendingMachineId)
        {
            return _drinksRepository.GetAllDrinks(vendingMachineId);
        }

        [HttpPost("Drinks/Delete")]
        public Result DeleteDrink([FromBody] Guid drinkId)
        {
            _drinksRepository.DeleteDrink(drinkId);
            return Result.Success();
        }

        #region DrinkReports

        [HttpPost("Drinks/GetReports")]
        public DrinkReport[] GetDrinkReports([FromBody] Guid[] drinkIds)
        {
            return _drinksRepository.GetDrinkReports(drinkIds);
        }

        #endregion DrinkReports
    }
}
