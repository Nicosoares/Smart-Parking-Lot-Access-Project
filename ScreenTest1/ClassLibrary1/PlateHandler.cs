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
    public class PlateInfo
    {
        public String plate;

        public PlateInfo() { }
        private static readonly HttpClient client = new HttpClient();

        public String CheckPlate()
        {
            Task<string> recognizeTask = Task.Run(() => new PlateInfo().ConsumePlateAPI());
            recognizeTask.Wait();
            string task_result = recognizeTask.Result;
            if(task_result != null)
            {
                RootObject plate = JObject.Parse(task_result).ToObject<RootObject>();
                if (plate.results.Count > 0)
                    return plate.results[0].plate;
            }
            return "No se pudo identificar su placa";
        }

        public String CheckPlate(byte[] picture)
        {
            Task<string> recognizeTask = Task.Run(() => new PlateInfo().ConsumePlateAPI());
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
            string path = await GeneratePlateImage();
            Byte[] bytes = File.ReadAllBytes(path);
            string imagebase64 = Convert.ToBase64String(bytes);

            var content = new StringContent(imagebase64);

            try
            {
                var response = await client.PostAsync("https://api.openalpr.com/v2/recognize_bytes?recognize_vehicle=1&country=us&secret_key=" + SECRET_KEY, content).ConfigureAwait(false);

                var buffer = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
                var byteArray = buffer.ToArray();
                var responseString = Encoding.UTF8.GetString(byteArray, 0, byteArray.Length);
                File.Delete(path);
                return responseString;
            }
            catch(Exception e)
            {
                File.Delete(path);
                string empty = null;
                return empty;
            }
        }

        /*private async Task<string> ConsumePlateAPI(WebcamHelper webcam)
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
        }*/
    }
}
