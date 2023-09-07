using APIMVC.UI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace APIMVC.UI.ApiServices
{
    public class KargoApiService
    {
        HttpClient _client;
        public KargoApiService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<ShipperDTO>> KargolariListele()
        {
            List<ShipperDTO> shipperDTOs = null;
            var donenCevap = await _client.GetAsync("api/Kargo");
            if (donenCevap.IsSuccessStatusCode)
            {
                shipperDTOs = JsonConvert.DeserializeObject<List<ShipperDTO>>(await donenCevap.Content.ReadAsStringAsync());
            }
            return shipperDTOs;
        }

        public async Task<string> ViewToAddShip(ShipperDTO dto)
        {
            var eklenecekIcerik = new StringContent(JsonConvert.SerializeObject(dto));
            eklenecekIcerik.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            string donenVeri = null;
            try
            {
             var donenPostDegeri=   await _client.PostAsync("api/addShip", eklenecekIcerik);
                if (donenPostDegeri.IsSuccessStatusCode)
                {
                    donenVeri=await donenPostDegeri.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                donenVeri = "bir hta oluştu.";
            }
            return donenVeri;
        }
    }
}
