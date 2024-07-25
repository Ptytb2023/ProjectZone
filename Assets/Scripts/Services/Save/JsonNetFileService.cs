using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace Services.Save
{
    public class JsonNetFileService : IJsonFileServiceService
    {
        public async Task<TModel> LoadAsync<TModel>(string filePath)
        {
            using var reader = new StreamReader(filePath);

            string json = await reader.ReadToEndAsync();

            TModel model;

            try
            {
                model = JsonConvert.DeserializeObject<TModel>(json);
            }
            catch (JsonSerializationException)
            {
                model = default;
            }

            return model;
        }

        public async Task SaveAsync<TModel>(TModel model, string filePath)
        {
            using var writer = new StreamWriter(filePath);

            string json = JsonConvert.SerializeObject(model, Formatting.Indented);

            await writer.WriteAsync(json);
        }
    }
}
