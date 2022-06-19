using Microsoft.AspNetCore.Mvc;
using MoviePlatform.Service;

namespace MoviePlatform.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IReviewRepository _reviewRepository;
  
        public MovieController(IMovieRepository movieRepository, IReviewRepository reviewRepository)
        {
            _movieRepository = movieRepository;
            _reviewRepository = reviewRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetMovies()
        {
            var movies = await _movieRepository.GetMoviesAsync();
            ViewBag.Movies = movies;
            return View(movies);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int idMovie)
        {
            var movie = await _movieRepository.GetMovieAsync(idMovie);
            movie.Reviews = await _reviewRepository.GetReviewsOfMovieAsync(idMovie);

            return View(movie);
            
        }
    }
}
