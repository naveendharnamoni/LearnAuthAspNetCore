using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnAuth.OAuthServer
{
    public static class Constants
    {
        public const string Audience = "https://localhost:44323";
        public const string Issuer = Audience;
        public const string key = "some_dummy_key_for_auth_which_is_not_too_short";
    }
}
