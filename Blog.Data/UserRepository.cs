using Blog.Model;

namespace Blog.Data
{
    public interface IUserRepository : IEntityBaseRepository<User>
    {
        bool IsEmailUniq(string email);
        bool IsUserNameUniq(string userName);
    }

    public class UserRepository : EntityBaseRepository<User>, IUserRepository
    {
        public UserRepository(BlogContext context) : base(context)
        {
        }

        public bool IsEmailUniq(string email)
        {
            return GetSingle(u => u.Email == email) == null;
        }

        public bool IsUserNameUniq(string userName)
        {
            return GetSingle(u => u.UserName == userName) == null;
        }
    }
}
