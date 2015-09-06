using HLeisureClient.Model;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void OnLogin(object sender, RoutedEventArgs e)
        {
            try
            {

                HttpClient client = new HttpClient();
                //client.BaseAddress = new Uri("http://localhost:62025/");
                client.BaseAddress = new Uri(ServerPath.ServerPathName);
                client.DefaultRequestHeaders.Accept.Add(
                   new MediaTypeWithQualityHeaderValue("application/json"));

                UserModel usr = new UserModel();
                usr.UserName = txtUserName.Text;
                usr.Password = txtPassword.Password;
                String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(txtUserName.Text + ":" + txtPassword.Password));
                client.DefaultRequestHeaders.Add("Authorization", "Basic " + encoded);
                var response = client.PostAsJsonAsync("api/Users", usr).Result;
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var user = response.Content.ReadAsAsync<UserModel>().Result;
                    user.Password = txtPassword.Password;
                    MainWindow wnd = new MainWindow(user);
                    wnd.Show();
                    this.Close();
                }
                else
                {
                    if(response.StatusCode==HttpStatusCode.NotFound)
                        MessageBox.Show("Invalid Credentials");
                    txtUserName.Text = "";
                    txtPassword.Password = "";
                }
            }
            catch
            {
                MessageBox.Show("Communication with server failed");
            }
        }

        private void OnCancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
