using BOS.DAMS.Client.ClientModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BOS.DAMS.Client.Responses
{
    public class GetCollectionsResponse<T> : BOSWebServiceResponse where T : IDAMSCollection
    {
        public List<T> Collections { get; set; }

        public GetCollectionsResponse(HttpStatusCode statusCode) 
            : base(statusCode)
        {
            Collections = new List<T>();
        }
    }
}
