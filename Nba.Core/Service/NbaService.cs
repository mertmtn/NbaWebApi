using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace Nba.Core
{
    public class NbaService
    {

        private readonly IMongoCollection<Teams> _teams;

        public NbaService(IDbSettings settings)
        {
            MongoClient client = new MongoClient(settings.ConnectionString);
            var db = client.GetDatabase(settings.Database);
            _teams = db.GetCollection<Teams>(settings.Collection);
        }

        public List<Teams> GetAll() => _teams.Find(teams => true).ToList();

        public Teams GetSingle(string id) => _teams.Find(teams => teams.Id == id).FirstOrDefault();

        public Teams Create(Teams team)
        { 
            //TODO: Validate team's property
            _teams.InsertOne(team);
            return team;
        }

        public long Delete(string id) => _teams.DeleteOne(teams => teams.Id == id).DeletedCount;

        public long Update(string id, Teams currentInfo) =>
            _teams.ReplaceOne(teams => teams.Id == id, currentInfo).ModifiedCount;
        
    }
}