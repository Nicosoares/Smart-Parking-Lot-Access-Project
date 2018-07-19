using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Net;
using System.Data.SqlClient;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Net;
using System.Net.Http;
using ClassLibrary1;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Windows.Media.Capture;
using Windows.Devices.Enumeration;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.Storage.FileProperties;
using Windows.Foundation.Metadata;
using Windows.Graphics.Display;
using Microsoft.Rest;
using Microsoft.Azure.Management.ResourceManager.Fluent.Authentication;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ScreenTest1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        string plateResponse = null;
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Access_Button_Click(object sender, RoutedEventArgs e)
        {
            bool needsResetting = false;
            SmartParkingAPI api = new SmartParkingAPI();

            if (Info_Textbox.Text == "Toque el boton verde para verificar su acceso al parqueo.")
            {
                plateResponse = new PlateInfo().CheckPlate();
                string plateAuthResponse = null;

                if (plateResponse != "No se pudo identificar su placa")
                {
                    plateAuthResponse = api.Plate.CheckPlateAuthStatus(plateResponse).ToString();
                    if (plateAuthResponse == "True")
                    {
                        Info_Textbox.Text = "Placa identificada:" + plateResponse + ". " + "Puede pasar, tenga un buen dia!";
                        needsResetting = true;
                    }
                    else
                        Info_Textbox.Text = "Su placa: " + plateResponse + " no posee autorizacion de acceso, por favor toque de nuevo el boton cuando este listo para iniciar el reconocimiento facial.";
                }
                else
                    Info_Textbox.Text = "Su placa no pudo ser identificada, por favor toque de nuevo el boton cuando este listo para iniciar el reconocimiento facial.";
            }
            else
            {
                UserInfo user = await new FaceRecognizer().GetFaces();
                if(user.PersonGuid != null && user.LargePersonGroupID != null)
                {
                    user = await new FaceRecognizer().GetPersonStatus(user);
                    if (user.IsAuthorized)
                    {
                        if(plateResponse != null && plateResponse.Length > 0 && plateResponse != "No se pudo identificar su placa")
                            api.Plate.AddPlateToUser(plateResponse, user.UserID);
                        Info_Textbox.Text = user.Name +  " ha sido identificado exitosamente, por favor proceda";
                        needsResetting = true;
                    }
                    else
                    {
                        Info_Textbox.Text = user.Name + " no esta autorizado para accessar, por favor comuniquese con HR";
                        needsResetting = true;
                    }
                }
                else
                {
                    Info_Textbox.Text = "No hemos sido capaces de identificarlo, por favor comuniquese con HR";
                    needsResetting = true;
                }
            }

            if (needsResetting)
            {
                await Task.Delay(20000);
                Info_Textbox.Text = "Toque el boton verde para verificar su acceso al parqueo.";

            }
        }
    }
}

