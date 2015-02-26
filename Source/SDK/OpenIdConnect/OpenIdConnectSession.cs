using System.Collections.Generic;
using System.Text;
using System.Web;

namespace PayPal.Api
{
    public class OpenIdConnectSession
    {
        /// <summary>
        /// Returns the PayPal URL to which the user must be redirected to start the authentication / authorization process
        /// </summary>
        /// <param name="redirectUri">Uri on merchant website to where the user must be redirected to post paypal login</param>
        /// <param name="scope">The access privilges that you are requesting for from the user. Pass empty array for all scopes. See https://developer.paypal.com/webapps/developer/docs/classic/loginwithpaypal/ht_OpenIDConnect/#parameters for more information</param>
        /// <param name="apiContext">APIContext containing the config and client credentials to use.</param>
        /// <returns>A string containing the PayPal URL to redirect the user to begin the authorization process.</returns>
        public static string GetRedirectUrl(string redirectUri, List<string> scope, APIContext apiContext)
        {
            var config = GetConfigWithDefaults(apiContext);
            var baseUrl = GetOpenIdRedirectBaseUrl(config);

            if (scope == null || scope.Count <= 0)
            {
                scope = new List<string>
                {
                    "openid",
                    "profile",
                    "address",
                    "email",
                    "phone",
                    "https://uri.paypal.com/services/paypalattributes"
                };
            } 
            else if (!scope.Contains("openid"))
            {
                scope.Add("openid");
            }

            var builder = new StringBuilder();
            builder.Append("client_id=").Append(HttpUtility.UrlEncode((config.ContainsKey(BaseConstants.ClientId)) ? config[BaseConstants.ClientId] : string.Empty));
            builder.Append("&response_type=code&scope=");
            builder.Append(HttpUtility.UrlEncode(string.Join(" ", scope.ToArray())));
            builder.Append("&redirect_uri=").Append(HttpUtility.UrlEncode(redirectUri));
            return string.Format("{0}/v1/authorize?{1}", baseUrl, builder.ToString());
        }

        /// <summary>
        /// Returns the URL to which the user must be redirected to logout from the OpenId provider (i.e., PayPal)
        /// </summary>
        /// <param name="redirectUri">Redirect URI for your site where you want PayPal to redirect to after the user has completed logging out.</param>
        /// <param name="idToken">id_token from the Tokeninfo object</param>
        /// <param name="apiContext">APIContext containing the config to use.</param>
        /// <returns>A string containing the PayPal URL to redirect the user to logout.</returns>
        public static string GetLogoutUrl(string redirectUri, string idToken, APIContext apiContext)
        {
            var config = GetConfigWithDefaults(apiContext);
            var baseUrl = GetOpenIdRedirectBaseUrl(config);
            
            var builder = new StringBuilder();
            builder.Append("id_token=").Append(HttpUtility.UrlEncode(idToken));
            builder.Append("&redirect_uri=").Append(HttpUtility.UrlEncode(redirectUri));
            builder.Append("&logout=true");
            return string.Format("{0}/v1/endsession?{1}", baseUrl, builder.ToString());
        }

        /// <summary>
        /// Helper method for getting the config to use for constructing the openid authorization and logout URLs.
        /// </summary>
        /// <param name="apiContext"></param>
        /// <returns></returns>
        private static Dictionary<string, string> GetConfigWithDefaults(APIContext apiContext)
        {
            return ConfigManager.GetConfigWithDefaults(apiContext == null ? ConfigManager.Instance.GetProperties() : apiContext.Config);
        }

        /// <summary>
        /// Helper method for getting the openid redirect URL.
        /// </summary>
        /// <param name="config">Configuration map specifying whether the URL will be for Sandbox or Live.</param>
        /// <returns>A string containing the openid redirect URL.</returns>
        private static string GetOpenIdRedirectBaseUrl(Dictionary<string, string> config)
        {
            // Default is the Sandbox URL
            var url = BaseConstants.OpenIdRedirectUriSandbox;

            if (config != null)
            {
                // Check to see if the config has an override for the URL.
                if (config.ContainsKey(BaseConstants.OpenIdRedirectUri))
                {
                    url = config[BaseConstants.OpenIdRedirectUri];
                }
                // Otherwise, check to see if Live mode is enabled.
                else if (config.ContainsKey(BaseConstants.ApplicationModeConfig) && config[BaseConstants.ApplicationModeConfig] == BaseConstants.LiveMode)
                {
                    url = BaseConstants.OpenIdRedirectUriLive;
                }
            }

            // Remove any trailing slash on the URL.
            if (url.EndsWith("/"))
            {
                url= url.Substring(0, url.Length - 1);
            }

            return url;
        }
    }
}
