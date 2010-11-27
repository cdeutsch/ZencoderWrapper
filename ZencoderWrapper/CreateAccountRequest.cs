using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZencoderWrapper
{
    public class CreateAccountRequest
    {
        /// <summary>
        /// Set to "1" if you agree to the Terms of Service.
        /// </summary>
        public string terms_of_service { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string affiliate_code { get; set; }
        public bool? newsletter { get; set; }

        public CreateAccountRequest()
        {
            
        }

        public CreateAccountRequest(string email, string password)
            : this()
        {
            this.terms_of_service = "1";
            this.email = email;
            this.password = password;
        }

        public CreateAccountRequest(string email, string password, string affiliate_code)
            :this(email, password)
        {
            this.affiliate_code = affiliate_code;
        }
    }
}
