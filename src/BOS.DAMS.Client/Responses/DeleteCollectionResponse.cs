using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BOS.DAMS.Client.Responses
{
    public class DeleteCollectionResponse : BOSWebServiceResponse
    {
        public DeleteCollectionResponse(HttpStatusCode statusCode)
            : base(statusCode)
        {
        }
    }
}
