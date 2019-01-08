using BOS.DAMS.Client.ClientModels;
using System.Collections.Generic;
using System.Net;

namespace BOS.DAMS.Client.Responses
{
    public class AddAssetResponse<T> : BOSWebServiceResponse
    {
        public T Asset { get; set; }
        public List<DAMSError> Errors { get; set; }

        public AddAssetResponse(HttpStatusCode statusCode) 
            : base(statusCode)
        {
            Errors = new List<DAMSError>();
        }
    }
}
