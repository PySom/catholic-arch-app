using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace LagosArch.Services
{
    public class ModelManager<T> : IManager<T>
    {
        public async Task<IEnumerable<T>> GetItemsAsync(string url, bool forceRefresh = false)
        {
            string BaseUrl = AppConstant.BaseUrl + url;
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                throw new NotImplementedException();
            }
            using (HttpClient _client = new HttpClient())
            {
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string response = await _client.GetStringAsync(BaseUrl);
                if (response != null)
                {
                    try
                    {
                        return JsonConvert.DeserializeObject<ICollection<T>>(response);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }

                }
            }

            return new List<T>();


        }
    }
}
