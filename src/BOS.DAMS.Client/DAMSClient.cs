using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BOS.DAMS.Client.ClientModels;
using BOS.DAMS.Client.Responses;

namespace BOS.DAMS.Client
{
    public class DAMSClient : IDAMSClient
    {
        public Task<AddAssetResponse> AddAssetAsync<T>(IAsset asset, Guid collectionId) where T : IAsset
        {
            throw new NotImplementedException();
        }

        public Task<AddAssetToCollectionResponse> AddAssetToCollectionAsync(Guid assetId, Guid collectionId)
        {
            throw new NotImplementedException();
        }

        public Task<AddCollectionResponse> AddCollectionAsync<T>(IDAMSCollection collection) where T : IDAMSCollection
        {
            throw new NotImplementedException();
        }

        public Task<DeleteAssetResponse> DeleteAssetByIdAsync(Guid assetId)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteCollectionResponse> DeleteCollectionByIdAsync(Guid collectionId)
        {
            throw new NotImplementedException();
        }

        public Task<GetAssetByIdResponse<T>> GetAssetByIdAsync<T>(Guid assetId) where T : IAsset
        {
            throw new NotImplementedException();
        }

        public Task<GetCollectionByIdResponse<T>> GetCollectionByIdAsync<T>(Guid collectionId, bool includeAssets = true, bool filterDeleted = true) where T : IDAMSCollection
        {
            throw new NotImplementedException();
        }

        public Task<GetCollectionsResponse<T>> GetCollectionsAsync<T>(bool includeAssets = false, bool filterDeleted = true) where T : IDAMSCollection
        {
            throw new NotImplementedException();
        }

        public Task<RemoveAssetFromCollectionResponse> RemoveAssetFromCollectionAsync(Guid assetId, Guid collectionId)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateAssetResponse> UpdateAssetAsync<T>(IAsset asset) where T : IAsset
        {
            throw new NotImplementedException();
        }

        public Task<UpdateCollectionResponse> UpdateCollectionAsync<T>(IDAMSCollection collection) where T : IDAMSCollection
        {
            throw new NotImplementedException();
        }
    }
}
