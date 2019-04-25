using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MoviesCatalog.Data.Models;
using MoviesCatalog.Services;
using MoviesCatalog.Services.Contracts;
using MoviesCatalog.Web.Mappers;
using MoviesCatalog.Web.Mappers.Contracts;
using MoviesCatalog.Web.Models;

namespace MoviesCatalog.Web.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IActorService actorService;
        private readonly IViewModelMapper<Actor, ActorViewModel> actorMapper;

        public ActorsController(IActorService actorService,
                                IViewModelMapper<Actor, ActorViewModel> actorMapper)
        {
            this.actorService = actorService ?? throw new ArgumentNullException(nameof(actorService));
            this.actorMapper = actorMapper ?? throw new ArgumentNullException(nameof(actorMapper));
        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> ActorsBySymbol(char symbol)
        {
            var actors = await this.actorService.ShowActorsStartWithSymbolAsync(symbol);

            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            var actor = await this.actorService.GetActorAsync(id);

            if (actor == null)
            {
                return NotFound();
            }
                
            return View(this.actorMapper.MapFrom(actor));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ActorViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var actor = this.actorService
                                .CreateActorAsync(model.FirstName, model.LastName);


                return RedirectToAction(nameof(Details), new { id = actor.Id });
            }

            catch (ArgumentException ex)
            {
                this.ModelState.AddModelError("Error", ex.Message);
                return View(model);
            }
        }
    }
}