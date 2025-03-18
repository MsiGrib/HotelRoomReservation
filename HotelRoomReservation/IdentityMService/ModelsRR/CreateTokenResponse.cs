namespace IdentityMService.ModelsRR
{
    public class CreateTokenResponse
    {
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
