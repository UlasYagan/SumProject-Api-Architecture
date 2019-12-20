using System.Collections.Generic;

namespace Sum.Model.Auth
{
    public class FailedAuthResponse : BaseAuthResponse
    {
        public FailedAuthResponse()
        {
            ErrorMessages = new List<string>();
            ErrorCode = new List<string>();
        }

        public ICollection<string> ErrorMessages { get; set; }
        public ICollection<string> ErrorCode { get; set; }
        public bool Success { get; set; } = false;
    }
}