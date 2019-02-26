using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceBet
{
    public class APITABAuthenticateResponse
    {
        public string accountNumber { get; set; }
        public string verificationStatus { get; set; }
        public _authentication authentication { get; set; }

        public class _authentication
        {
            public string token { get; set; }
            public string inactivityExpiry { get; set; }
            public string absoluteExpiry { get; set; }
        }
    }

    public class APITABBetSlipEnquiryResponse
    {
        public List<_bets> bets { get; set; }

        public class _bets
        {
            public string type { get; set; }

            public List<_errors> errors { get; set; }

            public class _errors
            {
                public string code { get; set; }
                public string message { get; set; }
            }

            public string stake { get; set; }

            public List<_legs> legs { get; set; }

            public class _legs
            {
                public string type { get; set; }
                public string propositionId { get; set; }
                public string odds { get; set; }
            }
            
            public string status { get; set; }
        }
    }

    public class APITABBetSlipResponse
    {
        public List<_bets> bets { get; set; }

        public class _bets
        {
            public string type { get; set; }

            public List<_errors> errors { get; set; }

            public class _errors
            {
                public string code { get; set; }
                public string message { get; set; }
            }

            public string stake { get; set; }

            public List<_legs> legs { get; set; }

            public class _legs
            {
                public string type { get; set; }
                public string propositionId { get; set; }
                public string odds { get; set; }
            }

            public string ticketSerialNumber { get; set; }
            public string status { get; set; }
            public string accountNumber { get; set; }
            public string accountBalance { get; set; }
        }

        public List<_errors> errors { get; set; }

        public class _errors
        {
            public string odds { get; set; }
            public string oldOdds { get; set; }
            public string propositionId { get; set; }
            public string code { get; set; }
            public string message { get; set; }
            public string betIndex { get; set; }
        }
    }

    public class APITABErrorResponse
    {
        public _error error { get; set; }

        public class _error
        {
            public List<_details> details { get; set; }

            public class _details
            {
                public string path { get; set; }
                public string value { get; set; }
                public string message { get; set; }
            }

            public string code { get; set; }
            public string message { get; set; }
        }
    }

    public class APISportsBetAuthenticateResponse
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public int refresh_expires_in { get; set; }
        public string refresh_token { get; set; }
        public string token_type { get; set; }
        public string session_state { get; set; }
        public string scope { get; set; }
    }

    public class APISportsBetBetSlipResponse
    {
        public string correlationId { get; set; }
    }
}
