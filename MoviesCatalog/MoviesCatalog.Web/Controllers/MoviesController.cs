﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MoviesCatalog.Data.Models;
using MoviesCatalog.Services.Contracts;
using MoviesCatalog.Web.Mappers.Contracts;
using MoviesCatalog.Web.Models;

namespace MoviesCatalog.Web.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService movieService;
        private readonly IViewModelMapper<Movie, MovieViewModel> movieViewMapper;
        private readonly IViewModelMapper<Review,ReviewViewModel> reviewViewMapper;
        private readonly IViewModelMapper<Genre, GenreViewModel> genreViewMapper;
        private readonly IViewModelMapper<Actor, ActorViewModel> actorViewMapper;

        public MoviesController(IMovieService movieService,
                                IViewModelMapper<Movie, MovieViewModel> movieViewMapper,
                                IViewModelMapper<Review, ReviewViewModel> reviewViewMapper,
                                IViewModelMapper<Genre,GenreViewModel> genreViewMapper,
                                IViewModelMapper<Actor, ActorViewModel> actorViewMapper)
        {
            this.movieService = movieService ?? throw new ArgumentNullException(nameof(movieService));
            this.movieViewMapper = movieViewMapper ?? throw new ArgumentNullException(nameof(movieViewMapper));
            this.reviewViewMapper = reviewViewMapper ?? throw new ArgumentNullException(nameof(reviewViewMapper));
            this.genreViewMapper = genreViewMapper ?? throw new ArgumentNullException(nameof(genreViewMapper));
            this.actorViewMapper = actorViewMapper ?? throw new ArgumentNullException(nameof(actorViewMapper));
        }        

        [HttpGet]
        public async Task<IActionResult> Search(SearchMovieViewModel model)
        {          
            model.SearchResults = (await this.movieService.SearchMoviesContainsString(model.SearchName))
                                                    .Select(this.movieViewMapper.MapFrom)
                                                    .ToList();

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var movie = await this.movieService.GetMovieById(id);            

            if (movie == null)
            {
                return NotFound();
            }

            var movieLast5Reviews = await this.movieService.LastFiveReviewsByMovie(id);
            var movieAllReviews = await this.movieService.AllReviewsByMovie(id);

            var movieAllGenres = await this.movieService.AllGenresByMovie(id);
            var movieAllActors = await this.movieService.AllActorsByMovie(id);

            var movieViewModel = this.movieViewMapper.MapFrom(movie);

            movieViewModel.LastFiveReviewsByMovie = movieLast5Reviews.Select(this.reviewViewMapper.MapFrom).ToList();
            movieViewModel.AllReviewsByMovie = movieAllReviews.Select(this.reviewViewMapper.MapFrom).ToList();

            movieViewModel.AllGenresByMovie = movieAllGenres.Select(this.genreViewMapper.MapFrom).ToList();
            movieViewModel.AllActorsByMovie = movieAllActors.Select(this.actorViewMapper.MapFrom).ToList();

            return View(movieViewModel);
        }
        
        public async Task<IActionResult> MoviesByName(char id)
        {
            var moviesByStartingSymbol = await this.movieService.ShowMoviesStartWithSymbol(id);

            var movieViewModel = moviesByStartingSymbol.Select(this.movieViewMapper.MapFrom).ToList();            
            
            return View(movieViewModel);
        }

        public async Task<IActionResult> Index()
        {
            var movies = await this.movieService.ShowAllMoviesOrderedDescByRating();
            var movieViewModel = movies.Select(this.movieViewMapper.MapFrom).ToList();
            return View(movieViewModel);
        }               
    }
}