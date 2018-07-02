using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Newtonsoft.Json.Linq;

namespace ClassLibrary1
{
    public class Plate
    {
        public String plate;

        public Plate() { }
        private static readonly HttpClient client = new HttpClient();

        public String CheckPlate()
        {
            Task<string> recognizeTask = Task.Run(() => new Plate().ConsumePlateAPI());
            recognizeTask.Wait();
            string task_result = recognizeTask.Result;
            RootObject plate = JObject.Parse(task_result).ToObject<RootObject>();
            return plate.results[0].plate;
        }

        public String CheckPlate(byte[] picture)
        {
            Task<string> recognizeTask = Task.Run(() => new Plate().ConsumePlateAPI());
            recognizeTask.Wait();
            string task_result = recognizeTask.Result;
            RootObject plate = JObject.Parse(task_result).ToObject<RootObject>();
            return plate.results[0].plate;
        }

        public async Task<String> GeneratePlateImage()
        {
            WebcamHelper webcam = new WebcamHelper();
            await webcam.InitializeCameraAsync();
            Task<StorageFile> recognizeTask = Task.Run(() => webcam.CapturePhoto());
            recognizeTask.Wait();
            StorageFile task_result = recognizeTask.Result;
            return task_result.Path;
        }

        private async Task<string> ConsumePlateAPI()
        {
            string SECRET_KEY = "sk_DEMODEMODEMODEMODEMODEMO";
            Byte[] bytes = File.ReadAllBytes(await GeneratePlateImage());
            string imagebase64 = Convert.ToBase64String(bytes);

            var content = new StringContent(imagebase64);

            var response = await client.PostAsync("https://api.openalpr.com/v2/recognize_bytes?recognize_vehicle=1&country=us&secret_key=" + SECRET_KEY, content).ConfigureAwait(false);

            var buffer = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            var byteArray = buffer.ToArray();
            var responseString = Encoding.UTF8.GetString(byteArray, 0, byteArray.Length);
            return responseString;
        }

        private async Task<string> ConsumePlateAPI(WebcamHelper webcam)
        {
            string SECRET_KEY = "sk_DEMODEMODEMODEMODEMODEMO";
            Byte[] bytes = File.ReadAllBytes("");
            string imagebase64 = Convert.ToBase64String(bytes);

            var content = new StringContent(imagebase64);

            var response = await client.PostAsync("https://api.openalpr.com/v2/recognize_bytes?recognize_vehicle=1&country=us&secret_key=" + SECRET_KEY, content).ConfigureAwait(false);

            var buffer = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            var byteArray = buffer.ToArray();
            var responseString = Encoding.UTF8.GetString(byteArray, 0, byteArray.Length);
            return responseString;
        }
    }
}
