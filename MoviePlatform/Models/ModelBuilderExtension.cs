using Microsoft.EntityFrameworkCore;

namespace MoviePlatform.Models
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder mb)
        {
            //User
            mb.Entity<User>()
                .HasData(
                   new User { IdUser = 1, UserName = "jkowal", Email = "jkowal@gmail.com", Password = "12345", UserType = UserType.StandardUser, ProfileDescription = "Standard user profile" },
                   new User { IdUser = 2, UserName = "ann", Email = "ann@gmail.com", Password = "12345", UserType = UserType.StandardUser, ProfileDescription = "Standard user profile" },
                   new User { IdUser = 3, UserName = "stom", Email = "stom@gmail.com", Password = "12345", UserType = UserType.StandardUser, ProfileDescription = "Standard user profile" },
                   new User { IdUser = 4, UserName = "joe2199", Email = "joe2199@gmail.com", Password = "12345", UserType = UserType.StandardUser, ProfileDescription = "Standard user profile" },
                   new User { IdUser = 5, UserName = "kkarpin", Email = "kkarpin@gmail.com", Password = "12345", UserType = UserType.StandardUser, ProfileDescription = "Standard user profile" },
                   new User { IdUser = 6, UserName = "jdoe", Email = "jdoe@gmail.com", Password = "12345", UserType = UserType.Moderator }
            );

            //Person
            string[] roles = { PersonType.Writer.ToString(), PersonType.Actor.ToString() };
            string[] roles2 = { PersonType.Director.ToString() };
            string[] roles3 = { PersonType.Writer.ToString(), PersonType.Actor.ToString(), PersonType.Director.ToString() };
            string[] roles4 = { PersonType.Actor.ToString() };
            mb.Entity<Person>()
                .HasData(
                    new Person { IdPerson = 1, FirstName = "Tom", LastName = "Smith", FullName = "Tom Smith", DateOfBirth = new DateTime(1990, 5, 5), Types = roles },
                    new Person { IdPerson = 2, FirstName = "John", LastName = "Thompson", FullName = "John Thompson", HasDegree = true, Types = roles2 },
                    new Person { IdPerson = 3, FirstName = "Anne", LastName = "Reddle", FullName = "Anne Reddle", DateOfBirth = new DateTime(1997, 3, 4), HasDegree = false, Types = roles3 },
                    new Person { IdPerson = 4, FirstName = "Brue", LastName = "Kaminski", FullName = "Brue Kaminski", DateOfBirth = new DateTime(1975, 5, 5), Types = roles4 }
            );

            string[] genres = { Movie.Genre.Drama.ToString() };
            string[] genres2 = { Movie.Genre.Action.ToString(), Movie.Genre.Fantasy.ToString() };
            string[] genres3 = { Movie.Genre.Drama.ToString() };
            //Movie
            mb.Entity<Movie>()
                .HasData(
                   new Movie { IdMovie = 1, ImageUrl = "C:\\Images\\drStrange.jpg", Title = "Titanic", IdDirector = 3, Description = "The movie is about the 1912 sinking of the RMS Titanic.", IdWriter = 3, Genres = genres  },
                   new Movie { IdMovie = 2, ImageUrl = "C:\\Images\\Titanic.jpg", Title = "Doctor Strange", IdDirector = 2, Description = "Doctor Strange is a 2016 American superhero film based on the Marvel Comics character of the same name.", IdWriter = 1, Genres = genres2 },
                   new Movie { IdMovie = 3, ImageUrl = "C:\\Images\\Hachi.jpg", Title = "Hachi", IdDirector = 3, Description = " The original film told the true story of the Akita dog named Hachiko who lived in Japan in the 1920s. ", IdWriter = 3, Genres = genres3  }
            );

            //Stars ins
            mb.Entity<StarsIn>()
                .HasData(
                   new StarsIn { IdMovie = 2, IdPerson = 1, Salary = 2500, StartDate = new DateTime(2021, 3, 4), EndDate = new DateTime(2022, 3, 4) },
                   new StarsIn { IdMovie = 2, IdPerson = 3, Salary = 2500, StartDate = new DateTime(2021, 3, 4), EndDate = new DateTime(2022, 3, 4) },
                   new StarsIn { IdMovie = 2, IdPerson = 4, Salary = 2500, StartDate = new DateTime(2021, 3, 4), EndDate = new DateTime(2022, 3, 4) }
            );

            mb.Entity<Review>()
              .HasData(
                 new Review { IdReview = 1, IdMovie = 2, IdUser = 1, Grade = Grade.excellent, Text = "Very interesting movie.", CreationDate = DateTime.UtcNow},
                 new Review { IdReview = 2, IdMovie = 2, IdUser = 2, Grade = Grade.okay, Text = "A bit boring.", CreationDate = DateTime.UtcNow },
                 new Review { IdReview = 3, IdMovie = 2, IdUser = 3, Grade = Grade.excellent, Text = "Impressive.", CreationDate = DateTime.UtcNow },
                 new Review { IdReview = 4, IdMovie = 2, IdUser = 4, Grade = Grade.good, Text = "Nice graphics.", CreationDate = DateTime.UtcNow }
          );
        }
    }
}
