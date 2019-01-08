using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BOS.DAMS.Client.ClientModels;
using BOS.DAMS.Client.Responses;
using BOS.DAMS.Client.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace BOS.DAMS.Client
{
    public class DAMSClient : IDAMSClient
    {
        private readonly HttpClient _httpClient;
        private readonly DefaultContractResolver _contractResolver;

        public DAMSClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };
        }

        /// <summary>
        /// Add asset that implements IAsset.
        /// </summary>
        /// <typeparam name="T">Your implementation of IAsset.</typeparam>
        /// <param name="asset">Your asset to add.</param>
        /// <returns></returns>
        public async Task<AddAssetResponse<T>> AddAssetAsync<T>(IAsset asset) where T : IAsset
        {
            var request = new HttpRequestMessage(new HttpMethod("POST"), $"{_httpClient.BaseAddress}Assets?api-version=1.0");
            request.Content = new StringContent(JsonConvert.SerializeObject(asset, new JsonSerializerSettings() { ContractResolver = _contractResolver, Formatting = Formatting.Indented }), Encoding.UTF8, "application/json");
            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
            var addAssetResponse = new AddAssetResponse<T>(response.StatusCode);

            if (!addAssetResponse.IsSuccessStatusCode)
            {
                addAssetResponse.Errors.Add(new DAMSError((int)response.StatusCode));
                return addAssetResponse;
            }
            
            addAssetResponse.Asset = addAssetResponse.IsSuccessStatusCode ? JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result) : default(T);
            return addAssetResponse;
        }

        /// <summary>
        /// Add an asset to a collection with the corresponding Ids.
        /// </summary>
        /// <param name="assetId">The asset Id.</param>
        /// <param name="collectionId">The collection Id.</param>
        /// <returns></returns>
        public async Task<AddAssetToCollectionResponse> AddAssetToCollectionAsync(Guid assetId, Guid collectionId)
        {
            var payload = new { assetId };
            var response = await _httpClient.PostAsJsonAsync($"Collections({collectionId.ToString()})/AddAssetToCollection?api-version=1.0", payload).ConfigureAwait(false);

            return new AddAssetToCollectionResponse(response.StatusCode);
        }

        /// <summary>
        /// Add a collection.
        /// </summary>
        /// <typeparam name="T">Your implementation of ICollection.</typeparam>
        /// <param name="collection">The collection to add.</param>
        /// <returns></returns>
        public async Task<AddCollectionResponse<T>> AddCollectionAsync<T>(IDAMSCollection collection) where T : IDAMSCollection
        {
            var request = new HttpRequestMessage(new HttpMethod("POST"), $"{_httpClient.BaseAddress}Collections?api-version=1.0");
            request.Content = new StringContent(JsonConvert.SerializeObject(collection, new JsonSerializerSettings() { ContractResolver = _contractResolver, Formatting = Formatting.Indented }), Encoding.UTF8, "application/json");
            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);

            var addCollectionResponse = new AddCollectionResponse<T>(response.StatusCode);

            addCollectionResponse.Collection = addCollectionResponse.IsSuccessStatusCode ? JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result) : default(T);

            return addCollectionResponse;
        }

        /// <summary>
        /// Delete the asset with the given Id.
        /// </summary>
        /// <param name="assetId">The Id of the asset to delete.</param>
        /// <returns></returns>
        public async Task<DeleteAssetResponse> DeleteAssetByIdAsync(Guid assetId)
        {
            var response = await _httpClient.DeleteAsync($"/Assets?key={assetId.ToString()}?api-version=1.0");
            return new DeleteAssetResponse(response.StatusCode);
        }

        /// <summary>
        /// Delete the collection with the given Id.
        /// </summary>
        /// <param name="collectionId">The Id of the collection to delete.</param>
        /// <returns></returns>
        public async Task<DeleteCollectionResponse> DeleteCollectionByIdAsync(Guid collectionId)
        {
            var response = await _httpClient.DeleteAsync($"Collections?key={collectionId.ToString()}?api-version=1.0");
            return new DeleteCollectionResponse(response.StatusCode);
        }

        /// <summary>
        /// Retrieve the asset by its Id.
        /// </summary>
        /// <typeparam name="T">Your implementation of IAsset to deserialize to.</typeparam>
        /// <param name="assetId">The Id of the asset to retrieve.</param>
        /// <returns></returns>
        public async Task<GetAssetByIdResponse<T>> GetAssetByIdAsync<T>(Guid assetId) where T : IAsset
        {
            var response = await _httpClient.GetAsync($"Assets({assetId.ToString()})?api-version=1.0");
            var getAssetByIdResponse = new GetAssetByIdResponse<T>(response.StatusCode);

            getAssetByIdResponse.Asset = getAssetByIdResponse.IsSuccessStatusCode ? JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result) : default(T);
            return getAssetByIdResponse;
        }

        /// <summary>
        /// Retrieves a collection by Id. Includes the assets if requested.
        /// </summary>
        /// <typeparam name="T">Your implementation of IDAMSCollection to deserialize to.</typeparam>
        /// <typeparam name="T2">Your implementation of IAsset to deserialize a collection's assets to.</typeparam>
        /// <param name="collectionId">The id of the collection.</param>
        /// <param name="includeAssets">Whether to include the assets. Defaults to true for retrieving a single collection.</param>
        /// <param name="filterDeleted">Whether or not to filter any assets marked as deleted. Defaults to true.</param>
        /// <returns></returns>
        public async Task<GetCollectionByIdResponse<T>> GetCollectionByIdAsync<T, T2>(Guid collectionId, bool includeAssets = true, bool filterDeleted = true) where T : IDAMSCollection where T2 : IAsset
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

            getCollectionByIdResponse.Collection = getCollectionByIdResponse.IsSuccessStatusCode ?
                JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result, new DynamicAssetJsonConverter<T2>()) : default(T);
            return getCollectionByIdResponse;
        }

        /// <summary>
        /// Retrieve many collections.
        /// </summary>
        /// <typeparam name="T">Your implementation of IDAMSCollection to deserialize to.</typeparam>
        /// <typeparam name="T2">Your implementation of IAsset to deserialize to.</typeparam>
        /// <param name="includeAssets">Whether or not to include each collections' assets. Defaults to false.</param>
        /// <param name="filterDeleted">Whether or not to include deleted collections and assets.</param>
        /// <returns></returns>
        public async Task<GetCollectionsResponse<T>> GetCollectionsAsync<T, T2>(bool includeAssets = false, bool filterDeleted = true) where T : IDAMSCollection where T2 : IAsset
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
            getCollectionsResponse.Collections = getCollectionsResponse.IsSuccessStatusCode ? 
                JsonConvert.DeserializeObject<List<T>>(json["value"].ToString(), new DynamicAssetJsonConverter<T2>()) : new List<T>();
            return getCollectionsResponse;
        }

        /// <summary>
        /// Removes an asset from the specified collection.
        /// </summary>
        /// <param name="assetId">The Id of the asset to remove.</param>
        /// <param name="collectionId">The Id of the collection to remove.</param>
        /// <returns></returns>
        public async Task<RemoveAssetFromCollectionResponse> RemoveAssetFromCollectionAsync(Guid assetId, Guid collectionId)
        {
            var payload = new { assetId };
            var response = await _httpClient.PostAsJsonAsync($"Collections({collectionId.ToString()})/RemoveAssetFromCollection", payload);
            return new RemoveAssetFromCollectionResponse(response.StatusCode);
        }

        /// <summary>
        /// Update the asset completely. Any properties left off will not be saved.
        /// </summary>
        /// <typeparam name="T">Your implementation of IAsset to send.</typeparam>
        /// <param name="asset">The asset to update.</param>
        /// <returns></returns>
        public async Task<UpdateAssetResponse> UpdateAssetAsync<T>(IAsset asset) where T : IAsset
        {
            var request = new HttpRequestMessage(new HttpMethod("PUT"), $"{_httpClient.BaseAddress}Assets({asset.Id.ToString()})?api-version=1.0");
            request.Content = new StringContent(JsonConvert.SerializeObject(asset, new JsonSerializerSettings() { ContractResolver = _contractResolver, Formatting = Formatting.Indented }), Encoding.UTF8, "application/json");
            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
            return new UpdateAssetResponse(response.StatusCode);
        }

        /// <summary>
        /// Updates the collection completely. Any properties left off will not be saved.
        /// </summary>
        /// <typeparam name="T">Your implementation of IDAMSCollection to send.</typeparam>
        /// <param name="collection">The collection to update.</param>
        /// <returns></returns>
        public async Task<UpdateCollectionResponse> UpdateCollectionAsync<T>(IDAMSCollection collection) where T : IDAMSCollection
        {
            var request = new HttpRequestMessage(new HttpMethod("PUT"), $"{_httpClient.BaseAddress}Collections({collection.Id.ToString()})?api-version=1.0");
            request.Content = new StringContent(JsonConvert.SerializeObject(collection, new JsonSerializerSettings() { ContractResolver = _contractResolver, Formatting = Formatting.Indented }), Encoding.UTF8, "application/json");
            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
            return new UpdateCollectionResponse(response.StatusCode);
        }
    }
}
