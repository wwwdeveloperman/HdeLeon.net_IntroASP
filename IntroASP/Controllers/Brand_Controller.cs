using IntroASP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IntroASP.Controllers
{
    public class Brand_Controller : Controller
    {
        // ------------------------------------------------
        // Crear var _context y en el constructor cargarla con
        // el parametro context.
        // Si HLeonContext no fue inyectado en Program.cs NO se lo podra
        // usar en el Controller.

        public readonly HLeonContext _context;

        public Brand_Controller(HLeonContext context)
        {
            _context = context;
        }
        // -------------------------------------------------


        // AGREGAR using EntityFrameworkCore PARA .ToListAsync
        public async Task<IActionResult> Index()
        {
            return View( 
                await _context.Brands.ToListAsync()
            );

            /* o bien:
             * 
             => View( await _context.Brands.ToListAsync() );
             
            */
         



        }


    }// class
}
