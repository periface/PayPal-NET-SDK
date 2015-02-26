using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PayPal.Util;
using System.Collections.Generic;
using System;

namespace PayPal.Api
{
    public class Tokeninfo
    {
        /// <summary>
        /// OPTIONAL, if identical to the scope requested by the client otherwise, REQUIRED
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string scope { get; set; }

        /// <summary>
        /// The access token issued by the authorization server
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string access_token { get; set; }

        /// <summary>
        /// The refresh token, which can be used to obtain new access tokens using the same authorization grant as described in OAuth2.0 RFC6749 in Section 6
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string refresh_token { get; set; }

        /// <summary>
        /// The type of the token issued as described in OAuth2.0 RFC6749 (Section 7.1), value is case insensitive
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string token_type { get; set; }

        /// <summary>
        /// The lifetime in seconds of the access token
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int expires_in { get; set; }

        /// <summary>
        /// Explicit default constructor
        /// </summary>
        public Tokeninfo() { }

        /// <summary>
        /// Constructor overload
        /// </summary>
        public Tokeninfo(string accessToken, string tokenType, int expiresIn)
        {
            this.access_token = accessToken;
            this.token_type = tokenType;
            this.expires_in = expiresIn;
        }

        /// <summary>
        /// Creates an Access Token from an Authorization Code.
        /// <param name="createFromAuthorizationCodeParameters">Query parameters used for API call</param>
        /// </summary>
        public static Tokeninfo CreateFromAuthorizationCode(CreateFromAuthorizationCodeParameters createFromAuthorizationCodeParameters)
        {
            return CreateFromAuthorizationCode(null, createFromAuthorizationCodeParameters);
        }

        /// <summary>
        /// Creates an Access Token from an Authorization Code.
        /// <param name="apiContext">APIContext to be used for the call.</param>
        /// <param name="createFromAuthorizationCodeParameters">Query parameters used for API call</param>
        /// </summary>
        public static Tokeninfo CreateFromAuthorizationCode(APIContext apiContext, CreateFromAuthorizationCodeParameters createFromAuthorizationCodeParameters)
        {
            var resourcePath = "v1/identity/openidconnect/tokenservice";
            return CreateFromAuthorizationCodeParameters(apiContext, createFromAuthorizationCodeParameters, resourcePath);
        }

        /// <summary>
        /// Creates Access and Refresh Tokens from an Authorization Code for future payments.
        /// </summary>
        /// <param name="apiContext">APIContext to be used for the call.</param>
        /// <param name="parameters">Query parameters used for the API call.</param>
        /// <returns>A TokenInfo object containing the Access and Refresh Tokens.</returns>
        public static Tokeninfo CreateFromAuthorizationCodeForFuturePayments(APIContext apiContext, CreateFromAuthorizationCodeParameters parameters)
        {
            parameters.RedirectUri = "urn:ietf:wg:oauth:2.0:oob";
            parameters.ResponseType = "token";
            var resourcePath = "v1/oauth2/token";
            return CreateFromAuthorizationCodeParameters(apiContext, parameters, resourcePath);
        }

        /// <summary>
        /// Helper method for creating Access and Refresh Tokens from an Authorization Code.
        /// </summary>
        /// <param name="apiContext">APIContext to be used for the call.</param>
        /// <param name="parameters">Query parameters used for the API call.</param>
        /// <param name="resourcePath">The path to the REST API resource that will be requested.</param>
        /// <returns>A TokenInfo object containing the Access and Refresh Tokens.</returns>
        private static Tokeninfo CreateFromAuthorizationCodeParameters(APIContext apiContext, CreateFromAuthorizationCodeParameters parameters, string resourcePath)
        {
            // Validate the arguments to be used in the request
            ArgumentValidator.ValidateAndSetupAPIContext(apiContext);
            ArgumentValidator.Validate(parameters, "parameters");

            var payloadQueryParameters = new QueryParameters
            {
                { "grant_type", string.IsNullOrEmpty(parameters.GrantType) ? "authorization_code" : parameters.GrantType },
                { "response_type", parameters.ResponseType },
                { "redirect_uri", parameters.RedirectUri },
                { "code", parameters.Code }
            };
            var payload = payloadQueryParameters.ToUrlFormattedString(false);

            apiContext.HTTPHeaders[BaseConstants.ContentTypeHeader] = "application/x-www-form-urlencoded";
            apiContext.MaskRequestId = true;
            return PayPalResource.ConfigureAndExecute<Tokeninfo>(apiContext, HttpMethod.POST, resourcePath, payload);
        }

        /// <summary>
        /// Creates an Access Token from an Refresh Token.
        /// <param name="parameters">Query parameters used for API call</param>
        /// </summary>
        public Tokeninfo CreateFromRefreshToken(CreateFromRefreshTokenParameters parameters)
        {
            return CreateFromRefreshToken(null, parameters);
        }

        /// <summary>
        /// Creates an Access Token from an Refresh Token
        /// <param name="apiContext">APIContext to be used for the call</param>
        /// <param name="parameters">Query parameters used for API call</param>
        /// </summary>
        public Tokeninfo CreateFromRefreshToken(APIContext apiContext, CreateFromRefreshTokenParameters parameters)
        {
            // Validate the arguments to be used in the request
            ArgumentValidator.Validate(parameters, "parameters");

            // Verify the client ID and secret are defined.
            if (apiContext == null)
            {
                apiContext = new APIContext
                {
                    Config = ConfigManager.Instance.GetProperties()
                };

                if (!string.IsNullOrEmpty(parameters.ClientId))
                {
                    apiContext.Config[BaseConstants.ClientId] = parameters.ClientId;
                }
                if (!string.IsNullOrEmpty(parameters.ClientSecret))
                {
                    apiContext.Config[BaseConstants.ClientSecret] = parameters.ClientSecret;
                }
            }

            // Set the request payload
            var payloadQueryParameters = new QueryParameters
            {
                { "grant_type", string.IsNullOrEmpty(parameters.GrantType) ? "refresh_token" : parameters.GrantType },
                { "refresh_token", string.IsNullOrEmpty(parameters.RefreshToken) ? HttpUtility.UrlEncode(refresh_token) : parameters.RefreshToken },
                { "scope", parameters.Scope }
            };
            var payload = payloadQueryParameters.ToUrlFormattedString(false);

            // Configure and send the request.
            apiContext.HTTPHeaders[BaseConstants.ContentTypeHeader] = "application/x-www-form-urlencoded";
            apiContext.MaskRequestId = true;
            var resourcePath = "v1/identity/openidconnect/tokenservice";
            return PayPalResource.ConfigureAndExecute<Tokeninfo>(apiContext, HttpMethod.POST, resourcePath, payload);
        }
    }
}



