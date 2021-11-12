using Npgsql;

namespace LightManager.CLI.EventStore;

internal static class EventStoreManagement
{
    public static async Task CreateSchema(IConfiguration configuration, IServiceProvider services)
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

        Console.WriteLine("Creating commands table");
        cmd.CommandText = EventStoreQueries.CreateEventsTable;
        await cmd.ExecuteNonQueryAsync();
        Console.WriteLine("Commands table created");

        Console.WriteLine("Creating events table");
        cmd.CommandText = EventStoreQueries.CreateCommandsTable;
        await cmd.ExecuteNonQueryAsync();
        Console.WriteLine("Events table created");

        Console.WriteLine("Creating aggregates table");
        cmd.CommandText = EventStoreQueries.CreateAggregatesTable;
        await cmd.ExecuteNonQueryAsync();
        Console.WriteLine("Aggregates table created");
    }


    private static class EventStoreQueries
    {
        public const string CreateCommandsTable = @"
            CREATE TABLE IF NOT EXISTS Commands (
                id uuid PRIMARY KEY NOT NULL,
                time timestamp without time zone NOT NULL,
                commandType VARCHAR(63) NOT NULL,
                data VARCHAR(1000) NOT NULL,
                success BOOLEAN NOT NULL
            )
        ";

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
