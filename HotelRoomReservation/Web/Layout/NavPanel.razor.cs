using Microsoft.AspNetCore.Components;

namespace Web.Layout
{
    public partial class NavPanel
    {
        [Inject]
        public required NavigationManager Navigation { get; set; }

        [Parameter]
        public bool IsOpen { get; set; }

        [Parameter]
        public EventCallback<bool> IsOpenChanged { get; set; }

        private void NavigationToHome()
        {
            Navigation.NavigateTo("/");
        }

        private void NavigationToMap()
        {
            Navigation.NavigateTo("/Map");
        }

        private void NavigationToLogIn()
        {
            Navigation.NavigateTo("/LogIn");
        }

        private void NavigationToProfile()
        {
            Navigation.NavigateTo("/Profile");
        }
    }
}
