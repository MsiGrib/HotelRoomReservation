using Microsoft.AspNetCore.Components;

namespace Web.Pages
{
    public partial class LogInPage
    {
        [Inject]
        public required NavigationManager Navigation { get; set; }
        public string TextValue { get; set; }

        private void OnLogIn()
        {
            Navigation.NavigateTo("/");//Переход в личный кабинет
        }

        private void OnSignIn()
        {
            Navigation.NavigateTo("/SignIn");
        }
    }
}
