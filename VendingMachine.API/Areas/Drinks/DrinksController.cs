using Microsoft.AspNetCore.Mvc;
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
    }
}
