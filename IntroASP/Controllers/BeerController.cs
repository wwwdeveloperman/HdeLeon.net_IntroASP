using IntroASP.Models;
using IntroASP.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            // Incluir en var beers los Brands relacionados con Beer segun key constrain.
            // Beers y Brands en PLURAL se crean en el Models.HLeonCotext.cs
            // public virtual DbSet<Beer> Beers { get; set; } = null!;
            // public virtual DbSet<Brand> Brands { get; set; } = null!;


            var beers = _context.Beers.Include(b => b.Brand);
            
            return View( await beers.ToListAsync() );
        }

        public IActionResult Create()       //  <<---- Crear la vista de Create. OJO
        {


            // El acceso a la dababase no necesita ser async. se lee Beer y Brand
            // Al correr el controlador se crea un diccionario q no llega como modelo sino como
            // un diccion para acceder desde la view.
            //  
            
            // SelectList usa using Microsoft.AspNetCore.Mvc.Rendering;
            ViewData["Brands"] = new SelectList(_context.Brands, "BrandId", "Name");

            return View();

            // ViewData[] es el diccionario
            // se lo carga con SelectList, un obj de Microsoft Rendering. poner using.
            // El origen de datos seria _context.Brands
            // los campos del diccionario son los BrandId y Name, un diccionario
            // siempre tiene un key y un Valor. el key no se vera sino q se mostrara
            // el Name en la vista.

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult>  Create(BeerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var beer = new Beer()
                {
                    Name = model.Name,
                    BrandId = model.BrandId
                };
                
                // Cargar buffer de EntityFramework.
                _context.Beers.Add(beer); 

                // Save in the Database :
                await _context.SaveChangesAsync();

                // Redireccionar:
                return RedirectToAction("Index");   


            }//if

            ViewData["Brands"] = new SelectList(_context.Brands, "BrandId", "Name", model.BrandId);

            return View(model);

        }



    }// class
}
