using BOS.DAMS.Client.ClientModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BOS.DAMS.Client.Responses
{
    public class GetAssetByIdResponse<T> : BOSWebServiceResponse where T : IAsset
    {
        public T Asset { get; set; }

        public GetAssetByIdResponse(HttpStatusCode statusCode) 
            : base(statusCode)
        {
        }
    }
}
