using System;

namespace Sum.Model.Auth
{
    public class AuthenticationResult
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
        public bool Success { get; set; }
        public string ReadableMessage { get; set; }
        public int UserTypeId { get; set; }
    }
}
