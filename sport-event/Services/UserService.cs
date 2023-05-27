using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using sport_event.ViewModels;
using sport_event.ViewModels.Auth;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Serilog;
namespace sport_event.Services
{
    public class UserService
    {
        private readonly HttpClient _client;
        private readonly string baseUrl = "https://api-sport-events.php6-02.test.voxteneo.com/api/v1/users";
        public UserService() {
            _client = new HttpClient();
        }
        public async Task<CallBackLoginModel> Login(LoginModel loginData)
        {
            Log.Information($"Start Login with {loginData.email} at {DateTime.Now.Date} {DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}");

            var urlAction = baseUrl + "/login";

            var content = new StringContent(JsonConvert.SerializeObject(loginData), Encoding.UTF8, "application/json");
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
            var responseCheck = await _client.PostAsync(urlAction, content);
            var responseStringLogin = await responseCheck.Content.ReadAsStringAsync();
            Log.Information($"respond api {responseStringLogin}");

            try
            {
                var user = JsonConvert.DeserializeObject<CallBackLoginModel>(responseStringLogin);
                return user;
            }
            catch(Exception ex)
            {
                // log error
                Log.Error(ex.Message, ex.InnerException);
            }

            return null;
        }

        public static bool IsValidEmail(string email)
        {
            // Define the regular expression pattern for email validation
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

            // Check if the email matches the pattern
            Regex regex = new Regex(pattern);
            return regex.IsMatch(email);
        }
        public async Task<UserModel> getDetailUser(int? id, string? token)
        {
            Log.Information($"Start cek detailUser with id {id} at {DateTime.Now.Date} {DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}");

            var urlAction = baseUrl +"/"+id;
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer "+ token);
            var responseCheck = await _client.GetAsync(urlAction);
            var responseString = await responseCheck.Content.ReadAsStringAsync();
            Log.Information($"respond api {responseCheck}");
            try
            {
                var user = JsonConvert.DeserializeObject<UserModel>(responseString);

                return user;
            }
            catch (Exception ex)
            {
                // log error
                Log.Error(ex.Message, ex.InnerException);
            }

            return null;
        }

        public async Task<bool> UpdateDetailUser(string? token, UserModel data)
        {
            Log.Information($"Start update User with id {data.id} at {DateTime.Now.Date} {DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}");

            var urlAction = baseUrl + "/" + data.id;
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
            Log.Information($"data {JsonConvert.SerializeObject(data)}");

            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var responseCheck = await _client.PutAsync(urlAction, content);
            var statusCode = responseCheck.StatusCode;
            Log.Information($"statusCode {statusCode}");

            if (statusCode == HttpStatusCode.NoContent)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> UpdatePasswordUser(string? token, UserModel data)
        {
            Log.Information($"Start UpdatePasswordUser User with id {data.id} at {DateTime.Now.Date} {DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}");

            var urlAction = baseUrl + "/" + data.id+"/password";
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            _client.DefaultRequestHeaders.Add("Accept", "application/json");

            Log.Information($"data {JsonConvert.SerializeObject(data)}");
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var responseCheck = await _client.PutAsync(urlAction, content);
            var statusCode = responseCheck.StatusCode;
            Log.Information($"statusCode {statusCode}");
            if (statusCode == HttpStatusCode.NoContent)
            {
                return true;
            }

            return false;
        }

        public async Task<CallBackLoginModel> createUser(UserModel data)
        {
            Log.Information($"Start createUser at {DateTime.Now.Date} {DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}");

            var urlAction = baseUrl;

            Log.Information($"data {JsonConvert.SerializeObject(data)}");
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
            var responseCheck = await _client.PostAsync(urlAction, content);
             
            var responseString = await responseCheck.Content.ReadAsStringAsync();
            Log.Information($"respond api {responseCheck}");
            try
            {
                var user = JsonConvert.DeserializeObject<CallBackLoginModel>(responseString);
                return user;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex.InnerException);
            }

            return null;
        }


        public async Task<bool> deletelUser(string? token, int? id)
        {
            Log.Information($"Start deletelUser with id {id} at {DateTime.Now.Date} {DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}");

            var urlAction = baseUrl + "/" + id;

            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
            var responseCheck = await _client.DeleteAsync(urlAction);
            var statusCode = responseCheck.StatusCode;
            Log.Information($"statusCode {statusCode}");

            if (statusCode == HttpStatusCode.NoContent)
            {
                return true;
            }

            return false;
        }
    }
}
