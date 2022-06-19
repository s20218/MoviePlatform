using Microsoft.EntityFrameworkCore;
using MoviePlatform.Models;
using System.Collections.ObjectModel;

namespace MoviePlatform.Service
{
    public interface IMovieRepository
    {
        public Task<List<Movie>> GetMoviesAsync();
        public IReadOnlyCollection<Movie> GetMoviesForDirector(int idDirector);
        public IReadOnlyCollection<Movie> GetMoviesForWriter(int idWriter);
        public Task<Movie> GetMovieAsync(int idMovie);
        public void AddMovie(Movie movie);
        public void UpdateMovie(Movie movie);
        public void DeleteMovie(int personId);
        public void Save();
    }
    public class MovieRepository : IMovieRepository
    {
        MoviePlatformDbContext Context { get; set; }
        public MovieRepository(MoviePlatformDbContext context)
        {
            Context = context;
        }

        public async Task<List<Movie>> GetMoviesAsync()
        {
            return await Context.Movies.ToListAsync();
        }

        public IReadOnlyCollection<Movie> GetMoviesForDirector(int idDirector)
        {
            return new ReadOnlyCollection<Movie>(Context.Movies.Where(m => m.IdDirector == idDirector).ToList());
        }

        public IReadOnlyCollection<Movie> GetMoviesForWriter(int idWriter)
        {
            return new ReadOnlyCollection<Movie>(Context.Movies.Where(m => m.IdWriter == idWriter).ToList());
        }

        public async Task<Movie> GetMovieAsync(int idMovie)
        {
            if (!Validate(idMovie))
            {
                return new Movie();
            }
            return await Context.Movies.FirstAsync(x => x.IdMovie == idMovie);
        }

        public void AddMovie(Movie movie)
        {
            if (!Context.People.Any(d => d.IdPerson == movie.IdDirector))
            {
                Console.WriteLine("Given director doesn't exist");
                return;
            }

            if (!Context.People.First(d => d.IdPerson == movie.IdDirector).Types.Contains(PersonType.Director.ToString())
                || !Context.People.First(d => d.IdPerson == movie.IdWriter).Types.Contains(PersonType.Writer.ToString()))
            {
                return;
            }
            Context.Movies.Add(new Movie { Title = movie.Title, IdDirector = movie.IdDirector, Description = movie.Description, Roles = movie.Roles, IdWriter = movie.IdWriter });
            Save();
        }

        public void UpdateMovie(Movie movie)
        {
            if (!Validate(movie.IdMovie))
            {
                return;
            }
            Movie newMovie = Context.Movies.First(m => m.IdMovie == movie.IdMovie);

            newMovie.Title = movie.Title;
            newMovie.Description = movie.Description;
            newMovie.Roles = movie.Roles;

            Save();

        }

        public void DeleteMovie(int movieId)
        {
            if (!Validate(movieId))
            {
                return;
            }

            Context.Movies.Remove(Context.Movies.First(m => m.IdMovie == movieId));
            Save();
        }

        public bool Validate(int idMovie)
        {
            if (!Context.Movies.Any(x => x.IdMovie == idMovie))
            {
                Console.WriteLine("Given movie doesn't exist");
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
