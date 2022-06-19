using Microsoft.EntityFrameworkCore;
using MoviePlatform.Models;
using System.Collections.ObjectModel;

namespace MoviePlatform.Service
{
    public interface IUserRepository
    {
        public IReadOnlyCollection<User> GetUsers();
        public IReadOnlyCollection<User> GetUsers(UserType userType);
        public Task<User> GetUserAsync(int idUser);
        public void AddUser(User user);
        public void UpdateUser(User user);
        public void DeleteUser(int userId);
    }
    public class UserRepository : IUserRepository
    {
        MoviePlatformDbContext Context { get; set; }
        public UserRepository(MoviePlatformDbContext context)
        {
            Context = context;
        }

        public IReadOnlyCollection<User> GetUsers()
        {
            return new ReadOnlyCollection<User>(Context.Users.ToList());
        }

        public IReadOnlyCollection<User> GetUsers(UserType userType)
        {
            return new ReadOnlyCollection<User>(Context.Users.Where(p => p.UserType == userType).ToList());
        }

        public async Task<User> GetUserAsync(int idUser)
        {
            if (!ValidateUser(idUser))
            {
                return new User();
            }

            return await Context.Users.FirstAsync(p => p.IdUser == idUser);
        }

        public void AddUser(User user)
        {
            if (Context.Users.Any(u => u.UserName == user.UserName))
            {
                Console.WriteLine("User with given username already exists");
                return;
            }

            if (user.UserType == UserType.StandardUser)
            {
                if (user.ProfileDescription is null || user.ProfileDescription == "")
                {
                    return;
                }

                Context.Users.Add(new User() { UserName = user.UserName, Password = user.Password, Email = user.Email, ProfileDescription = "Standard user profile" });
            }         
            else if(user.UserType == UserType.Moderator)
            {
                Context.Users.Add(new User() { UserName = user.UserName, Password = user.Password, Email = user.Email });
            }
            Save();
        }

        public void UpdateUser(User user)
        {
            if (!ValidateUser(user.IdUser))
            {
                return;
            }
            User newUser = Context.Users.First(p => p.IdUser == user.IdUser);
            newUser.UserName = user.UserName;
            newUser.Password = user.Password;

            if (user.UserType == UserType.StandardUser)
            {
                newUser.ProfileDescription = user.ProfileDescription;
            }
            Save();
        }

        public void DeleteUser(int userId)
        {
            if (!ValidateUser(userId))
            {
                return;
            }
            Context.Users.Remove(Context.Users.First(p => p.IdUser == userId));
            Save();
        }

        public void ChangeToModerator(int userId)
        {
            User user = Context.Users.First(p => p.IdUser == userId);
            if (!ValidateUser(user.IdUser) || user.UserType == UserType.Moderator)
            {
                return;
            }

            user.UserType = UserType.Moderator;
            user.ProfileDescription = null;
        }
        public void ChangeToStandardUser(int userId, string profileDescription)
        {
            User user = Context.Users.First(p => p.IdUser == userId);
            if (!ValidateUser(user.IdUser) || user.UserType == UserType.StandardUser || profileDescription is null || profileDescription == "")
            {
                return;
            }

            user.UserType = UserType.StandardUser;
            user.ProfileDescription = profileDescription;
        }

        public bool ValidateUser(int idUser)
        {
            if (!Context.Users.Any(p => p.IdUser == idUser))
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
