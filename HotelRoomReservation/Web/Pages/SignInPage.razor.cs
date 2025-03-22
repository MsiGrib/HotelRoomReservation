using DataModel.ModelsResponse;
using IdentityMService.ModelsRR;
using Microsoft.AspNetCore.Components;
using System.Xml.Linq;

namespace Web.Pages
{
    public partial class SignInPage
    {
        [Inject]
        public required NavigationManager Navigation { get; set; }
        [Inject]
        public required UniversalApiManager UniversalApiManager { get; set; }
        [Inject]
        public required BasicConfiguration BasicConfiguration { get; set; }

        private string _login = string.Empty;
        private string _password = string.Empty;
        private string _repeatPassword = string.Empty;
        private string _email = string.Empty;
        private string _numberPhone = string.Empty;
        private string _lastName = string.Empty;
        private string _firstName = string.Empty;
        private string _birthday = string.Empty;

        private void OnBack()
        {
            Navigation.NavigateTo("/LogIn");
        }

        private async Task OnSignIn()
        {
            if (_password != _repeatPassword)
            {
                // пароли не равны
            }

            var request = new RegistrationRequest
            {
                Login = _login,
                Password = _password,
                Email = _email,
                NumberPhone = _numberPhone,
                LastName = _lastName,
                FirstName = _firstName,
                Birthday = _birthday
            };
            string url = $"{BasicConfiguration.IdentityApiUrl}api/Auth/Registration​";
            try
            {
                var result = await UniversalApiManager.PostAsync<RegistrationRequest, BaseResponse>(BasicConfiguration.IdentityApiName, url, request);
            }
            catch (Exception ex)
            {
                var qwe = ex;
                throw;
            }

        }

        //private async Task LoadData()
        //{
        //    // Выполняем GET запрос к первому API
        //    apiOneData = await ApiManager.GetAsync<MyData>("ApiOne", "api/data");
        //}

        //private async Task CreateData()
        //{
        //    // Выполняем POST запрос ко второму API
        //    var requestData = new PostRequest { RequestProperty = "Test" };
        //    postResponse = await ApiManager.PostAsync<PostRequest, PostResponse>("ApiTwo", "api/create", requestData);
        //}
    }
}
