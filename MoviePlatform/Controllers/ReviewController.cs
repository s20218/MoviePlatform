using Microsoft.AspNetCore.Mvc;
using MoviePlatform.Dtos;
using MoviePlatform.Models;
using MoviePlatform.Service;

namespace MoviePlatform.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMovieRepository _movieRepository;
        private static Movie _movie;

        public ReviewController(IReviewRepository reviewRepository, IMovieRepository movieRepository)
        {
            _reviewRepository = reviewRepository;
            _movieRepository = movieRepository;
        }

        public async Task<IActionResult> Add(string opinion, string grade)
        {
            ReviewDto review = new ReviewDto { Movie = _movie, Grade = grade, Opinion = opinion };
            bool isAdded = await _reviewRepository.AddReview(review);
            if (isAdded)
            {
                return RedirectToAction("Details", "Movie", review.Movie);
            }
            return View("Error", review);
        }

        public async Task<IActionResult> SubmitForm(int idMovie)
        {
            _movie = await _movieRepository.GetMovieAsync(idMovie);
            var review = new ReviewDto { Movie = _movie };
            return View(review);
        }
    }
}

