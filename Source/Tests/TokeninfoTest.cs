using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PayPal.Api;
using System;
using System.Net;

namespace PayPal.Testing
{
    [TestClass]
    public class TokeninfoTest
    {
        [TestMethod, TestCategory("Functional")]
        public void TokeninfoCreateFromAuthorizationCodeTest()
        {
        }

        [TestMethod, TestCategory("Functional")]
        public void TokeninfoCreateFromRefreshTokenTest()
        {
            var apiContext = TestingUtil.GetApiContext();
            var tokenInfo = new Tokeninfo
            {
                refresh_token = "0CKmT7PhTP5Fhxd8Jph7_HbuNWlPonw8E0MHIi_BxWKbtBugPTil8lMqnIcqowvW9LX3EVuExYKKjEJJP0Mwc2llTl2sT233B4ll-GnfGmr700hkFI9AxAI9VdXHa2j_mHHDmhoxg7CUti6VuocGd5VOH3J6H4mXTZ3YIlLcXopLZia_"
            };

            var token = tokenInfo.CreateFromRefreshToken(apiContext, new CreateFromRefreshTokenParameters());

            Assert.IsTrue(!string.IsNullOrEmpty(token.access_token));
        }
    }
}
