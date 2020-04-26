using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nba.Core;
using System.Collections.Generic;

namespace Nba.Web.Test
{
    [TestClass]
    public class NbaTest
    {

        private NbaService nbaService;

        [TestInitialize]
        public void Setup()
        {
            IDbSettings settings = new MongoDbSettings
            {
                Collection = "Teams",
                ConnectionString = "mongodb://localhost:27017",
                Database = "NBADb"
            };
            nbaService = new NbaService(settings);
        }

        [TestMethod]
        public void GetAll_Should_Return_Teams()
        {
            List<Teams> teamList = nbaService.GetAll();
            Assert.AreNotEqual(0, teamList.Count);
        }

        [TestMethod]
        public void GetSingle_Should_Return_Team_ById()
        {
            string id = "5ea5a6d86fd98bec287af4d9";
            Teams team = nbaService.GetSingle(id);
            Assert.AreNotEqual(null, team);
            Assert.AreEqual("Bucks", team.SimpleName);
        }

        [TestMethod]
        public void Create_Should_Create_NewTeam()
        {
            Teams team = new Teams
            {
                Abbreviation = "SEA",
                Location = "Seattle",
                TeamName = "Seattle Supersonics",
                SimpleName = "Seattle",
                TeamId = 1610612765
            };


            var inserted = nbaService.Create(team);
            Assert.AreNotEqual(null, inserted.Id);
            Assert.AreEqual(24, inserted.Id.Length);
        }

        public void Update_Should_Change_Team_Info()
        {
            var id = "5ea5a6d86fd98bec287af4e6";
            Teams currentInfo = new Teams
            {
                Id = "5ea5a6d86fd98bec287af4e6",
                TeamId = 16106128000
            };
            var updatedCount = nbaService.Update(id, currentInfo);
            Assert.AreEqual(1, updatedCount);
        }



        [TestMethod]
        public void Delete_Should_Remove_Team()
        {
            var id = "5ea5dc0644ff751a687c7427";
            var deletedCount = nbaService.Delete(id);
            Assert.AreEqual(1, deletedCount);
        }
    }
}
