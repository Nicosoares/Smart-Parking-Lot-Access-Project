using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Graphics.Imaging;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Streams;

namespace ClassLibrary1
{
    public class Camera
    {
        public Camera() { }

        public Byte[] GetImageByteArrayAsync()
        {
            Camera cam = new Camera();
            cam.CaptureImage();
            Byte[] task_result = File.ReadAllBytes("C:\\Users\\nic_l\\OneDrive\\Pictures\\photo.jpg");
            return task_result;
        }

        public async void CaptureImage()
        {
            var settings = new MediaCaptureInitializationSettings();
            //settings.StreamingCaptureMode = ;
            settings.PhotoCaptureSource = PhotoCaptureSource.VideoPreview;
            var devices = await Windows.Devices.Enumeration.DeviceInformation.FindAllAsync(Windows.Devices.Enumeration.DeviceClass.VideoCapture);


                MediaCapture mediaCapture = new MediaCapture();
            await mediaCapture.InitializeAsync(settings);
            var myPictures = await Windows.Storage.StorageLibrary.GetLibraryAsync(Windows.Storage.KnownLibraryId.Pictures);
            StorageFile file = await myPictures.SaveFolder.CreateFileAsync("photo.jpg", CreationCollisionOption.GenerateUniqueName);

            using (var captureStream = new InMemoryRandomAccessStream())
            {
                await mediaCapture.CapturePhotoToStreamAsync(ImageEncodingProperties.CreateJpeg(), captureStream);

                using (var fileStream = await file.OpenAsync(FileAccessMode.ReadWrite))
                {
                    var decoder = await BitmapDecoder.CreateAsync(captureStream);
                    var encoder = await BitmapEncoder.CreateForTranscodingAsync(fileStream, decoder);

                    var properties = new BitmapPropertySet {
                        { "System.Photo.Orientation", new BitmapTypedValue(PhotoOrientation.Normal, PropertyType.UInt16) }
                    };
                    await encoder.BitmapProperties.SetPropertiesAsync(properties);

                    await encoder.FlushAsync();
                }
            }
            //return File.ReadAllBytes("C:\\Users\\nic_l\\OneDrive\\Pictures\\photo.jpg");
        }
    }
}
