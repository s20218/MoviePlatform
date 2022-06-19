using MoviePlatform.Models;
using System.Collections.ObjectModel;

namespace MoviePlatform.Service
{
    public interface IStarsInRepository
    {
        public IReadOnlyCollection<StarsIn> GetStarsIns();
        public IReadOnlyCollection<StarsIn> GetStarsInsForTheMovie(int idMovie);
        public IReadOnlyCollection<StarsIn> GetStarsInsOfActor(int idActor);
        public StarsIn GetStarsIn(int idActor, int idMovie);
        public void AddStarsIn(StarsIn starsIn);
        public void UpdateStarsIn(StarsIn starsIn);
        public void DeleteStarsIn(int movieId, int actorId);
        public void Save();
    }
    public class StarsInRepository : IStarsInRepository
    {
        MoviePlatformDbContext Context { get; set; }
        public StarsInRepository(MoviePlatformDbContext context)
        {
            Context = context;
        }

        public IReadOnlyCollection<StarsIn> GetStarsIns()
        {
            return new ReadOnlyCollection<StarsIn>(Context.StarsIns.ToList());
        }

        public IReadOnlyCollection<StarsIn> GetStarsInsForTheMovie(int idMovie)
        {
            return new ReadOnlyCollection<StarsIn>(Context.StarsIns.Where(s => s.IdMovie == idMovie).ToList());
        }

        public IReadOnlyCollection<StarsIn> GetStarsInsOfActor(int idActor)
        {
            return new ReadOnlyCollection<StarsIn>(Context.StarsIns.Where(s => s.IdPerson == idActor).ToList());
        }

        public StarsIn GetStarsIn(int idActor, int idMovie)
        {
            return Context.StarsIns.First(s => s.IdPerson == idActor && s.IdMovie == idMovie);
        }

        public void AddStarsIn(StarsIn starsIn)
        {
            if (Validate(starsIn.IdPerson, starsIn.IdMovie))
            {
                Console.WriteLine("Relation between this movie and actor already exists");
                return;
            }
            if (!Context.People.Any(r => r.IdPerson == starsIn.IdPerson) || !Context.Movies.Any(r => r.IdMovie == starsIn.IdMovie))
            {
                Console.WriteLine("Given movie or actor doesn't exist");
                return;
            }
            if (!Context.People.First(d => d.IdPerson == starsIn.IdPerson).Types.Contains(PersonType.Actor.ToString()))
            {
                return;
            }
            Context.StarsIns.Add(new StarsIn { IdPerson = starsIn.IdPerson, IdMovie = starsIn.IdMovie, Salary = starsIn.Salary, StartDate = starsIn.StartDate, EndDate = starsIn.EndDate });
            Save();
        }

        public void UpdateStarsIn(StarsIn starsIn)
        {
            if (!Validate(starsIn.IdPerson, starsIn.IdMovie))
            {
                return;
            }

            StarsIn newStarsIn = Context.StarsIns.First(r => r.IdMovie == starsIn.IdMovie && r.IdPerson == starsIn.IdPerson);
            newStarsIn.Salary = starsIn.Salary;
            newStarsIn.StartDate = starsIn.StartDate;
            newStarsIn.EndDate = starsIn.EndDate;
            Save();
        }

        public void DeleteStarsIn(int movieId, int actorId)
        {
            if (!Validate(actorId, movieId))
            {
                return;
            }
            Context.StarsIns.Remove(Context.StarsIns.First(s => s.IdPerson == actorId && s.IdMovie == movieId));
            Save();
        }

        public bool Validate(int idActor, int idMovie)
        {
            if (!Context.StarsIns.Any(s => s.IdPerson == idActor && s.IdMovie == idMovie))
            {
                Console.WriteLine("Given role doesn't exist");
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
