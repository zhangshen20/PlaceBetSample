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
using System.Net.Http;

namespace PlaceBet
{
    class BetSportsBet
    {
        public string AccessToken { get; set; }
        public string Outcome { get; set; }
        public string Odds { get; set; }
        public string PriceNum { get; set; }
        public string PriceDen { get; set; }
        public string Stake { get; set; }
        public string URLBetslipSportsBet { get; set; }
        public string UUIDV4 { get; set; }

        public BetSportsBet(string accesstoken, string outcome, string stake, string odds)
        {
            this.AccessToken = accesstoken;
            this.Outcome = outcome;
            this.Stake = String.Format("{0:F2}", (Convert.ToDecimal(stake)));
            this.Odds = String.Format("{0:F2}", (Convert.ToDecimal(odds)));

            // -- TEMP SET
            this.PriceNum = "1000";
            this.PriceDen = "1000";

            Console.WriteLine("this.Stake -> " + this.Stake);
            Console.WriteLine("this.Odds -> " + this.Odds);

            this.URLBetslipSportsBet = @"https://www.sportsbet.com.au/apigw/acs/bets";
        }

        public Boolean POSTingBetSlipSportsBet()
        {
            Boolean bPlaceBetSucceed = false;

            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(this.URLBetslipSportsBet);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                httpWebRequest.Headers.Add("accesstoken", this.AccessToken);

                string sTransuniqueId = this.GenerateUUIDV4();
                Console.WriteLine("accesstoken -> {0}", this.AccessToken);
                Console.WriteLine("transuniqueid -> {0}", sTransuniqueId);

                httpWebRequest.Headers.Add("transuniqueid", sTransuniqueId);

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = CreatePayloadBetSlipSportsBet();

                    Console.WriteLine(" ----------------------------------  ");
                    Console.WriteLine(" json payload \t {0} ", json);

                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                // Temp Set 'bPlaceBetSucceed = true;'
                bPlaceBetSucceed = true;

                //HttpClientHandler handler = new HttpClientHandler();
                //handler.AutomaticDecompression = System.Net.DecompressionMethods.GZip;
                //HttpClient _client = new HttpClient(handler);

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    Console.WriteLine(" *** var result = streamReader.ReadToEnd(); *** ");
                    Console.WriteLine(result.ToString());

                    //APISportsBetBetSlipResponse betSlipResponse = JsonConvert.DeserializeObject<APISportsBetBetSlipResponse>(result);

                    //if (betSlipResponse.errors.Count == 0)
                    //{
                    //    bPlaceBetSucceed = true;
                    //}
                    //else
                    //{
                    //    Console.WriteLine(" ----------------------------------  ");
                    //    Console.WriteLine(" Error: (code)          \t {0} ", betSlipResponse.errors.First<APITABBetSlipResponse._errors>().code);
                    //    Console.WriteLine(" Error: (message)       \t {0} ", betSlipResponse.errors.First<APITABBetSlipResponse._errors>().message);
                    //    Console.WriteLine(" Error: (odds)          \t {0} ", betSlipResponse.errors.First<APITABBetSlipResponse._errors>().odds);
                    //    Console.WriteLine(" Error: (oldOdds)       \t {0} ", betSlipResponse.errors.First<APITABBetSlipResponse._errors>().oldOdds);
                    //    Console.WriteLine(" Error: (propositionId) \t {0} ", betSlipResponse.errors.First<APITABBetSlipResponse._errors>().propositionId);
                    //}

                }
            }
            catch (WebException ex)
            {
                using (var stream = ex.Response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    Console.WriteLine(" ----------------------------------  ");
                    Console.WriteLine(reader.ReadToEnd());
                }
                //Console.WriteLine(" ----------------------------------  ");
                //Console.WriteLine("Error at POSTingBetSlip() -> " + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(" ----------------------------------  ");
                Console.WriteLine("Error at POSTingBetSlip() -> " + e.Message);
            }

            return bPlaceBetSucceed;
        }

        public string GenerateUUIDV4()
        {
            Guid g = Guid.NewGuid();
            return g.ToString().Replace("-", "").Substring(0,29);
        }

        public string CreatePayloadBetSlipSportsBet()
        {
            string sPayload = @"
                {
                    ""betItems"": [
                    {
                        ""betNo"": 0,
                        ""stakePerLine"": " + this.Stake.ToString() + @",
                        ""betType"": ""SGL"",
                        ""legs"": [
                        {
                            ""legNo"": 0,
                            ""legSort"": ""--"",
                            ""legType"": ""W"",
                            ""legDesc"": """",
                            ""parts"": [
                            {
                                ""outcome"": " + this.Outcome.ToString() + @", 
                                ""partNo"": 1,
                                ""priceType"": ""L"",
                                ""priceNum"": " + this.PriceNum.ToString() + @",
                                ""priceDen"": " + this.PriceDen.ToString() + @"
                            }
                            ]
                        }
                      ],
                      ""legType"": ""W""
                    }
                  ],
                  ""checkBalance"": true,
                  ""errorDetail"": ""ALL"",
                  ""firstBet"": true,
                  ""fullDetails"": true,
                  ""pendingBetCount"": true,
                  ""returnBalance"": true,
                  ""returnCashoutAvailable"": true,
                  ""jointAccountId"": null
                }
            ";

            return sPayload;
        }
    }
}
