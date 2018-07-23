using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace ClassLibrary1
{
    public class FaceRecognizer
    {
        private readonly IFaceServiceClient faceServiceClient =
            new FaceServiceClient("ae3512e532c545ba9e821202a1bbd350", "https://eastus.api.cognitive.microsoft.com/face/v1.0");

        public async Task<String> GenerateFaceImage()
        {
            WebcamHelper webcam = new WebcamHelper();
            await webcam.InitializeCameraAsync();
            Task<StorageFile> recognizeTask = Task.Run(() => webcam.CapturePhoto());
            recognizeTask.Wait();
            StorageFile task_result = recognizeTask.Result;
            return task_result.Path;
        }

        public async Task<UserInfo> GetFaces()
        {
            string path = await GenerateFaceImage();
            UserInfo task_result = await GetFaces(path);
            return task_result;
        }

        private async Task<UserInfo> GetFaces(string path)
        {
            UserInfo user = new UserInfo();
            using (Stream imageFileStream = File.OpenRead(path))
            {
                Face[] faces = await faceServiceClient.DetectAsync(imageFileStream, returnFaceId: true, returnFaceLandmarks: false);
                if (faces.Length > 0)
                {
                    var facesID = faces.Select(face => face.FaceId).ToArray();
                    LargePersonGroup[] largePersonGroups = await faceServiceClient.ListLargePersonGroupsAsync("", 1000);
                    float confidenceLevel = 0.75F;
                    foreach (var largePersonGroup in largePersonGroups)
                    {
                        var result = await faceServiceClient.IdentifyAsync(facesID, null, largePersonGroup.LargePersonGroupId, confidenceLevel, 1);
                        if (result != null)
                        {
                            if (result.Length > 0)
                            {
                                if (result[0].Candidates.Length > 0)
                                {
                                    user.PersonGuid = result[0].Candidates[0].PersonId;
                                    user.LargePersonGroupID = largePersonGroup.LargePersonGroupId;
                                }
                            }
                        }
                    }
                }
                return user;
            }
        }

        public async Task<UserInfo> GetPersonStatus(UserInfo user)
        {
            SmartParkingAPI api = new SmartParkingAPI();
            var result = await faceServiceClient.GetPersonInLargePersonGroupAsync(user.LargePersonGroupID, user.PersonGuid);
            user.UserID = result.UserData;
            user.Name = result.Name;
            user.IsAuthorized = Convert.ToBoolean(api.User.CheckUserAuthStatus(user.UserID));
            return user;
        }
    }
}
