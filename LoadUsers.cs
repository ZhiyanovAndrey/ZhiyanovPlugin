using PhoneApp.Domain.DTO;
using PhoneApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ZhiyanovPlugin
{

    public class LoadUsers : IPluggable

    {
        const string url = "https://dummyjson.com/users";
        public IEnumerable<DataTransferObject> Run(IEnumerable<DataTransferObject> args)
        {
            var client = new HttpClient();
            var task = client.GetStringAsync(url);
            task.Wait();
            var res = task.Result;
            var data = JsonConvert.DeserializeObject<Data>(res);

            var datatransfer = new EmployeesDTO();
            var list = new List<EmployeesDTO>();

            if (data != null)
            {
                foreach (var item in data.Users)
                {

                    var dto = new EmployeesDTO();

                    dto.Name = $"{item.FirstName} {item.LastName}";

                    dto.AddPhone(item.Phone);


                    list.Add(dto);

                }


            }



            return list;

        }

        public class Data
        {
            [JsonProperty("users")]
            public DataItem[] Users { get; set; }
        }

        public class DataItem
        {
            [JsonProperty("id")]
            public int Id { get; set; }
            [JsonProperty("firstName")]
            public string FirstName { get; set; }
            [JsonProperty("lastName")]
            public string LastName { get; set; }
            [JsonProperty("phone")]
            public string Phone { get; set; }
        }
    }
}
