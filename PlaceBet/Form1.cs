using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace PlaceBet
{
    public partial class Form1 : Form
    {
        public string AuthToken { get; set; }
        public string AccountNumber { get; set; }
        public string Password { get; set; }
        public string URLAuthenticate { get; set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonAuthenticate_Click(object sender, EventArgs e)
        {
            this.Password = this.textBoxPassword.Text;
            this.AccountNumber = this.textBoxAccountNumber.Text;
            this.URLAuthenticate = @"https://webapi.tab.com.au/v1/account-service/tab/authenticate";
            GetAuthToken();
        }

        public string CreatePayloadAuthenticate()
        {
            string sPayload = @"{""password"":""" + this.Password + @""",""channel"":""TABCOMAU"",""scopes"":["" * ""],""extendedTokenLifeTime"":true,""accountNumber"": " + this.AccountNumber + "}";
            return sPayload;
        }

        public void GetAuthToken()
        {
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(this.URLAuthenticate);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = CreatePayloadAuthenticate();

                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    APITABAuthenticateResponse authencateResponse = JsonConvert.DeserializeObject<APITABAuthenticateResponse>(result);
                    this.AuthToken = authencateResponse.authentication.token;
                    this.textWebResponse.Text = this.AuthToken;
                }
            }
            catch(Exception e)
            {

            }
        }
    }
}
