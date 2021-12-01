using DapperASPNetCore.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperASPNetCore.Repository
{
    public class CompanyService
    {
        
          
            private readonly IMongoCollection<MongoLog> _company;

            public CompanyService(ILogstoreDatabaseSettings settings)
            {
                var client = new MongoClient(settings.ConnectionString);
                var database = client.GetDatabase(settings.DatabaseName);

                _company = database.GetCollection<MongoLog>(settings.LogsCollectionName);
            }

            public List<MongoLog> Get() =>
                _company.Find(company => true).ToList();

            public MongoLog Get(string id) =>
                _company.Find<MongoLog>(log => log.Id == id).FirstOrDefault();

            public MongoLog Create(MongoLog log)
            {
                _company.InsertOne(log);
                return log;
            }
        //var createdLog = new MongoLog
        //{
        //    NameLog = company.Name
        //};

        //public void Update(int id, Company companyIn) =>
        //     _company.ReplaceOne(company => company.Id == id, companyIn);

        //public void Remove(Company companyIn) =>
        //    _company.DeleteOne(company => company.Id == companyIn.Id);

        //public void Remove(int id) =>
        //    _company.DeleteOne(company => company.Id == id);


    }
}
