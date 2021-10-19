using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DonorCentar.Model;
using Flurl.Http;
using Xamarin.Forms;

namespace DonorCentar.Mobile
{
    public class APIService
    {
        public static string Username { get; set; }
        public static string Password { get; set; }
        public static Model.Korisnik Korisnik { get; set; }

        private readonly string _route;

        public static string ApiUrl = "http://localhost:56769";



        public APIService(string route)
        {
            _route = route;
        }

        public async Task<T> Get<T>(object search, string action = null)
        {
            var url = $"{ApiUrl}/{_route}";
            if (action != null)
            {
                url += "/";
                url += action;
            }

            try
            {
                if (search != null)
                {
                    url += "?";
                    url += await search.ToQueryString();
                }

                return await url.WithBasicAuth(Username, Password).GetJsonAsync<T>();
            }
            catch (FlurlHttpException ex)
            {
                if (ex.Call.HttpResponseMessage.StatusCode== System.Net.HttpStatusCode.Unauthorized)
                {
                    //MessageBox.Show("Niste authentificirani");
                    await Application.Current.MainPage.DisplayAlert("Greška", "Niste autentificirani", "OK");
                }
                throw;
            }
        }

        public async Task<T> GetById<T>(object id, string action= null)
        {
            string url;
            if(action==null)
             url = $"{ApiUrl}/{_route}/{id}";

            else
                url = $"{ApiUrl}/{_route}/{action}/{id}";

            return await url.WithBasicAuth(Username, Password).GetJsonAsync<T>();
        }

        public async Task<T> Insert<T>(object request)
        {
            var url = $"{ApiUrl}/{_route}";

            try
            {
                return await url.WithBasicAuth(Username, Password).PostJsonAsync(request).ReceiveJson<T>();
            }
            catch (FlurlHttpException ex)
            {
                var errors = await ex.GetResponseJsonAsync<Dictionary<string, string[]>>();

                var stringBuilder = new StringBuilder();
                foreach (var error in errors)
                {
                    stringBuilder.AppendLine($"{error.Key}, ${string.Join(",", error.Value)}");
                }

                await Application.Current.MainPage.DisplayAlert("Greška", stringBuilder.ToString(), "OK");
                return default(T);
            }

        }

        public async Task<T> Update<T>(int id, object request, string action = "")
        {
            try
            {
                var url = $"{ApiUrl}/{_route}";

                if (action != "")
                    url += "/" + action;

                url += "/" + id;

                return await url.WithBasicAuth(Username, Password).PutJsonAsync(request).ReceiveJson<T>();
            }
            catch (FlurlHttpException ex)
            {
                var errors = await ex.GetResponseJsonAsync<Dictionary<string, string[]>>();

                var stringBuilder = new StringBuilder();
                foreach (var error in errors)
                {
                    stringBuilder.AppendLine($"{error.Key}, ${string.Join(",", error.Value)}");
                }

                await Application.Current.MainPage.DisplayAlert("Greška", stringBuilder.ToString(), "OK");
                return default(T);
            }

        }

        public async Task<T> Delete<T>(object id)
        {
            var url = $"{ApiUrl}/{_route}/{id}";

            return await url.WithBasicAuth(Username, Password).DeleteAsync().ReceiveJson<T>();
        }
    }
}