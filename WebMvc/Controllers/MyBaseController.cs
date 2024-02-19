using Entity.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace WebMvc.Controllers
{
    public class MyBaseController : Controller
    {
        public string BaseAdress = "";
   
        public MyBaseController()
        {
            BaseAdress = System.Environment.GetEnvironmentVariable("BaseAddress");

        }

        public HttpResponseMessage? NewhttpClinetGet(string _url)
        {
            using var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(BaseAdress);
            return httpClient.GetAsync(_url).Result;
        }

        public HttpResponseMessage? NewhttpClinetPost(string _url,object obj)
        {
            using var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(BaseAdress);

            var serializeProduct = JsonConvert.SerializeObject(obj);
            StringContent stringContent = new StringContent(serializeProduct, Encoding.UTF8, "application/json");
            return httpClient.PostAsync(_url, stringContent).Result;
        }
    }
}
