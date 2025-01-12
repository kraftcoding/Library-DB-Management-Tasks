using LibraryDBManagement.Tasks.Model;
using System;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Threading;

namespace LibraryDBManagement.Tasks.Core
{
    public class WebAPIManager
    {
        static HttpClient client;

        public WebAPIManager()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5000/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        internal static void Run()
        {
            RunAsync().GetAwaiter().GetResult();
        }

        public static async Task<User> GetUser(int id)
        {
            User Usr = await GetUserAsync(id);            
            return Usr;
        }

        public static async Task<Uri> CreateUser(User Usr)
        {
            Uri uri = await CreateUserAsync(Usr);
            return uri;
        }

        static async Task RunAsync()
        {
            //client = new HttpClient();
            //client.BaseAddress = new Uri("http://localhost:5000/");
            //client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            User Usr;

            // GET
            Usr = await GetUserAsync(3);

            // POST
            //Usr = EntityManager.GetUserObject("usr2", "usr2@email.com", "1234", "test");
            //Uri uri = await CreateUserAsync(Usr);

            //UPDATE
            //Usr = EntityManager.GetUserObject("usr2", "usr2@email.com", "1234", "test1");
            //Usr.Id = 2;
            //Usr = await UpdateUserAsync(Usr);

        }

        static async Task<User> GetUserAsync(int id)
        {
            User Usr = null;
            string path = string.Format($"/api/Users/get/{id}");
            HttpResponseMessage response = await client.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                Usr = await response.Content.ReadAsAsync<User>();
            }

            return Usr;
        }

        static async Task<Uri> CreateUserAsync(User Usr)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync($"/api/Users/create", Usr);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }

        //static async Task<User> UpdateUserAsync(User Usr)
        //{
        //    HttpResponseMessage response = await client.PutAsJsonAsync($"/api/Users/update/{Usr.Id}", Usr);
        //    response.EnsureSuccessStatusCode();

        //    // Deserialize the updated product from the response body.
        //    Usr = await response.Content.ReadAsAsync<User>();
        //    return Usr;
        //}

        //static async Task<HttpStatusCode> DeleteUserAsync(int id)
        //{
        //    User Usr = null;
        //    string path = string.Format($"/api/Users/delete/{id}");
        //    HttpResponseMessage response = await client.DeleteAsync(path);

        //    return response.StatusCode;
        //}
    }
}
