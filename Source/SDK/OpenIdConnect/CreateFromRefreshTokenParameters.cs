using System;
using System.Collections.Generic;

namespace PayPal.Api
{
    public class CreateFromRefreshTokenParameters : ClientCredentials
    {
        /// <summary>
        /// Gets or sets a space-delimited string containing the resource URL endpoints that the client wants the token to be scoped for.
        /// </summary>
        public string Scope
        {
            get { return (this.ContainerMap.ContainsKey("scope") ? this.ContainerMap["scope"] : null); }
            set { this.ContainerMap["scope"] = value; }
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
        /// Gets or sets a previously received refresh token.
        /// </summary>
        public string RefreshToken
        {
            get { return (this.ContainerMap.ContainsKey("refresh_token") ? this.ContainerMap["refresh_token"] : null); }
            set { this.ContainerMap["refresh_token"] = value; }
        }

        /// <summary>
        /// Gets or sets the container map used to store custom parameters.
        /// </summary>
        public Dictionary<string, string> ContainerMap { get; set; }

        /// <summary>
        /// Initializes a new instance of the CreateFromRefreshTokenParameters class and defaults the GrantType to 'refresh_token'.
        /// </summary>
        public CreateFromRefreshTokenParameters()
        {
            this.ContainerMap = new Dictionary<string, string>();
            this.GrantType = "refresh_token";
        }

        #region Obsolete Methods
        /// <summary>
        /// Set the scope
        /// </summary>
        /// <param name="scope">Resource URL endpoints that the client wants the token to be scoped for.</param>
        [Obsolete("This method is obsolete. Use the Scope property.", false)]
        public void SetScope(string scope) { this.Scope = scope; }

        /// <summary>
        /// Set the Grant Type
        /// </summary>
        /// <param name="grantType"></param>
        [Obsolete("This method is obsolete. Use the GrantType property.", false)]
        public void SetGrantType(string grantType) { this.GrantType = grantType; }

        /// <summary>
        /// Set the Refresh Token
        /// </summary>
        /// <param name="refreshToken"></param>
        [Obsolete("This method is obsolete. Use the RefreshToken property.", false)]
        public void SetRefreshToken(string refreshToken) { this.RefreshToken = refreshToken; }
        #endregion
    }
}
