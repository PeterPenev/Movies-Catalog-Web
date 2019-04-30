using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesCatalog.Data.Models;
using MoviesCatalog.Services;
using MoviesCatalog.Services.Contracts;
using MoviesCatalog.Web.Mappers;
using MoviesCatalog.Web.Mappers.Contracts;
using MoviesCatalog.Web.Models;
using MoviesCatalog.Web.Models.AccountViewModels;

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

        public async Task<IActionResult> Index()
        {
            var showTo10Actors = await this.actorService.ShowAllActors();

            var actorIndexView = new ActorIndexViewModel()
            {
                AllActors = showTo10Actors.Select(this.actorMapper.MapFrom).ToList()
            };
            return View(actorIndexView);
        }


        public async Task<IActionResult> ActorsByName(int id)
        {
            var actorsByStartingSymbol = await this.actorService.ShowActorsStartWithSymbolAsync(id);

            var actorIndexView = new ActorIndexViewModel()
            {
                ActorsByName = actorsByStartingSymbol.Select(this.actorMapper.MapFrom).ToList()
            };

            return View(actorIndexView);
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
        //[Authorize(Roles = "Admin")]
        public IActionResult Create(ActorViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var actor = this.actorService
                                .CreateActorAsync(model.FirstName, model.LastName, model.Biography);


                return RedirectToAction(nameof(Index), new { id = actor.Id });
            }

            catch (ArgumentException ex)
            {
                this.ModelState.AddModelError("Error", ex.Message);
                return View(model);
            }
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public IActionResult Update(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin")]
        public IActionResult Update(ActorViewModel model, int id)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var actor = this.actorService
                                .UpdateActorBiographyAsync(model.Id, model.Biography);


                return RedirectToAction(nameof(Index), new { id = actor.Id });
            }

            catch (ArgumentException ex)
            {
                this.ModelState.AddModelError("Error", ex.Message);
                return View(model);
            }
        }
    }
}