using BOS.DAMS.Client.ClientModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BOS.DAMS.Client.Responses
{
    public class AddCollectionResponse<T> : BOSWebServiceResponse where T : IDAMSCollection
    {
        public T Collection { get; set; }
        public List<DAMSError> Errors { get; set; }

        public AddCollectionResponse(HttpStatusCode statusCode) 
            : base(statusCode)
        {
            Errors = new List<DAMSError>();
        }
    }
}
