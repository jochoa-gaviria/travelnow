﻿
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using TravelNow.Common.Enums;
using TravelNow.Common.Interfaces;
using TravelNow.DataAccess.Contracts.Interfaces;

namespace TravelNow.DataAccess.Context;

public class DataBaseContextRepository : IDataBaseContextRepository
{
    #region internals
    IMongoClient? _client;
    private readonly IAppConfigHelper _appConfig;
    public readonly IMongoDatabase? _database = null;

    #endregion internals

    #region constructor 
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="configuration"></param>
    public DataBaseContextRepository(IAppConfigHelper appConfig)
    {
        _appConfig = appConfig;
        string connectionString = _appConfig.DatabaseSettings[EDatabaseSettings.ConnectionString];
        string databaseName = _appConfig.DatabaseSettings[EDatabaseSettings.Database];

        _client = new MongoClient(connectionString);
        if (_client != null)
            _database = _client.GetDatabase(databaseName);
    }

    #endregion constructor

    #region methods

    public async Task<List<T>> GetAllDocumentsInCollectionAsync<T>(string collectionName) 
    {
        return await _database.GetCollection<T>(collectionName).Find(_ => true).ToListAsync();
    }

    public async Task<List<T>> GetAllDocumentsInCollectionByFilterAsync<T>(string collectionName, FilterDefinition<T> filer)
    {
        return await _database.GetCollection<T>(collectionName).Find(filer).ToListAsync();
    }

    public async Task<T> InsertDocumentAsync<T>(string collectionName, T document)
    {
        await _database.GetCollection<T>(collectionName).InsertOneAsync(document);
        return document;
    }

    public async Task<bool> ExistsDocumentAsync<T>(string collectionName, FilterDefinition<T> filter)
    {
        var document = await _database.GetCollection<T>(collectionName).FindAsync(filter);
        return document.Any();
    }

    public async Task<bool> ExistsDocumentByIdAsync<T>(string collectionName, string id)
    {
        FilterDefinition<T> filter = new BsonDocument("_id", new ObjectId(id));
        var document = await _database.GetCollection<T>(collectionName).FindAsync(filter);
        return document.Any();
    }

    public async Task<T> GetFirstDocumentByIdAsync<T>(string collectionName, string id)
    {
        FilterDefinition<T> filter = new BsonDocument("_id", new ObjectId(id));
        var response = await _database.GetCollection<T>(collectionName).FindAsync(filter);
        return await response.FirstOrDefaultAsync();
    }

    public async Task<T> GetFirstDocumentByFilterAsync<T>(string collectionName, FilterDefinition<T> filter)
    {
        var response = await _database.GetCollection<T>(collectionName).FindAsync(filter);
        return await response.FirstOrDefaultAsync();
    }

    public async Task<UpdateResult> UpdateDocumentByIdAsync<T>(string collectionName, string id, UpdateDefinition<T> definition)
    {
        FilterDefinition<T> filter = new BsonDocument("_id", new ObjectId(id));
        var result = await _database.GetCollection<T>(collectionName).UpdateOneAsync(filter, definition);
        return result;
    }

    public async Task<UpdateResult> UpdateDocumentByFilterAsync<T>(string collectionName, UpdateDefinition<T> definition, FilterDefinition<T> filter)
    {
        var result = await _database.GetCollection<T>(collectionName).UpdateOneAsync(filter, definition);
        return result;
    }

    #endregion methods
}
