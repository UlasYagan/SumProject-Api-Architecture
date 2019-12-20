namespace Sum.Model.Auth
{
    public class SuccessAuthResponse : BaseAuthResponse
    {
        public string Token { get; set; }
        public bool Success { get; set; }
    }
}