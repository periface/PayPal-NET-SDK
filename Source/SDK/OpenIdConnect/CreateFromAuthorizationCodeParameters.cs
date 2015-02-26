using System;
using System.Collections.Generic;

namespace PayPal.Api
{
    public class CreateFromAuthorizationCodeParameters : ClientCredentials
    {        
        /// <summary>
        /// Gets or sets the authorization code previously received from the server.
        /// </summary>
        public string Code
        {
            get { return (this.ContainerMap.ContainsKey("code") ? this.ContainerMap["code"] : null); }
            set { this.ContainerMap["code"] = value; }
        }

        /// <summary>
        /// Gets or sets the redirection endpoint that must match the one provided during the authorization request that ended in receiving the authorization code.
        /// </summary>
        public string RedirectUri
        {
            get { return (this.ContainerMap.ContainsKey("redirect_uri") ? this.ContainerMap["redirect_uri"] : null); }
            set { this.ContainerMap["redirect_uri"] = value; }
        }

        /// <summary>
        /// Gets or sets the token grant type.
        /// </summary>
        public string GrantType
        {
            get { return (this.ContainerMap.ContainsKey("grant_type") ? this.ContainerMap["grant_type"] : null); }
            set { this.ContainerMap["grant_type"] = value; }
        }

        /// <summary>
        /// Gets or sets the response type for the request.
        /// </summary>
        public string ResponseType { get; set; }

        /// <summary>
        /// Gets or sets the container map used to store custom parameters.
        /// </summary>
        public Dictionary<string, string> ContainerMap { get; set; }

        /// <summary>
        /// Initializes a new instance of the CreateFromAuthorizationCodeParameters class and defaults the GrantType to 'authorization_code'.
        /// </summary>
        public CreateFromAuthorizationCodeParameters()
        {
            this.ContainerMap = new Dictionary<string, string>();
            this.GrantType = "authorization_code";
        }

        #region Obsolete Methods
        /// <summary>
        /// Set the code
        /// </summary>
        /// <param name="code">Authorization code received from the server</param>
        [Obsolete("This method is obsolete. Use the Code property.", false)]
        public void SetCode(string code) { this.Code = code; }

        /// <summary>
        /// Set the Redirect URI
        /// </summary>
        /// <param name="redirectUri">Redirection endpoint that must match the one provided during the authorization request that ended in receiving the authorization code.</param>
        [Obsolete("This method is obsolete. Use the RedirectUri property.", false)]
        public void SetRedirectUri(string redirectUri) { this.RedirectUri = redirectUri; }

        /// <summary>
        /// Set the Grant Type
        /// </summary>
        /// <param name="grantType">Token grant type.</param>
        [Obsolete("This method is obsolete. Use the GrantType property.", false)]
        public void SetGrantType(string grantType) { this.GrantType = grantType; }
        #endregion
    }
}
