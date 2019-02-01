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
        public string Token { get; set; }
        public string AccountNumber { get; set; }
        public string Password { get; set; }
        public string URLAuthenticate { get; set; }

        System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();

        private AuthToken gAuthToken = new AuthToken();
        private int gTimerOffsetInSecs = 5;

        public Form1()
        {
            InitializeComponent();

            myTimer.Tick += new EventHandler(TimerValidateAuthToken);
            myTimer.Interval = 1000 * gTimerOffsetInSecs;
            myTimer.Start();

            this.Password = this.textBoxPassword.Text;
            this.AccountNumber = this.textBoxAccountNumber.Text;
            this.URLAuthenticate = @"https://webapi.tab.com.au/v1/account-service/tab/authenticate";

            Console.WriteLine(" ----------------------------------  ");
            Console.WriteLine("Timer starts ");
            Console.WriteLine("Current time at {0}", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        private void buttonAuthenticate_Click(object sender, EventArgs e)
        {
            this.Password = this.textBoxPassword.Text;
            this.AccountNumber = this.textBoxAccountNumber.Text;
            this.URLAuthenticate = @"https://webapi.tab.com.au/v1/account-service/tab/authenticate";

            GetAuthToken();
        }

        private void TimerValidateAuthToken(Object myObject, EventArgs myEventArgs)
        {
            ValidateAuthToken();
        }

        private void ValidateAuthToken()
        {
            if(gAuthToken.AbsoluteExpiryDateTimeUTC.AddSeconds(-1 * gTimerOffsetInSecs) <= DateTime.UtcNow)
            {
                GetAuthToken();

                Console.WriteLine(" ----------------------------------  ");
                Console.WriteLine("New Auth Token generated");
                Console.WriteLine("Current time at {0}", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));
                Console.WriteLine("Expiry  time at {0}", gAuthToken.AbsoluteExpiryDateTimeUTC.ToString("yyyy-MM-dd HH:mm:ss"));
                Console.WriteLine("New Auth Token is {0}", gAuthToken.Token);
            }
            else
            {
                //Console.WriteLine(" ----------------------------------  ");
                //Console.WriteLine("Existing Token still active");
                //Console.WriteLine("Current time at {0}", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));
                //Console.WriteLine("Expiry  time at {0}", gAuthToken.AbsoluteExpiryDateTimeUTC.ToString("yyyy-MM-dd HH:mm:ss"));
                //Console.WriteLine("Current Auth Token is {0}", gAuthToken.Token);
            }
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
                    gAuthToken.Token = authencateResponse.authentication.token;
                    gAuthToken.UpdateAbsoluteExpiryDateTimeUTC(authencateResponse.authentication.absoluteExpiry);
                    gAuthToken.UpdateInactivityExpiryDateTimeUTC(authencateResponse.authentication.inactivityExpiry);
                    this.textWebResponse.Text = gAuthToken.Token;
                }
            }
            catch(Exception e)
            {

            }
        }

        private void buttonPlaceBet_Click(object sender, EventArgs e)
        {
            BetTAB bBet = new BetTAB(this.AccountNumber, gAuthToken.Token, textBoxPropositionId.Text, "1", textBoxOdds.Text, "WIN");
            if(bBet.POSTingBetSlip())
            {
                Console.WriteLine(" ----------------------------------  ");
                Console.WriteLine("SUCCEED: Place Bet PropositionId {0} with Stake {1} at FW Odds {2}", bBet.PropositionId, bBet.Stake, bBet.Odds);
            }
            else
            {
                Console.WriteLine(" ----------------------------------  ");
                Console.WriteLine("FAILED: Place Bet PropositionId {0} with Stake {1} at FW Odds {2}", bBet.PropositionId, bBet.Stake, bBet.Odds);
            }
        }
    }
}
