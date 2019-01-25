﻿using System;
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
            public string vabsoluteExpiry { get; set; }
        }
    }

    public class APITABBetSlipEnquiryResponse
    {
        public List<_bets> bets { get; set; }

        public class _bets
        {
            public List<_legs> legs { get; set; }

            public class _legs
            {
                public string type { get; set; }
                public string propositionId { get; set; }
                public string odds { get; set; }
            }            
        }
    }

    public class APITABBetSlipResponse
    {
        public List<_bets> bets { get; set; }

        public class _bets
        {
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
}