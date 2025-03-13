using Microsoft.AspNetCore.Components;

namespace Web.Layout
{
    public partial class MainLayout : LayoutComponentBase
    {
        [Inject] 
        public required NavigationManager Navigation { get; set; }

        protected override void OnInitialized()
        {
            Navigation.NavigateTo("/LogIn");
        }
    }
}
