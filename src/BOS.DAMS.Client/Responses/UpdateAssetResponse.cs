using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BOS.DAMS.Client.Responses
{
    public class UpdateAssetResponse : BOSWebServiceResponse
    {
        public UpdateAssetResponse(HttpStatusCode statusCode)
            : base(statusCode)
        {
        }
    }
}
