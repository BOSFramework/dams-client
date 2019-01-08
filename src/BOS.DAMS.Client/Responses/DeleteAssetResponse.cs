using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BOS.DAMS.Client.Responses
{
    public class DeleteAssetResponse : BOSWebServiceResponse
    {
        public DeleteAssetResponse(HttpStatusCode statusCode)
            : base(statusCode)
        {
        }
    }
}
