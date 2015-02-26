using System;
using System.Collections.Generic;

namespace PayPal.Api
{
    public class APIContext
    {
        /// <summary>
        /// Request Id
        /// </summary>
        private string reqId;

        /// <summary>
        /// Explicit default constructor
        /// </summary>
        public APIContext()
        {
            this.HTTPHeaders = new Dictionary<string, string>();
            this.SdkVersion = new SDKVersion();
        }

        /// <summary>
        /// Access Token required for the call
        /// </summary>
        /// <param name="accessToken"></param>
        public APIContext(string accessToken) : this()
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentNullException("AccessToken cannot be null");
            }
            this.AccessToken = accessToken;
        }

        /// <summary>
        /// Access Token and Request Id required for the call
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="requestId"></param>
        public APIContext(string accessToken, string requestId) : this(accessToken)
        {
            if (string.IsNullOrEmpty(requestId))
            {
                throw new ArgumentNullException("RequestId cannot be null");
            }
            this.reqId = requestId;
        }

        /// <summary>
        /// Gets the Access Token
        /// </summary>
        public string AccessToken { get; private set; }

        /// <summary>
        /// Gets and sets the Mask Request Id
        /// </summary>
        public bool MaskRequestId { get; set; }
        
        /// <summary>
        /// Gets the Request Id
        /// </summary>
        public string RequestId
        {
            get
            {
                if (!this.MaskRequestId)
                {
                    if (string.IsNullOrEmpty(this.reqId))
                    {
                        this.reqId = Convert.ToString(Guid.NewGuid());
                    }
                    return this.reqId;
                }
                return null;
            }
        }

        /// <summary>
        /// Gets and sets the Dynamic Configuration
        /// </summary>
        public Dictionary<string, string> Config { get; set; }

        /// <summary>
        /// Gets and sets HTTP Headers
        /// </summary>
        public Dictionary<string, string> HTTPHeaders { get; set; }

        /// <summary>
        /// Get or sets the SDK version.
        /// </summary>
        public SDKVersion SdkVersion { get; set; }
    }
}
