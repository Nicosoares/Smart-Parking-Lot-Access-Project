using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Net;
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


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ScreenTest1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Access_Button_Click(object sender, RoutedEventArgs e)
        {

            //Access_Button.Content = new Camera().GetImageName();
            byte[] vs = new Byte[1];
            string result = new Plate().CheckPlate();
            Access_Button.Content = result;
        }
    }
}

