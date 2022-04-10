using IntroASP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IntroASP.Controllers
{
    public class BeerController : Controller
    {
        private readonly HLeonContext _context;

        public BeerController(HLeonContext context)
        {
            _context = context;
        }

        // IActionResult es p identificar una interfaz como puede ser json, vista, etc
        public async Task<IActionResult> Index()
        {
            // Incluir en var beers los Brand relacionados con Beer segun key constrain.
            var beers = _context.Beers.Include(b => b.Brand);
            
            return View( await beers.ToListAsync() );
        }


    }// class
}
