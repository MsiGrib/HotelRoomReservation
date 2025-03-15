using Microsoft.AspNetCore.Components;

namespace Web.Pages
{
    public partial class SignInPage
    {
        [Inject]
        public required NavigationManager Navigation { get; set; }
        public string TextValue { get; set; }
        public string PhoneNumber { get; set; }
        public string BirthDate { get; set; }

        private void OnSignIn()
        {
            Navigation.NavigateTo("/");//Переход в личный кабинет
        }

        private void OnBack()
        {
            Navigation.NavigateTo("/LogIn");
        }
    }
}
