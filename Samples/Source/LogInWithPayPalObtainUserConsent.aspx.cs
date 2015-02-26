using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PayPal.Api;
using PayPal.Sample.Utilities;

namespace PayPal.Sample
{
    public partial class LogInWithPayPalObtainUserConsent : BaseSamplePage
    {
        protected override void RunSample()
        {
            // ### Api Context
            // Pass in a `APIContext` object to authenticate 
            // the call and to send a unique request id 
            // (that ensures idempotency). The SDK generates
            // a request id if you do not pass one explicitly. 
            // See [Configuration.cs](/Source/Configuration.html) to know more about APIContext.
            var apiContext = Configuration.GetAPIContext();

            var success = Request.Params["success"];

            if (string.IsNullOrEmpty(success))
            {
                var baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/LogInWithPayPalObtainUserConsent.aspx?success=true";

                var redirectUrl = OpenIdConnectSession.GetRedirectUrl(baseURI, null, apiContext);

                this.flow.AddNewRequest("Redirect user to PayPal");
                this.flow.RecordRedirectUrl("Redirect to PayPal to obtain the user's consent.", redirectUrl);
                Session.Add("flow-lipp", this.flow);
            }
            else
            {
                this.flow = Session["flow-lipp"] as RequestFlow;
                this.RegisterSampleRequestFlow();

                this.flow.AddNewRequest("Callback from Identity service received");

                var code = Request.Params["code"];
                if(!string.IsNullOrEmpty(code))
                {
                    this.flow.RecordActionSuccess("User permission granted!  Refresh token is " + code);
                }
                else
                {
                    // If there was no code, then an error occurred.
                    var error = Request.Params["error"];
                    throw new Exception(string.Format("Error: {0}\nError Description: {1}\nMore Information: {2}", error, Request.Params["error_description"], Request.Params["error_uri"]));
                }
            }
        }
    }
}