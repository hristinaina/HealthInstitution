using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.Models.Services
{
    class FileService
    {
        //public static string _roomsFilename = "../../Data/rooms.json";
        //public static string _equipmentFilename = "../../Data/equipment.json";
        //public static string _ingredientsFilename = "../../Data/ingredients.json";

        public static void Serialize<T>(string _filename, List<T> items)
        {
            using (StreamWriter writer = new StreamWriter(_filename))
            {
                writer.Write(JsonConvert.SerializeObject(items, Formatting.Indented));
            }
        }
        public static List<T> Deserialize<T>(string _filename)
        {
            using (StreamReader reader = new StreamReader(_filename))
            {
                string json = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<List<T>>(json);
            }
        }
    }
}
