using Microsoft.EntityFrameworkCore;
using MoviePlatform.Dtos;
using MoviePlatform.Models;
using System.Collections.ObjectModel;

namespace MoviePlatform.Service
{
    public interface IReviewRepository
    {
        public IReadOnlyCollection<Review> GetReviews();
        public IReadOnlyCollection<Review> GetReviewsOfUser(int idUser);
        public Task<List<Review>> GetReviewsOfMovieAsync(int idMovie);
        public Review GetReview(int idReview);
        public Task<bool> AddReview(ReviewDto review);
        public void UpdateReview(Review review);
        public void DeleteReview(int reviewId);
        public void Save();
    }
    public class ReviewRepository : IReviewRepository
    {
        IUserRepository _userRepository;
        MoviePlatformDbContext Context { get; set; }
        public ReviewRepository(MoviePlatformDbContext context, IUserRepository userRepository)
        {
            Context = context;
            _userRepository = userRepository;
        }

        public IReadOnlyCollection<Review> GetReviews()
        {
            return new ReadOnlyCollection<Review>(Context.Reviews.ToList());
        }

        public IReadOnlyCollection<Review> GetReviewsOfUser(int idUser)
        {
            return new ReadOnlyCollection<Review>(Context.Reviews.Where(r => r.IdUser == idUser).ToList());
        }
        public async Task<List<Review>> GetReviewsOfMovieAsync(int idMovie)
        {
            var Reviews = await Context.Reviews.Where(r => r.IdMovie == idMovie).ToListAsync();
            foreach(var review in Reviews)
            {
                review.User = await _userRepository.GetUserAsync(review.IdUser);
            }

            return Reviews;
        }

        public Review GetReview(int idReview)
        {
            if (!Validate(idReview))
            {
                return new Review();
            }
            return Context.Reviews.First(x => x.IdReview == idReview);
        }

        public async Task<bool> AddReview(ReviewDto review)
        {
            if (!Context.Users.Any(r => r.IdUser == 1) || !Context.Movies.Any(m => m.IdMovie == review.Movie.IdMovie))
            {
                Console.WriteLine("Given user or movie doesn't exist");
                return false;
            }

            using (var dbContextTransaction = Context.Database.BeginTransaction())
            {
                try
                {
                    Grade myEnum = (Grade)Enum.Parse(typeof(Grade), review.Grade);
                    await Context.Reviews.AddAsync(new Review { Text = review.Opinion, Grade = myEnum, IdUser = 1, IdMovie = review.Movie.IdMovie, CreationDate = DateTime.UtcNow });
                    Save();

                    dbContextTransaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                }
            }

            return false;
        }

        public void UpdateReview(Review review)
        {
            if (!Validate(review.IdReview))
            {
                return;
            }
            Review newReview = Context.Reviews.First(r => r.IdReview == review.IdReview);

            newReview.Text = review.Text;
            newReview.Grade = review.Grade;
            newReview.CreationDate = review.CreationDate;
            Save();
        }

        public void DeleteReview(int reviewId)
        {
            if (!Validate(reviewId))
            {
                return;
            }
            Context.Reviews.Remove(Context.Reviews.First(r => r.IdReview == reviewId));
            Save();
        }

        public bool Validate(int idReview)
        {
            if (!Context.Reviews.Any(x => x.IdReview == idReview))
            {
                Console.WriteLine("Given review doesn't exist");
                return false;
            }
            return true;
        }

        public void Save()
        {
            Context.SaveChanges();
        }
    }
}
