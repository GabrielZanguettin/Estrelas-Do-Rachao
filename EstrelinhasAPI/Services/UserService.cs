using EstrelinhasAPI.Entidades;
using NHibernate;
using NHibernate.Linq;

namespace EstrelinhasAPI.Services
{
    public class UserService
    {
        private readonly ISessionFactory sessionFactory;
        public UserService (ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }

        public async Task<List<User>> GetUsers()
        {
            using var session = sessionFactory.OpenSession();
            var users = await session.Query<User>().ToListAsync();
            return users ?? new List<User>();
        }

        public async Task<User> CreateUser(User user)
        {
            using var session = sessionFactory.OpenSession();
            using var transaction = session.BeginTransaction();

            await session.SaveAsync(user);
            await transaction.CommitAsync();

            return user;
        }
        public async Task<User?> DeleteUser(int id)
        {
            using var session = sessionFactory.OpenSession();
            using var transaction = session.BeginTransaction();

            var user = await session.GetAsync<User>(id);
            if (user == null) return null;

            await session.DeleteAsync(user);
            await transaction.CommitAsync();

            return user;
        }

        public async Task<User?> AddStar(int userId)
        {
            using var session = sessionFactory.OpenSession();
            using var transaction = session.BeginTransaction();

            var user = await session.GetAsync<User>(userId);

            if (user == null)
                return null;

            if (user.Stars >= 9)
            {
                user.Stars = 0;
                user.PurpleStars += 1;
            }
            else
            {
                user.Stars += 1;
            }

            await session.SaveOrUpdateAsync(user);
            await transaction.CommitAsync();

            return user;
        }
    }
}
