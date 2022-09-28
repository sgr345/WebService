using System;
namespace RestApiServer.Utilities.Secure
{
    public interface IPasswordHasher
    {
        string GetGUIDSalt();
        string GetRngSalt();
        string GetPasswordHasher(string userId, string password, string guiSalt, string rngSalt);
        bool CheckThePasswordInfo(string userId, string password, string guiSalt, string rngSalt, string passwordHash);
        PasswordHashInfo GetPasswordInfo(string userId, string password);
    }
}

