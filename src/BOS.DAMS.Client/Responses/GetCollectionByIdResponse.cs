using BOS.DAMS.Client.ClientModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BOS.DAMS.Client.Responses
{
    public class GetCollectionByIdResponse<T> : BOSWebServiceResponse where T : IDAMSCollection
    {
        public T Collection { get; set; }
        public GetCollectionByIdResponse(HttpStatusCode statusCode) : base(statusCode)
        {
        }
    }
}
