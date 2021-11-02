using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.IO;

namespace LightManager.CLI
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory)!.FullName)
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Development.json", optional: true)
                .Build();

            CreateEventStoreSchema(configuration);
        }

        private static void CreateEventStoreSchema(IConfiguration configuration)
        {
            string cs = configuration.GetConnectionString("EventStore");

            NpgsqlConnectionStringBuilder csBuilder = new(cs)
            {
                SslMode = SslMode.Require,
                TrustServerCertificate = true
            };

            using NpgsqlConnection connection = new(csBuilder.ToString());
            connection.Open();

            using NpgsqlCommand cmd = new();
            cmd.Connection = connection;

            Console.WriteLine("Creating events table");
            cmd.CommandText = Queries.CreateEventsTable;
            cmd.ExecuteNonQuery();
            Console.WriteLine("Events table created");

            Console.WriteLine("Creating aggregates table");
            cmd.CommandText = Queries.CreateAggregatesTable;
            cmd.ExecuteNonQuery();
            Console.WriteLine("Aggregates table created");
        }

        private static class Queries
        {
            public const string CreateEventsTable = @"
                CREATE TABLE IF NOT EXISTS Events (
                    id uuid PRIMARY KEY NOT NULL,
                    aggregateId uuid NOT NULL,
                    time timestamp without time zone NOT NULL,
                    eventType VARCHAR(63) NOT NULL,
                    data VARCHAR(1000) NOT NULL
                )
            ";

            public const string CreateAggregatesTable = @"
                CREATE TABLE IF NOT EXISTS Aggregates (
                    id uuid PRIMARY KEY NOT NULL,
                    version INT NOT NULL,
                    creationTime timestamp without time zone NOT NULL,
                    modificationTime timestamp without time zone NOT NULL,
                    deletionTime timestamp without time zone NULL,
                    aggregateType VARCHAR(63) NOT NULL
                )

            ";
        }
    }
}
