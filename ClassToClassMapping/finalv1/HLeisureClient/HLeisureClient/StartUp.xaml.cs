using HLeisureClient.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
namespace HLeisureClient
{
    /// <summary>
    /// Interaction logic for StartUp.xaml
    /// </summary>
    public partial class StartUp : Window
    {
        public StartUp()
        {
            InitializeComponent();
            checkServerStatus();
        }

        private void checkServerStatus()
        {
            string serverPath = "";
            try
            {
                

                string basePath = AppDomain.CurrentDomain.BaseDirectory;
                string commonPath = basePath.Remove(basePath.Length - @"bin\debug\".Length);
                commonPath += "Config\\hostConfig.dat";
                

                using(StreamReader reader = new StreamReader(commonPath))
                {
                    
                    serverPath = reader.ReadLine();
                    txtPath.Text = serverPath;
                }
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(serverPath);

                client.DefaultRequestHeaders.Accept.Add(
                   new MediaTypeWithQualityHeaderValue("application/json"));
                UserModel usr = new UserModel();
                var response = client.PostAsJsonAsync("api/Users", usr).Result;
                if (response!=null)
                {
                    ServerPath.ServerPathName = txtPath.Text;
                    Login wnd = new Login();
                    wnd.Show();
                    this.Close();
                }
                
            }
            catch(System.Exception ex)
            {
                
                MessageBox.Show("Communication with server failed" + ex.GetType().ToString());
            }

            
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string commonPath = basePath.Remove(basePath.Length - @"bin\debug\".Length);
            commonPath += "Config\\hostConfig.dat";
            using (StreamWriter writer = new StreamWriter(commonPath))
            {
                writer.WriteLine(txtPath.Text);
            }
            checkServerStatus();
        }
    }
}
