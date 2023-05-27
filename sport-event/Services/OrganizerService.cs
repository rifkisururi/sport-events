using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;
using sport_event.ViewModels.Auth;
using sport_event.ViewModels.Organizer;
using System.Net;
using System.Text;

namespace sport_event.Services
{
    public class OrganizerService
    {
        private readonly HttpClient _client;
        private readonly string baseUrl = "https://api-sport-events.php6-02.test.voxteneo.com/api/v1/organizers";
        public OrganizerService()
        {
            _client = new HttpClient();
        }

        public async Task<OrganizersResponseDto> getData(string? token, int page, int perPage)
        {
            var urlAction = baseUrl + "?page="+page+"&perPage="+perPage;
            Log.Information($"Start getData {baseUrl} at {DateTime.Now.Date} {DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}");

            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var responseCheck = await _client.GetAsync(urlAction);
            var responseString = await responseCheck.Content.ReadAsStringAsync();
            Log.Information($"respond api {responseString}");
            try
            {
                var data = JsonConvert.DeserializeObject<OrganizersResponseDto>(responseString);
                return data;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex.InnerException);
            }

            return null;
        }

        public async Task<OrganizerDto> createData(string? token, OrganizerDto data)
        {

            Log.Information($"Start createData at {DateTime.Now.Date} {DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}"); 
            Log.Information($"data {JsonConvert.SerializeObject(data)}"); 

            var urlAction = baseUrl ;
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var responseCheck = await _client.PostAsync(urlAction, content);
            var responseString = await responseCheck.Content.ReadAsStringAsync();
            Log.Information($"respond api {responseString}");
            try
            {
                var dataOrg = JsonConvert.DeserializeObject<OrganizerDto>(responseString);
                return dataOrg;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex.InnerException);
            }

            return null;
        }

        public async Task<bool> DeleteData(string? token, int id)
        {
            var urlAction = baseUrl + "/" + id;
            Log.Information($"Start DeleteData {urlAction} at {DateTime.Now.Date} {DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}");
            var responseCheck = await _client.DeleteAsync(urlAction);
            var statusCode = responseCheck.StatusCode;

            Log.Information($"statusCode {statusCode}");
            if (statusCode == HttpStatusCode.NoContent)
            {
                return true;
            }

            return false;
        }

        public async Task<OrganizerDto> GetDataDetail(string? token, int id)
        {

            var urlAction = baseUrl + "/" + id;
            Log.Information($"Start GetDataDetail {urlAction} at {DateTime.Now.Date} {DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}");
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var responseCheck = await _client.GetAsync(urlAction);
            var responseString = await responseCheck.Content.ReadAsStringAsync();
            Log.Information($"respond api {responseString}");

            try
            {
                var dataOrg = JsonConvert.DeserializeObject<OrganizerDto>(responseString);
                return dataOrg;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex.InnerException);
            }

            return null;
        }

        public async Task<bool> UpdateData(string? token, OrganizerDto data)
        {
            var urlAction = baseUrl+"/"+data.id;
            Log.Information($"Start UpdateData {urlAction} at {DateTime.Now.Date} {DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}");

            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var responseCheck = await _client.PutAsync(urlAction, content);
            var statusCode = responseCheck.StatusCode;
            Log.Information($"statusCode api {statusCode}");
            if (statusCode == HttpStatusCode.NoContent)
            {
                return true;
            }

            return false;
        }
    }


}
