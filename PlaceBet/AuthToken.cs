using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceBet
{
    public class AuthToken
    {
        private const string gsDateFormat = "yyyy-MM-ddTHH:mm:ss.fffZ";

        public string Token { get; set; }
        public DateTime AbsoluteExpiryDateTimeUTC { get; set; }
        public DateTime InactivityExpiryDateTimeUTC { get; set; }

        public AuthToken()
        {
            this.Token = "";
            this.AbsoluteExpiryDateTimeUTC = DateTime.UtcNow.AddDays(-1);
            this.InactivityExpiryDateTimeUTC = DateTime.UtcNow.AddDays(-1);
        }

        public void UpdateAbsoluteExpiryDateTimeUTC(string dtUtc)
        {
            Console.WriteLine("DateTime.ParseExact(dtUtc.Replace(), gsDateFormat.Replace(), CultureInfo.InvariantCulture) -> {0}", DateTime.ParseExact(dtUtc.Replace("Z", ""), gsDateFormat.Replace("Z", ""), CultureInfo.InvariantCulture).ToString());

            this.AbsoluteExpiryDateTimeUTC = DateTime.ParseExact(dtUtc.Replace("Z", ""), gsDateFormat.Replace("Z", ""), CultureInfo.InvariantCulture);
            //this.AbsoluteExpiryDateTimeUTC = DateTime.ParseExact(dtUtc, CultureInfo.InvariantCulture);
        }

        public void UpdateInactivityExpiryDateTimeUTC(string dtUtc)
        {
            this.InactivityExpiryDateTimeUTC = DateTime.ParseExact(dtUtc, gsDateFormat, CultureInfo.InvariantCulture);
        }
    }

    public class AuthTokenSportsBet
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public int AccessTokenExpiresInSeconds { get; set; }
        public int RefreshTokenExpiresInSeconds { get; set; }
    
        public string TokenType { get; set; }
        public int NotBeforePolicy { get; set; }
        public string SessionState { get; set; }
        public string Scope { get; set; }
    }
}
