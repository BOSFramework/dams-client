using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BOS.DAMS.Client.Responses
{
    public class AddCollectionResponse : BOSWebServiceResponse
    {
        public AddCollectionResponse(HttpStatusCode statusCode) 
            : base(statusCode)
        {
        }
    }
}
