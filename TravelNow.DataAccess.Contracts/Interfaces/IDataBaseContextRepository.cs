
using MongoDB.Driver;

namespace TravelNow.DataAccess.Contracts.Interfaces;

public interface IDataBaseContextRepository
{
    Task<List<T>> GetAllDocumentsInCollectionAsync<T>(string collectionName);

    Task<T> GetFirstDocumentByIdAsync<T>(string collectionName, string id);

    Task<T> GetFirstDocumentByFilterAsync<T>(string collectionName, FilterDefinition<T> filter);
    
    Task<T> InsertDocumentAsync<T>(string collectionName, T document);

    Task<UpdateResult> UpdateDocumentByIdAsync<T>(string collectionName, string id, UpdateDefinition<T> definition);

    Task<UpdateResult> UpdateDocumentByFilterAsync<T>(string collectionName, UpdateDefinition<T> definition, FilterDefinition<T> filter);

    Task<bool> ExistsDocumentAsync<T>(string collectionName, FilterDefinition<T> filter);

    Task<bool> ExistsDocumentByIdAsync<T>(string collectionName, string id);
}
