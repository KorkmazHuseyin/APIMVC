using APIMVC.UI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace APIMVC.UI.ApiServices
{
    public class TokenApiService
    {
        HttpClient _client;
        public TokenApiService(HttpClient client)
        {
            _client = client;
        }

        public async Task<string> TokenAl(string kullaniciAdi, string sifre)
        {
            LoginDTO dto = new LoginDTO()
            { Password = sifre, UserName = kullaniciAdi };
            StringContent mycontent = new StringContent(JsonConvert.SerializeObject(dto));
            mycontent.Headers.ContentType = new
                System.Net.Http.Headers.MediaTypeHeaderValue("applicatirn/json");
            var apininGondermisOlduguDeger = await _client.PostAsync("api/auth/login", mycontent);
            string token = "";
            if (apininGondermisOlduguDeger.IsSuccessStatusCode)
            {
                token = await apininGondermisOlduguDeger.Content.ReadAsStringAsync();
            }

            return token;
        }





        public async Task<string> KullaniciKaydet(RegisterDTO dto)
        {
           
            StringContent mycontent = new StringContent(JsonConvert.SerializeObject(dto));
            mycontent.Headers.ContentType = new
                System.Net.Http.Headers.MediaTypeHeaderValue("applicatirn/json");
            var apininGondermisOlduguDeger = await _client.PostAsync("api/auth/register", mycontent);
        
            if (apininGondermisOlduguDeger.IsSuccessStatusCode)
            {
                //201 status kodunun gelip gelmediğini de kontrol edebilriz.
                return "Giriş yapıldı..";
            }

            return "";
        }
    }

}
