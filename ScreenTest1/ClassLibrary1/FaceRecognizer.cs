using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class FaceRecognizer
    {
        private readonly IFaceServiceClient faceServiceClient =
            new FaceServiceClient("ae3512e532c545ba9e821202a1bbd350", "https://eastus.api.cognitive.microsoft.com/face/v1.0");

        public String GetFaces()
        {
            string path = "C:\\Users\\nic_l\\OneDrive\\Pictures\\Caras\\Caras1.jpg";
            var recognizeTask = Task.Run(() => GetFaces(path));
            recognizeTask.Wait();
            string task_result = recognizeTask.Result.ToString();
            return task_result;
        }

        private async Task<Face[]> GetFaces(string path)
        {
            using (Stream imageFileStream = File.OpenRead(path))
            {
                Face[] faces = await faceServiceClient.DetectAsync(imageFileStream, returnFaceId: true, returnFaceLandmarks: false);
                faceServiceClient.
                return faces;
            }
        }
    }
}
