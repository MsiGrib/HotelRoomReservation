using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Web.Pages
{
    public partial class MapPage
    {
        [Inject]
        public required IJSRuntime JSRuntime { get; set; }
        [Inject]
        public required BasicConfiguration BasicConfiguration { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                double latitude = Convert.ToDouble(BasicConfiguration.LatitudeHotel); 
                double longitude = Convert.ToDouble(BasicConfiguration.LongitudeHotel);

                await JSRuntime.InvokeVoidAsync("initializeMap", latitude, longitude);
            }
        }
    }
}
