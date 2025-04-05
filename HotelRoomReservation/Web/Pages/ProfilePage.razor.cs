using DataModel.DataBase;
using DataModel.ModelsResponse;
using IdentityMService.ModelsRR;
using Microsoft.AspNetCore.Components;

namespace Web.Pages
{
    public partial class ProfilePage
    {
        [Inject] public required UniversalApiManager UniversalApiManager { get; set; }
        [Inject] public required BasicConfiguration BasicConfiguration { get; set; }
        [Inject] public required UserStorage UserStorage { get; set; }

        // Добавить проверку на наличие токена + на то жив токен или ент если нет то выход из акка а еслипочти умер токен то нужно его обновить 
        // Нужно чистить локал сторедж
        protected override async Task OnInitializedAsync()
        {
            string url = $"{BasicConfiguration.IdentityApiUrl}api/Profile/ProfileUser";
            string token = await UserStorage.GetToken();
            var response = await UniversalApiManager.GetAsync<ApiResponse<UserProfileDTO>>(BasicConfiguration.IdentityApiName, url, token);


        }
    }
}
