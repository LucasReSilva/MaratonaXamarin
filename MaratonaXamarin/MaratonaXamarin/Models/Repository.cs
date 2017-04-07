using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaratonaXamarin.Models
{
    class Repository
    {
        public async Task<List<Cat>> GetCatsAPI()
        {
            List<Cat> Cats;
           /* var URLWebAPI = "http://demos.ticapacitacion.com/cats"; */
            var URLWebAPI = "";
            using (var Client = new System.Net.Http.HttpClient())
            {
                var JSON = await Client.GetStringAsync(URLWebAPI);
                Cats = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cat>>(JSON);
            }
            return Cats;
        }

        public async Task<List<Cat>> GetCatsAzure()
        {
            var Service = new Services.AzureService<Cat>();
            var Items = await Service.GetTable();
            return Items.ToList();
        }

    }


}
