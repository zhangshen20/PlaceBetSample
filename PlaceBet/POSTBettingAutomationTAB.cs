using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using System.Globalization;
using System.Collections.Concurrent;
using System.Threading;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Configuration;

namespace PlaceBet
{
    class POSTBettingAutomationTAB
    {
        public string TabcorpAuth { get; set; }
        public string AccountNumber { get; set; }
        public string URLAuthenticate { get; set; }
        public string URLBalance { get; set; }
        public string URLBetslipEnquiry { get; set; }
        public string URLBetslip { get; set; }
        public string UUIDV4 { get; set; }
        public string PropositionId { get; set; }
        public string Odds { get; set; }

        public string RunnerName { get; set; }
        public string BetAmount { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public POSTBettingAutomationTAB(string bettingurl, string runnername, string betamount, string username, string password)
        {
            this.URLAuthenticate = @"https://webapi.tab.com.au/v1/account-service/tab/authenticate";
            this.URLBalance = @"https://webapi.tab.com.au/v1/account-service/tab/accounts/" + this.AccountNumber + @"/balance";
            this.URLBetslipEnquiry = @"https://webapi.tab.com.au/v1/tab-betting-service/accounts/" + this.AccountNumber + @"/betslip-enquiry?TabcorpAuth=" + this.TabcorpAuth;
            this.URLBetslip = @"https://webapi.tab.com.au/v1/tab-betting-service/accounts/" + this.AccountNumber + @"/betslip?TabcorpAuth=" + this.TabcorpAuth;

            this.RunnerName = runnername;
            this.BetAmount = betamount;
            this.Username = username;
            this.Password = password;
        }

        public void POSTingBetSlipEnquiry(string propositionid, string odds)
        {
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(this.URLBetslipEnquiry);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = CreatePayloadBetSlipEnquiry(propositionid, odds);

                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    APITABBetSlipEnquiryResponse betSlipEnquiryResponse = JsonConvert.DeserializeObject<APITABBetSlipEnquiryResponse>(result);
                    this.Odds = betSlipEnquiryResponse.bets.First().legs.First().odds;
                }
            }
            catch (Exception e)
            {

            }
        }


        public void POSTingBetSlip(string propositionid, string odds)
        {
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(this.URLBetslip);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = CreatePayloadBetSlip(propositionid, odds);

                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    APITABBetSlipResponse betSlipResponse = JsonConvert.DeserializeObject<APITABBetSlipResponse>(result);
                    this.Odds = betSlipResponse.bets.First().legs.First().odds;
                }
            }
            catch (Exception e)
            {

            }
        }


        public string CreatePayloadAuthenticate()
        {
            string sPayload = @"{""password"":""" + this.Password + @""",""channel"":""TABCOMAU"",""scopes"":["" * ""],""extendedTokenLifeTime"":true,""accountNumber"": " + this.AccountNumber + "}";
            return sPayload;
        }

        public string CreatePayloadBetSlipEnquiry(string propositionid, string odds)
        {
            this.PropositionId = propositionid;
            this.Odds = odds;

            string sPayload = @"
                        {
                          ""bets"": [
                            {
                              ""type"": ""FIXED_ODDS"",
                              ""stake"": ""$1.00"",
                              ""legs"": [
                                {
                                  ""type"": ""WIN"",
                                  ""propositionId"": " + this.PropositionId + @",
                                  ""odds"": " + this.Odds + @"""
                                }
                              ],
                              ""enableMultiplier"": false,
                              ""source"": ""racing.race-nav.race.bet-type""
                            }
                          ]
                        }
            ";

            return sPayload;
        }

        public string GenerateUUIDV4()
        {
            Guid g = Guid.NewGuid();
            return g.ToString();
        }

        public string CreatePayloadBetSlip(string propositionid, string odds)
        {
            this.UUIDV4 = GenerateUUIDV4();

            string sPayload = @"
                        {
                          ""transactionId"": """ + this.UUIDV4 + @""",
                          ""bets"": [
                            {
                              ""type"": ""FIXED_ODDS"",
                              ""stake"": ""$1.00"",
                              ""legs"": [
                                {
                                  ""type"": ""WIN"",
                                  ""propositionId"": " + this.PropositionId + @",
                                  ""odds"": " + this.Odds + @"""
                                }
                              ],
                              ""enableMultiplier"": false,
                              ""source"": ""racing.race-nav.race.bet-type""
                            }
                          ]
                        }
            ";

            return sPayload;
        }
    }
}
