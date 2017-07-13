using Facebook;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using XPressWPF.Model;
using XPressWPF.Shared;

namespace XPressWPF.Facebook
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //added nuget package Facebook by Outercurve Foundation
        public string AccessToken;
        public FacebookLoginParameters _facebookLoginData;
        public FacebookClient _facebookClient;
        public TaskCompletionSource<bool> _tcs;

        // No need to apply MVVM pater to this kind of simple app
        // Maybe add some exception handling
        public MainWindow(TaskCompletionSource<bool> tcs)
        {
            InitializeComponent();
            _tcs = tcs;

            // change value if you wish to use your own Facebook app
            LoginToFacebook(1466885683378698);

            this.Closing += Window_Closing;
        }

        private void LoginToFacebook(long facebookAppId)
        {
            _facebookLoginData = new FacebookLoginParameters()
            {
                Endpoint = "https://www.facebook.com/v2.9/dialog/oauth?",
                ClientIdParameterName = "client_id=",
                //ClientIdParameterValue = "1466885683378698",
                ClientIdParameterValue = facebookAppId.ToString(),
                RedirectUriParameterName = "redirect_uri=",
                RedirectUriParameterValue = "http://localhost/",
                ResponseType = "response_type=token&",
                DisplayTypeParameterName = "display=",
                DisplayTypeParameterValue = "popup",
                ParameterSeparator = "&"
            };

            // Uri should look like this
            // 2.9 versio is latest version and is supported until July 2019
            // TODO: notify user 2 months before deadline
            //WebBrowser.Navigate(new Uri($"https://www.facebook.com/v2.9/dialog/oauth?client_id=1466885683378698&display=popup&response_type=token&redirect_uri=http://localhost/", UriKind.Absolute));
            WebBrowser.Navigate(new Uri(_facebookLoginData.Endpoint +
                                        _facebookLoginData.ClientIdParameterName + _facebookLoginData.ClientIdParameterValue +
                                        _facebookLoginData.ParameterSeparator + _facebookLoginData.DisplayTypeParameterName +
                                        _facebookLoginData.DisplayTypeParameterValue +
                                        _facebookLoginData.ParameterSeparator + _facebookLoginData.ResponseType +
                                        _facebookLoginData.ParameterSeparator + _facebookLoginData.RedirectUriParameterName +
                                        _facebookLoginData.RedirectUriParameterValue,
                UriKind.Absolute));
        }

        //Get only my full name and profile picture 
        private void WebBrowser_OnNavigated(object sender, NavigationEventArgs e)
        {
            if (e.Uri.ToString().StartsWith(_facebookLoginData.RedirectUriParameterValue))
            {
                // Remove url and acces_token part
                // we are only interested in access token
                AccessToken = e.Uri.Fragment.Split('?')[0].Replace("#access_token=", "");

                _facebookClient = new FacebookClient(AccessToken);

                dynamic facebookUser = _facebookClient.Get("Me");
                User.FacebookFullName = facebookUser.name.ToString();
                User.FacebookProfileImageSmall = new BitmapImage(new Uri("https://graph.facebook.com/v2.9/" + facebookUser.id.ToString() + "/picture/"));

                //this.Close();

                _tcs.SetResult(true);
            }
        }

        // For some reason when closing this window, window is just hidden
        // so we override non virtual Window.Close method
        // and disable CS0108 warning:
        // 'MainWindow.Close()' hides inherited member 'Window.Close()'. Use the new keyword if hiding was intended.
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
 
        #pragma warning disable CS0108
        public void Close()
        {
            this.Closing -= Window_Closing;
            base.Close();
        }
        #pragma warning restore CS0108
        
    }
}
