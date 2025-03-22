using IdentityMService.ModelsRR;

namespace IdentityMService.Validators
{
    public static class AuthCValidator
    {
        public static bool IsValidAuthorizationRequest(AuthorizationRequest request)
        {
            return !string.IsNullOrEmpty(request.Login) && !string.IsNullOrEmpty(request.Password);
        }

        public static bool IsValidRegistrationRequest(RegistrationRequest request)
        {
            return !string.IsNullOrEmpty(request.Login) && !string.IsNullOrEmpty(request.Password) && !string.IsNullOrEmpty(request.Email)
                && !string.IsNullOrEmpty(request.NumberPhone) && !string.IsNullOrEmpty(request.LastName) && !string.IsNullOrEmpty(request.FirstName);
        }
    }
}
