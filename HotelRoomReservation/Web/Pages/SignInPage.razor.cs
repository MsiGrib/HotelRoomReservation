using Microsoft.AspNetCore.Components;
using System.Xml.Linq;

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
