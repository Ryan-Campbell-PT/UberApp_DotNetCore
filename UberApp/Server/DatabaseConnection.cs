using System;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using UberApp.Shared;

namespace UberApp.Server
{
    public class DatabaseConnection
    {
        // The Azure Cosmos DB endpoint for running this sample.
        private static readonly string EndpointUri = ConfigurationManager.AppSettings["DBEndpointUri"];

        // The primary key for the Azure Cosmos account.
        private static readonly string PrimaryKey = ConfigurationManager.AppSettings["DBPrimaryKey"];

        // The Cosmos client instance
        private CosmosClient cosmosClient;

        // The database we will create
        private Database database;

        // The container we will create.
        private Container container;

        // The name of the database and container we will create
        private string databaseId = "Users";
        private string containerId = "Logins";


        public DatabaseConnection()
        {
            this.cosmosClient = new CosmosClient(EndpointUri, PrimaryKey, new CosmosClientOptions() { ApplicationName = "UberApp" });
        }

        public async Task ConnectToDatabase(string DbId, string DbContainer)
        {
            try
            {
                this.database = await this.cosmosClient.CreateDatabaseIfNotExistsAsync(DbId);
            }

            catch (Exception)
            {
                Console.WriteLine("Failed database");
            }

            try
            {
                this.container = await this.database.CreateContainerIfNotExistsAsync(DbContainer, "/partitionKey");
            }
            catch (Exception)
            {
                Console.WriteLine("Failed Container");
            }
        }
        
        public async Task SendInfo()
        {
            await ConnectToDatabase(DatabaseStrings.Users.Id, DatabaseStrings.Users.Container);

            var user = new TestUser
            {
                Email = "uber@test.com",
                Id = "UberTestId",
                Password = "UberTestPassword",
                PartitionKey = "UberTestPKey"
            };

            try
            {
                await this.container.ReadItemAsync<TestUser>(user.Id, new PartitionKey(user.PartitionKey));
            }
            catch (Exception)
            {
                await this.container.CreateItemAsync(user, new PartitionKey(user.PartitionKey));
            }
            
            
        }
        
        
    }
}