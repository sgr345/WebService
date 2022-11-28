using System;
namespace RestApiServer.Utilities.Secure
{
    public class PasswordHashInfo
    {
        public string GUIDSalt { get; set; }
        public string RNGSalt { get; set; }
        public string PasswordHash { get; set; }
    }
}

