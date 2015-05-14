using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Facebook;

namespace FB_Controller
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Get("oauth/access_token", new
            {
                client_id = "1499942773583853",
                client_secret = "50242a2d8dc6df614c3ca50ed4d1d20e",
                grant_type = "client_credentials"
            });
            fb.AccessToken = result.access_token;
            textBox1.Text = fb.AccessToken.ToString();
            var rq = new FacebookClient(fb.AccessToken);
            string request = rq.Get("/me").ToString();
            textBox1.Text += request.ToString();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            dynamic parameters = new ExpandoObject();

            parameters.client_id = "1499942773583853";
            parameters.redirect_uri = "https://www.facebook.com/connect/login_success.html";
            parameters.response_type = "code token";
            parameters.display = "popup";
            parameters.scope = "email";

            var fb = new FacebookClient();

            Uri loginUri = fb.GetLoginUrl(parameters);

            webBrowser1.Navigate(loginUri.AbsoluteUri);

        }
        bool _authorized = false;
        string _accessToken = "CAAVUMKQz7ZB0BANtlRFjMPvzH2PQ2We0Xy6tyd8iAAnSMMHZCduQHQMGZAln4PWOQO6CPFnYT4LVpQJ2TBay7vIR268fuq3jVONE5Hteq05GQA8QlBapz3ZAWmQdmu43giQytttLwpCAphqZCcztAwrlLX3kpc2hm4tBCYmTn7xkRHPoEazE1Nb2XSXQAnYDRZCK6sNixcj7nDni2QN4am";
        string _code = "";
        string _expire = "";
        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            if (webBrowser1.Visible)
            {
                var fb = new FacebookClient();

                FacebookOAuthResult oauthResult;
                if (fb.TryParseOAuthCallbackUrl(e.Url, out oauthResult))
                {
                    if (oauthResult.IsSuccess)
                    {
                        _accessToken = oauthResult.AccessToken;
                        _code = oauthResult.Code;
                        _expire = oauthResult.Expires.ToString();
                        _authorized = true;
                    }
                    else
                    {
                        _accessToken = "";
                        _authorized = false;
                    }

                    if (_authorized)
                    {
                        fb = new FacebookClient(_accessToken);

                        dynamic result = fb.Get("me");
                        var _currentName = result.name;
                        var _currentEmail = result.email;
                        MessageBox.Show(_accessToken);
                        textBox1.Text = _accessToken.ToString();
                        textBox1.Text += "\n\n" + _code;
                        textBox1.Text += "\n\n" + _expire;

                        //Do what need being done now that we are logged in!
                    }
                    else
                    {
                        MessageBox.Show("Couldn't log into Facebook!", "Login unsuccessful", MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dynamic messagePost = new ExpandoObject();
            messagePost.message = textBox2.Text;
            var fb = new FacebookClient(_accessToken);
            fb.Post("me" + "/feed", messagePost );
        }
    }
}
