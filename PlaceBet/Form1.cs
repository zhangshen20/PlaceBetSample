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
using System.Web;
using RestSharp;

namespace PlaceBet
{
    public partial class Form1 : Form
    {
        public string Token { get; set; }
        public string AccountNumber { get; set; }
        public string Password { get; set; }
        public string URLAuthenticateTAB { get; set; }
        public string URLAuthenticateSportsBet { get; set; }

        System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();

        private AuthToken gAuthToken = new AuthToken();
        private AuthTokenSportsBet gAuthTokenSportsBet = new AuthTokenSportsBet();

        private int gTimerOffsetInSecs = 5;

        public Form1()
        {
            InitializeComponent();

            myTimer.Tick += new EventHandler(TimerValidateAuthToken);
            myTimer.Interval = 1000 * gTimerOffsetInSecs;
            myTimer.Start();

            this.Password = this.textBoxPassword.Text;
            this.AccountNumber = this.textBoxAccountNumber.Text;
            this.URLAuthenticateTAB = @"https://webapi.tab.com.au/v1/account-service/tab/authenticate";
            this.URLAuthenticateSportsBet = @"https://www.sportsbet.com.au/apigw/auth/realms/sportsbet/protocol/openid-connect/token";

            Console.WriteLine(" ----------------------------------  ");
            Console.WriteLine("Timer starts ");
            Console.WriteLine("Current time at {0}", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        private void buttonAuthenticate_Click(object sender, EventArgs e)
        {
            this.Password = this.textBoxPassword.Text;
            this.AccountNumber = this.textBoxAccountNumber.Text;
            this.URLAuthenticateSportsBet = @"https://www.sportsbet.com.au/apigw/auth/realms/sportsbet/protocol/openid-connect/token";

            GetAuthTokenSportsBetUseLogin();
        }

        private void TimerValidateAuthToken(Object myObject, EventArgs myEventArgs)
        {
            RefreshAuthToken();
        }

        private void RefreshAuthToken()
        {
            if(gAuthTokenSportsBet.AbsoluteExpiryDateTimeUTC.AddSeconds(-1 * gTimerOffsetInSecs) <= DateTime.UtcNow)
            {
                GetAuthTokenSportsBetUseRefreshToken();

                Console.WriteLine(" ----------------------------------  ");
                Console.WriteLine("New Access Token generated");
                Console.WriteLine("Current time at {0}", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));
                Console.WriteLine("Expiry  time at {0}", gAuthTokenSportsBet.AbsoluteExpiryDateTimeUTC.ToString("yyyy-MM-dd HH:mm:ss"));
                Console.WriteLine("New Access Token is {0}", gAuthTokenSportsBet.AccessToken);
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

        public string CreatePayloadAuthenticateTAB()
        {
            string sPayload = @"{""password"":""" + this.Password + @""",""channel"":""TABCOMAU"",""scopes"":["" * ""],""extendedTokenLifeTime"":true,""accountNumber"": " + this.AccountNumber + "}";
            return sPayload;
        }

        public void GetAuthTokenTAB()
        {
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(this.URLAuthenticateTAB);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = CreatePayloadAuthenticateTAB();

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

        public string CreateFormDataAuthenticateSportsBetUseLogin()
        {
            string sPayload = "client_id=cxp&grant_type=password&username=jackyzhang1981&password=Shenzhang123&scope=offline_access";
            return sPayload;
        }

        public string CreateFormDataAuthenticateSportsBetUseRefreshToken()
        {
            string sPayload = "client_id=cxp&grant_type=refresh_token&refresh_token=" + gAuthTokenSportsBet.RefreshToken;
            return sPayload;
        }

        public void GetAuthTokenSportsBetUseLogin()
        {
            try
            {
                Console.WriteLine(" *************** ");

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(this.URLAuthenticateSportsBet);

                httpWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/72.0.3626.109 Safari/537.36";
                httpWebRequest.Accept = "application/json";
                httpWebRequest.Headers.Add(HttpRequestHeader.Cookie, "cust_lang=en; aid=8575744680914864; _ga=GA1.3.1848199981.1531856942; sbt_banner_cookie=1; optimizelyEndUserId=oeu1537716251577r0.3532249167138428; __zlcmid=oXhfEcjtNfgNOd; NaN_hash=ae29bf59WXGOHZAF1540292697941; sb_partner_mid=qF3bS5LVKIbulQmTAVK722Nd7ZgqdRLk12921; _gid=GA1.3.1714688751.1550894255; _gcl_au=1.1.2063565678.1550894256; _fbp=fb.2.1550894255916.672290551; NJ_POI=e769089ee3d34add816b7f30f95efcae; tl_cid=1080031; OB_REQ=3e16098016ebe97c8350; OB_REQ_NONSSL=6d9bcb8610aab2daebf0; tl_clid=2; tl_cmpid=3955; tl_evtid=4531329; isLoggedIn=false; breakpoint=narrow; AKA_A2=A; _gat_GUA=1; utag_main=v_id:0164a9c9f21b002358b45ac4eec403072001d06a00bd0$_sn:17$_ss:0$_st:1551090727256$dc_visit:17$ses_id:1551088411877%3Bexp-session$_pn:2%3Bexp-session$_prevpage:%2Flogin%3Bexp-1551092527260$dc_event:4%3Bexp-session$dc_region:ap-southeast-2%3Bexp-session");
                httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string str = CreateFormDataAuthenticateSportsBetUseLogin();
                    streamWriter.Write(str);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    APISportsBetAuthenticateResponse authencateResponse = JsonConvert.DeserializeObject<APISportsBetAuthenticateResponse>(result);
                    gAuthTokenSportsBet.AccessToken = authencateResponse.access_token;
                    gAuthTokenSportsBet.AccessTokenExpiresInSeconds = authencateResponse.expires_in;
                    gAuthTokenSportsBet.RefreshToken = authencateResponse.refresh_token;
                    gAuthTokenSportsBet.RefreshTokenExpiresInSeconds = authencateResponse.refresh_expires_in;
                    gAuthTokenSportsBet.Scope = authencateResponse.scope;
                    gAuthTokenSportsBet.SessionState = authencateResponse.session_state;
                    gAuthTokenSportsBet.TokenType = authencateResponse.token_type;
                    gAuthTokenSportsBet.UpdateAbsoluteExpiryDateTimeUTC(gAuthTokenSportsBet.AccessTokenExpiresInSeconds);

                    this.textWebResponse.Text = gAuthTokenSportsBet.AccessToken;

                    Console.WriteLine(" -------------------------------------------------------------------- ");
                    Console.WriteLine(" *** public void GetAuthTokenSportsBetUseLogin() *** ");
                    Console.WriteLine("Access Token -> " + gAuthTokenSportsBet.AccessToken);
                    Console.WriteLine("Time Now\t: {0}", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));
                    Console.WriteLine("Expire At\t: {0}", gAuthTokenSportsBet.AbsoluteExpiryDateTimeUTC.ToString("yyyy-MM-dd HH:mm:ss"));

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(" exception -> " + e.ToString());
                //MessageBox.Show(e.Message);
            }
        }

        public void GetAuthTokenSportsBetUseRefreshToken()
        {
            try
            {
                Console.WriteLine(" *************** ");

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(this.URLAuthenticateSportsBet);

                httpWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/72.0.3626.109 Safari/537.36";
                httpWebRequest.Accept = "application/json";
                httpWebRequest.Headers.Add(HttpRequestHeader.Cookie, "cust_lang=en; aid=8575744680914864; _ga=GA1.3.1848199981.1531856942; sbt_banner_cookie=1; optimizelyEndUserId=oeu1537716251577r0.3532249167138428; __zlcmid=oXhfEcjtNfgNOd; NaN_hash=ae29bf59WXGOHZAF1540292697941; sb_partner_mid=qF3bS5LVKIbulQmTAVK722Nd7ZgqdRLk12921; _gid=GA1.3.1714688751.1550894255; _gcl_au=1.1.2063565678.1550894256; _fbp=fb.2.1550894255916.672290551; NJ_POI=e769089ee3d34add816b7f30f95efcae; tl_cid=1080031; OB_REQ=3e16098016ebe97c8350; OB_REQ_NONSSL=6d9bcb8610aab2daebf0; tl_clid=2; tl_cmpid=3955; tl_evtid=4531329; isLoggedIn=false; breakpoint=narrow; AKA_A2=A; _gat_GUA=1; utag_main=v_id:0164a9c9f21b002358b45ac4eec403072001d06a00bd0$_sn:17$_ss:0$_st:1551090727256$dc_visit:17$ses_id:1551088411877%3Bexp-session$_pn:2%3Bexp-session$_prevpage:%2Flogin%3Bexp-1551092527260$dc_event:4%3Bexp-session$dc_region:ap-southeast-2%3Bexp-session");
                httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string str = CreateFormDataAuthenticateSportsBetUseRefreshToken();
                    streamWriter.Write(str);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    APISportsBetAuthenticateResponse authencateResponse = JsonConvert.DeserializeObject<APISportsBetAuthenticateResponse>(result);
                    gAuthTokenSportsBet.AccessToken = authencateResponse.access_token;
                    gAuthTokenSportsBet.AccessTokenExpiresInSeconds = authencateResponse.expires_in;
                    gAuthTokenSportsBet.RefreshToken = authencateResponse.refresh_token;
                    gAuthTokenSportsBet.RefreshTokenExpiresInSeconds = authencateResponse.refresh_expires_in;
                    gAuthTokenSportsBet.Scope = authencateResponse.scope;
                    gAuthTokenSportsBet.SessionState = authencateResponse.session_state;
                    gAuthTokenSportsBet.TokenType = authencateResponse.token_type;

                    gAuthTokenSportsBet.UpdateAbsoluteExpiryDateTimeUTC(gAuthTokenSportsBet.AccessTokenExpiresInSeconds);

                    this.textWebResponse.Text = gAuthTokenSportsBet.AccessToken;

                    Console.WriteLine(" -------------------------------------------------------------------- ");
                    Console.WriteLine(" *** public void GetAuthTokenSportsBetUseRefreshToken() *** ");
                    Console.WriteLine("Access Token -> " + gAuthTokenSportsBet.AccessToken);
                    Console.WriteLine("Time Now\t: {0}", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));
                    Console.WriteLine("Expire At\t: {0}", gAuthTokenSportsBet.AbsoluteExpiryDateTimeUTC.ToString("yyyy-MM-dd HH:mm:ss"));

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(" exception -> " + e.ToString());
                //MessageBox.Show(e.Message);
            }
        }

        private void buttonPlaceBet_Click(object sender, EventArgs e)
        {
            //BetTAB bBet = new BetTAB(this.AccountNumber, gAuthToken.Token, textBoxPropositionId.Text, "1", textBoxOdds.Text, "WIN");
            //if(bBet.POSTingBetSlip())
            //{
            //    Console.WriteLine(" ----------------------------------  ");
            //    Console.WriteLine("SUCCEED: Place Bet PropositionId {0} with Stake {1} at FW Odds {2}", bBet.PropositionId, bBet.Stake, bBet.Odds);
            //}
            //else
            //{
            //    Console.WriteLine(" ----------------------------------  ");
            //    Console.WriteLine("FAILED: Place Bet PropositionId {0} with Stake {1} at FW Odds {2}", bBet.PropositionId, bBet.Stake, bBet.Odds);
            //}

            BetSportsBet bBet = new BetSportsBet(gAuthTokenSportsBet.AccessToken, textBoxPropositionId.Text, "0.01", "1");
            if (bBet.POSTingBetSlipSportsBet())
            {
                Console.WriteLine(" ----------------------------------  ");
                Console.WriteLine("SUCCEED: Place Bet PropositionId {0} with Stake {1} at FW Odds {2}", bBet.Outcome, bBet.Stake, bBet.Odds);
            }
            else
            {
                Console.WriteLine(" ----------------------------------  ");
                Console.WriteLine("FAILED: Place Bet PropositionId {0} with Stake {1} at FW Odds {2}", bBet.Outcome, bBet.Stake, bBet.Odds);
            }

        }
    }
}
