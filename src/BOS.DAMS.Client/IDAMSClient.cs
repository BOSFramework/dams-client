using BOS.DAMS.Client.ClientModels;
using BOS.DAMS.Client.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BOS.DAMS.Client
{
    public interface IDAMSClient
    {
        Task<AddCollectionResponse<T>> AddCollectionAsync<T>(IDAMSCollection collection) where T : IDAMSCollection;
        Task<AddAssetResponse> AddAssetAsync<T>(IAsset asset) where T : IAsset;
        Task<GetAssetByIdResponse<T>> GetAssetByIdAsync<T>(Guid assetId) where T : IAsset;
        Task<AddAssetToCollectionResponse> AddAssetToCollectionAsync(Guid assetId, Guid collectionId);
        Task<RemoveAssetFromCollectionResponse> RemoveAssetFromCollectionAsync(Guid assetId, Guid collectionId);
        Task<UpdateAssetResponse> UpdateAssetAsync<T>(IAsset asset) where T : IAsset;
        Task<DeleteAssetResponse> DeleteAssetByIdAsync(Guid assetId);
        Task<GetCollectionByIdResponse<T>> GetCollectionByIdAsync<T>(Guid collectionId, bool includeAssets = true, bool filterDeleted = true) where T : IDAMSCollection;
        Task<GetCollectionsResponse<T>> GetCollectionsAsync<T, T2>(bool includeAssets = false, bool filterDeleted = true) where T : IDAMSCollection where T2 : IAsset;
        Task<UpdateCollectionResponse> UpdateCollectionAsync<T>(IDAMSCollection collection) where T : IDAMSCollection;
        Task<DeleteCollectionResponse> DeleteCollectionByIdAsync(Guid collectionId);
    }
}
