using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BOS.DAMS.Client.Responses
{
    public class AddAssetResponse : BOSWebServiceResponse
    {
        public AddAssetResponse(HttpStatusCode statusCode) 
            : base(statusCode)
        {
        }
    }
}
