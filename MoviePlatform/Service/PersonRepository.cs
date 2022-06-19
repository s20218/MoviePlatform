using MoviePlatform.Models;
using System.Collections.ObjectModel;

namespace MoviePlatform.Service
{
    public interface IPersonRepository
    {
        public IReadOnlyCollection<Person> GetPeople();
        public IReadOnlyCollection<Person> GetPeople(PersonType personType);
        public Person GetPerson(int idPerson);
        public void AddPerson(Person person);
        public void UpdatePerson(Person person);
        public void DeletePerson(int personId);
        public void Save();
    }
    public class PersonRepository : IPersonRepository
    {
        MoviePlatformDbContext Context { get; set; }
        public PersonRepository(MoviePlatformDbContext context)
        {
            Context = context;
        }

        public IReadOnlyCollection<Person> GetPeople()
        {
            return new ReadOnlyCollection<Person>(Context.People.ToList());
        }

        public IReadOnlyCollection<Person> GetPeople(PersonType personType)
        {
            return new ReadOnlyCollection<Person>(Context.People.Where(p => p.Types.Contains(personType.ToString())).ToList());
        }

        public void AddPerson(Person person)
        {
            if ((person.Types.Contains(PersonType.Actor.ToString()) && person.DateOfBirth is null) ||
                (person.Types.Contains(PersonType.Director.ToString()) && person.HasDegree is null))
            {
                return; 
            }

            if ((person.Types.Contains(PersonType.Actor.ToString()) && person.Types.Contains(PersonType.Director.ToString())) ||
                (person.Types.Contains(PersonType.Actor.ToString()) && person.Types.Contains(PersonType.Director.ToString()) && person.Types.Contains(PersonType.Writer.ToString())))
            {
                Context.People.Add(new Person() { FirstName = person.FirstName, LastName = person.LastName, DateOfBirth = person.DateOfBirth, HasDegree = person.HasDegree });
            }
            else if (person.Types.Contains(PersonType.Actor.ToString()))
            {
                Context.People.Add(new Person() { FirstName = person.FirstName, LastName = person.LastName, DateOfBirth = person.DateOfBirth });
            }

            else if (person.Types.Contains(PersonType.Director.ToString()))
            {
                Context.People.Add(new Person() { FirstName = person.FirstName, LastName = person.LastName, HasDegree = person.HasDegree });
            }

            Save();
        }

        public void UpdatePerson(Person person)
        {
            if (!ValidatePerson(person.IdPerson))
            {
                return;
            }

            Person newPerson = Context.People.First(p => p.IdPerson == person.IdPerson);
            if (newPerson.Types.Contains(PersonType.Actor.ToString()))
            {
                newPerson.FirstName = person.FirstName;
                newPerson.LastName = person.LastName;
                newPerson.DateOfBirth = person.DateOfBirth;
            }
            Save();
        }

        public Person GetPerson(int idPerson)
        {
            if (!ValidatePerson(idPerson))
            {
                return new Person();
            }

            return Context.People.First(p => p.IdPerson == idPerson);
        }

        public void DeletePerson(int personId)
        {
            if (!ValidatePerson(personId))
            {
                return;
            }

            Person newPerson = Context.People.First(p => p.IdPerson == personId);

            Context.People.Remove(newPerson);
            Save();
        }

        public bool ValidatePerson(int idPerson)
        {
            if (!Context.People.Any(p => p.IdPerson == idPerson))
            {
                Console.WriteLine("This person doesn't exist in the database");
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