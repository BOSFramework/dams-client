using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BOS.DAMS.Client.Responses
{
    public class RemoveAssetFromCollectionResponse : BOSWebServiceResponse
    {
        public RemoveAssetFromCollectionResponse(HttpStatusCode statusCode) 
            : base(statusCode)
        {
        }
    }
}
