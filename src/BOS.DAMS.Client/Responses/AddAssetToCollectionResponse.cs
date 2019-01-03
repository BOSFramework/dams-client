using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BOS.DAMS.Client.Responses
{
    public class AddAssetToCollectionResponse : BOSWebServiceResponse
    {
        public AddAssetToCollectionResponse(HttpStatusCode statusCode) 
            : base(statusCode)
        {
        }
    }
}
