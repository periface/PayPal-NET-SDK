using System;
using System.Collections.Generic;
using System.Text;

namespace PayPal.Api
{
    public abstract class ClientCredentials
    {
        /// <summary>
        /// Gets or sets the client ID.
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the client secret.
        /// </summary>
        public string ClientSecret { get; set; }

        #region Obsolete Properties and Methods
        /// <summary>
        /// Gets or sets the client ID.
        /// </summary>
        [Obsolete("This method is obsolete. Use the ClientId property.", false)]
        public string clientId { get { return this.ClientId; } set { this.ClientId = value; } }

        /// <summary>
        /// Gets or sets the client secret.
        /// </summary>
        [Obsolete("This method is obsolete. Use the ClientSecret property.", false)]
        public string clientSecret { get { return this.ClientSecret; } set { this.ClientSecret = value; } }

        /// <summary>
        /// Set the Client ID
        /// </summary>
        /// <param name="clientId"></param>
        [Obsolete("This method is obsolete. Use the ClientId property.", false)]
        public void setClientId(string clientId) { this.ClientId = clientId; }

        /// <summary>
        /// Set the Client Secret
        /// </summary>
        /// <param name="clientSecret"></param>
        [Obsolete("This method is obsolete. Use the ClientSecret property.", false)]
        public void setClientSecret(string clientSecret) { this.ClientSecret = clientSecret; }

        /// <summary>
        /// Returns the Client ID
        /// </summary>
        /// <returns></returns>
        [Obsolete("This method is obsolete. Use the ClientId property.", false)]
        public string getClientId() { return this.ClientId; }

        /// <summary>
        /// Returns the Client Secret
        /// </summary>
        /// <returns></returns>
        [Obsolete("This method is obsolete. Use the ClientSecret property.", false)]
        public string getClientSecret() { return this.ClientSecret; }
        #endregion

    }
}
