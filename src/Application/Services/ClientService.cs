using Domain.Costumers;
using Infra;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MySolution.Application.Services
{
    public class ClientService
    {
        private readonly IMongoCollection<Client> _clientsCollection;

        public ClientService(IOptions<MongoClusterDatabaseSettings> clientStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
            clientStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                clientStoreDatabaseSettings.Value.DatabaseName);

            _clientsCollection = mongoDatabase.GetCollection<Client>(
                clientStoreDatabaseSettings.Value.ClientCollectionName);
        }

        public async Task<Client> GetAsync(string document) => await _clientsCollection.Find(x => x.Document == document ).FirstOrDefaultAsync();

        public async Task CreateAsync(Client newClient) => await _clientsCollection.InsertOneAsync(newClient);

        public async Task<ReplaceOneResult> UpdateAsync(string id, Client updatedClient) => await _clientsCollection.ReplaceOneAsync(x => x._id == id, updatedClient);

    }
}
