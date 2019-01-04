﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BOS.DAMS.Client.ClientModels;
using BOS.DAMS.Client.Responses;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BOS.DAMS.Client
{
    public class DAMSClient : IDAMSClient
    {
        private readonly HttpClient _httpClient;

        public DAMSClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AddAssetResponse> AddAssetAsync<T>(IAsset asset) where T : IAsset
        {
            var response = await _httpClient.PostAsJsonAsync("Assets?api-version=1.0", asset).ConfigureAwait(false);

            return new AddAssetResponse(response.StatusCode);
        }

        public async Task<AddAssetToCollectionResponse> AddAssetToCollectionAsync(Guid assetId, Guid collectionId)
        {
            var payload = new { assetId };
            var response = await _httpClient.PostAsJsonAsync($"Collections({collectionId.ToString()})/AddAssetToCollection?api-version=1.0", payload).ConfigureAwait(false);

            return new AddAssetToCollectionResponse(response.StatusCode);
        }

        public async Task<AddCollectionResponse<T>> AddCollectionAsync<T>(IDAMSCollection collection) where T : IDAMSCollection
        {
            var response = await _httpClient.PostAsJsonAsync("Collections?api-version=1.0", collection).ConfigureAwait(false);

            var addCollectionResponse = new AddCollectionResponse<T>(response.StatusCode);

            addCollectionResponse.Collection = addCollectionResponse.IsSuccessStatusCode ? JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result) : default(T);

            return addCollectionResponse;
        }

        public async Task<DeleteAssetResponse> DeleteAssetByIdAsync(Guid assetId)
        {
            var response = await _httpClient.DeleteAsync($"/Assets?key={assetId.ToString()}?api-version=1.0");
            return new DeleteAssetResponse(response.StatusCode);
        }

        public async Task<DeleteCollectionResponse> DeleteCollectionByIdAsync(Guid collectionId)
        {
            var response = await _httpClient.DeleteAsync($"Collections?key={collectionId.ToString()}?api-version=1.0");
            return new DeleteCollectionResponse(response.StatusCode);
        }

        public async Task<GetAssetByIdResponse<T>> GetAssetByIdAsync<T>(Guid assetId) where T : IAsset
        {
            var response = await _httpClient.GetAsync($"Assets({assetId.ToString()})?api-version=1.0");
            var getAssetByIdResponse = new GetAssetByIdResponse<T>(response.StatusCode);

            getAssetByIdResponse.Asset = getAssetByIdResponse.IsSuccessStatusCode ? JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result) : default(T);
            return getAssetByIdResponse;
        }

        public async Task<GetCollectionByIdResponse<T>> GetCollectionByIdAsync<T>(Guid collectionId, bool includeAssets = true, bool filterDeleted = true) where T : IDAMSCollection
        {

            var response = new HttpResponseMessage();

            if (includeAssets && filterDeleted)
            {
                response = await _httpClient.GetAsync($"Collections({collectionId.ToString()})?$expand=Assets($filter=Deleted eq false)&api-version=1.0").ConfigureAwait(false);
            }
            else if (includeAssets && !filterDeleted)
            {
                response = await _httpClient.GetAsync($"Collections({collectionId.ToString()})?$expand=Assets&api-version=1.0").ConfigureAwait(false);
            }
            else if (!includeAssets && filterDeleted)
            {
                response = await _httpClient.GetAsync($"Collections({collectionId.ToString()})?api-version=1.0").ConfigureAwait(false);
            }
            else
            {
                response = await _httpClient.GetAsync($"Collections({collectionId.ToString()})?api-version=1.0").ConfigureAwait(false);
            }

            var getCollectionByIdResponse = new GetCollectionByIdResponse<T>(response.StatusCode);
            getCollectionByIdResponse.Collection = getCollectionByIdResponse.IsSuccessStatusCode ? JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result) : default(T);

            return getCollectionByIdResponse;
        }

        public async Task<GetCollectionsResponse<T>> GetCollectionsAsync<T>(bool includeAssets = false, bool filterDeleted = true) where T : IDAMSCollection
        {
            var response = new HttpResponseMessage();

            if (includeAssets && filterDeleted)
            {
                response = await _httpClient.GetAsync("Collections?$filter=Deleted eq false&expand=Assets($filter=Deleted eq false)&api-version=1.0").ConfigureAwait(false);
            }
            else if (includeAssets && !filterDeleted)
            {
                response = await _httpClient.GetAsync("Collections?$expand=Assets&api-version=1.0").ConfigureAwait(false);
            }
            else if (!includeAssets && filterDeleted)
            {
                response = await _httpClient.GetAsync("Collections?$filter=Deleted eq false&api-version=1.0").ConfigureAwait(false);
            }
            else
            {
                response = await _httpClient.GetAsync("Collections?api-version=1.0").ConfigureAwait(false);
            }

            var getCollectionsResponse = new GetCollectionsResponse<T>(response.StatusCode);

            JObject json = JsonConvert.DeserializeObject<JObject>(response.Content.ReadAsStringAsync().Result);
            getCollectionsResponse.Collections = getCollectionsResponse.IsSuccessStatusCode ? JsonConvert.DeserializeObject<List<T>>(json["value"].ToString()) : new List<T>();
            return getCollectionsResponse;
        }

        public async Task<RemoveAssetFromCollectionResponse> RemoveAssetFromCollectionAsync(Guid assetId, Guid collectionId)
        {
            var payload = new { assetId };
            var response = await _httpClient.PostAsJsonAsync($"Collections({collectionId.ToString()})/RemoveAssetFromCollection", payload);
            return new RemoveAssetFromCollectionResponse(response.StatusCode);
        }

        public async Task<UpdateAssetResponse> UpdateAssetAsync<T>(IAsset asset) where T : IAsset
        {
            var response = await _httpClient.PutAsJsonAsync($"Assets({asset.Id.ToString()})?api-version=1.0", asset);
            return new UpdateAssetResponse(response.StatusCode);
        }

        public async Task<UpdateCollectionResponse> UpdateCollectionAsync<T>(IDAMSCollection collection) where T : IDAMSCollection
        {
            var response = await _httpClient.PutAsJsonAsync($"Collections({collection.Id})?api-version=1.0", collection);
            return new UpdateCollectionResponse(response.StatusCode);
        }
    }
}
