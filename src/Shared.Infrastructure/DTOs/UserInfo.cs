namespace Shared.Infrastructure.DTOs
{
    public interface IUserInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
    }

    public class UserInfo : IUserInfo
    {
        public UserInfo()
        {

        }

        public UserInfo(int id, string name, string email, int roleId)
        {
            Id = id;
            Name = name;
            Email = email;
            RoleId = roleId;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
    }
}
