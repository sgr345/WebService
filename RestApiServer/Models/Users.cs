using System;
namespace RestApiServer.Models
{
    public class Users
    {
        public string UserName { get; set; }
        public Guid Id { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}

