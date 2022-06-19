using MoviePlatform.Models;
using System.Collections.ObjectModel;

namespace MoviePlatform.Service
{
    public interface IFavoriteRepository
    {
        public IReadOnlyCollection<Favorite> GetFavorites();
        public IReadOnlyCollection<Favorite> GetUserFavorites(int idUser);
        public void AddFavorite(Favorite favorite);
        public void DeleteFavorite(int userId, int movieId);
        public void Save();
    }
    public class FavoriteRepository : IFavoriteRepository
    {
        MoviePlatformDbContext Context { get; set; }
        public FavoriteRepository(MoviePlatformDbContext context)
        {
            Context = context;
        }

        public IReadOnlyCollection<Favorite> GetFavorites()
        {
            return new ReadOnlyCollection<Favorite>(Context.Favorites.ToList());
        }

        public IReadOnlyCollection<Favorite> GetUserFavorites(int idUser)
        {
            return new ReadOnlyCollection<Favorite>(Context.Favorites.Where(f => f.IdUser == idUser).ToList());
        }

        public void AddFavorite(Favorite favorite)
        {
            if (!Context.Users.Any(r => r.IdUser == favorite.IdUser || !Context.Movies.Any(m => m.IdMovie == favorite.IdMovie)))
            {
                Console.WriteLine("Given user or movie doesn't exist");
                return;
            }

            if (favorite.User.UserType != UserType.StandardUser)
            {
                Console.WriteLine("Only standard users can add movie to favorites");
                return;
            }
            Context.Favorites.Add(new Favorite { IdMovie = favorite.IdMovie, IdUser = favorite.IdUser });
            Save();
        }

        public void DeleteFavorite(int userId, int movieId)
        {
            if (!Validate(userId, movieId))
            {
                return;
            }
            Context.Favorites.Remove(Context.Favorites.First(f => f.IdUser == userId && f.IdMovie == movieId));
            Save();
        }

        public bool Validate(int idUser, int idMovie)
        {
            if (!Context.Favorites.Any(x => x.IdUser == idUser && x.IdMovie == idMovie))
            {
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