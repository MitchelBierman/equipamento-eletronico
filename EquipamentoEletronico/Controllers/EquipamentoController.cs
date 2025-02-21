using Microsoft.AspNetCore.Mvc;

namespace EquipamentoEletronicoAPI.Controllers
{
    public class EquipamentoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
