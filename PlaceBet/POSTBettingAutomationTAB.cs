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
    class BetTAB
    {
        public string TabcorpAuth { get; set; }
        public string AccountNumber { get; set; }
        public string PropositionId { get; set; }
        public string Odds { get; set; }
        public string Stake { get; set; }
        public string BetType { get; set; }

        public string URLAuthenticate { get; set; }
        public string URLBalance { get; set; }
        public string URLBetslipEnquiry { get; set; }
        public string URLBetslip { get; set; }
        public string UUIDV4 { get; set; }

        public BetTAB(string accountnubmer, string token, string propositionid, string stake, string odds, string bettype)
        {
            this.AccountNumber = accountnubmer;
            this.TabcorpAuth = token;
            this.PropositionId = propositionid;
            this.Stake = stake;
            this.Odds = odds;
            this.BetType = bettype;

            this.URLAuthenticate = @"https://webapi.tab.com.au/v1/account-service/tab/authenticate";
            this.URLBalance = @"https://webapi.tab.com.au/v1/account-service/tab/accounts/" + this.AccountNumber + @"/balance";
            this.URLBetslipEnquiry = @"https://webapi.tab.com.au/v1/tab-betting-service/accounts/" + this.AccountNumber + @"/betslip-enquiry?TabcorpAuth=" + this.TabcorpAuth;
            this.URLBetslip = @"https://webapi.tab.com.au/v1/tab-betting-service/accounts/" + this.AccountNumber + @"/betslip?TabcorpAuth=" + this.TabcorpAuth;
        }

        public void POSTingBetSlipEnquiry()
        {
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(this.URLBetslipEnquiry);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = CreatePayloadBetSlipEnquiry();

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


        public Boolean POSTingBetSlip()
        {
            Boolean bPlaceBetSucceed = false;

            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(this.URLBetslip);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = CreatePayloadBetSlip();

                    Console.WriteLine(" ----------------------------------  ");
                    Console.WriteLine(" json payload \t {0} ", json);

                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    APITABBetSlipResponse betSlipResponse = JsonConvert.DeserializeObject<APITABBetSlipResponse>(result);
                    //this.Odds = betSlipResponse.bets.First().legs.First().odds;
                    if(betSlipResponse.errors.Count == 0)
                    {
                        bPlaceBetSucceed = true;
                    }
                    else
                    {
                        Console.WriteLine(" ----------------------------------  ");
                        Console.WriteLine(" Error: (code)          \t {0} ", betSlipResponse.errors.First<APITABBetSlipResponse._errors>().code);
                        Console.WriteLine(" Error: (message)       \t {0} ", betSlipResponse.errors.First<APITABBetSlipResponse._errors>().message);
                        Console.WriteLine(" Error: (odds)          \t {0} ", betSlipResponse.errors.First<APITABBetSlipResponse._errors>().odds);
                        Console.WriteLine(" Error: (oldOdds)       \t {0} ", betSlipResponse.errors.First<APITABBetSlipResponse._errors>().oldOdds);
                        Console.WriteLine(" Error: (propositionId) \t {0} ", betSlipResponse.errors.First<APITABBetSlipResponse._errors>().propositionId);
                    }

                }
            }
            catch(WebException e)
            {
                Console.WriteLine(" ----------------------------------  ");
                Console.WriteLine("Error at POSTingBetSlip() -> " + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(" ----------------------------------  ");
                Console.WriteLine("Error at POSTingBetSlip() -> " + e.Message);
            }

            return bPlaceBetSucceed;
        }

        public string CreatePayloadBetSlipEnquiry()
        {
            string sPayload = @"
                        {
                          ""bets"": [
                            {
                              ""type"": ""FIXED_ODDS"",
                              ""stake"": """ + this.Stake + @""",
                              ""legs"": [
                                {
                                  ""type"": ""WIN"",
                                  ""propositionId"": " + this.PropositionId + @",
                                  ""odds"": """ + this.Odds + @"""
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

        public string CreatePayloadBetSlip()
        {
            this.UUIDV4 = GenerateUUIDV4();

            string sPayload = @"
                        {
                          ""transactionId"": """ + this.UUIDV4 + @""",
                          ""bets"": [
                            {
                              ""type"": ""FIXED_ODDS"",
                              ""stake"": """ + this.Stake + @""",
                              ""legs"": [
                                {
                                  ""type"": ""WIN"",
                                  ""propositionId"": " + this.PropositionId + @",
                                  ""odds"": """ + this.Odds + @"""
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
