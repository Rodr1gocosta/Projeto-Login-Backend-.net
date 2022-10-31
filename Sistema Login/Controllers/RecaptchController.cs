using Castle.Core.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using Sistema_Login.Data.Dtos;
using Sistema_Login.Data.Requests;
using System;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Net.WebRequestMethods;

namespace Sistema_Login.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RecaptchController
    {

        [HttpPost]
        public bool ValidarRecaptch(RecaptchaDto recaptchaResponse)
        {

            if (string.IsNullOrEmpty(recaptchaResponse.token))
            {
                return false;
            }

            var secret = "6LfscikiAAAAAEO3R58hWv0P3DiWeU3aHsBWhZiA";
            if (string.IsNullOrEmpty(secret))
            {
                return false;
            }

            var path = "https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}";

            var client = new WebClient();
            var reply = client.DownloadString(string.Format(path, secret, recaptchaResponse.token));

            return JsonSerializer.Deserialize<CaptchaGoogle>(reply).Success;
        }
    }
}
