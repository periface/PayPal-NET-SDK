using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PayPal.Api;

namespace PayPal.Sample
{
    public partial class LogInWithPayPalGetUserInfo : BaseSamplePage
    {
        protected override void RunSample()
        {
            this.flow.AddNewRequest("Get an access token using a refresh token");

            // Using the refresh token, first get an access token.
            //PayPal.Api.Tokeninfo.CreateFromAuthorizationCode()
            var tokenInfo = new Tokeninfo();
            var refreshTokenParameters = new CreateFromRefreshTokenParameters
            {
                RefreshToken = "CKrxHd8Dphj-GOtnY1EdnduPADuXshKlc4-y1z7uxKaKqsqldDELzpy8tx1gfA1Ivnq7ls12u3Th7DlOFQjXekD51wdXlx4G-mr7VH0P6EUYha_v5EkJIDVDXUL8qmSZmTD4TqfpcyC0v-WbE_OJXyeQ790-K3o_vugwKzF4VHMAaZAf"
            };
            var token = tokenInfo.CreateFromRefreshToken(refreshTokenParameters);

            this.flow.RecordActionSuccess("Access token: " + token.access_token);

            // Create a new APIContext that will be used to get the user credentials using the short-lived access token we just received.
            var apiContext = new APIContext(token.access_token);

            this.flow.AddNewRequest("Request user information");

            // Get the user information.
            var userInfo = PayPal.Api.OpenIdConnect.Userinfo.GetUserinfo(apiContext);

            this.flow.RecordActionSuccess("User information received: " + userInfo.user_id);
        }
    }
}