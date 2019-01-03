using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BOS.DAMS.Client.Responses
{
    public class UpdateCollectionResponse : BOSWebServiceResponse
    {
        public UpdateCollectionResponse(HttpStatusCode statusCode)
            : base(statusCode)
        {
        }
    }
}
