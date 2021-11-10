using Dapper;
using Newtonsoft.Json;
using System.Data;

namespace LightManager.Infrastructure.CQRS.Commands;

internal class CommandStore : ICommandStore
{
    private readonly IDbConnection dbConnection;

    public CommandStore(IDbConnection dbConnection)
    {
        this.dbConnection = dbConnection;
    }

    public async Task Add(Command command, CommandResult result)
    {
        CommandDataModel dataModel = new(
            Guid.NewGuid(),
            command.Time,
            command.CommandType,
            JsonConvert.SerializeObject(command.Data),
            result.Success
        );

        string query = @"INSERT INTO Commands(
                                id,
                                time,
                                commandType,
                                data,
                                success
                            )
                            VALUES(
                                @id,
                                @time,
                                @commandType,
                                @data,
                                @success
                            )";

        await dbConnection.ExecuteAsync(query, dataModel);
    }

    private record CommandDataModel(
        Guid Id,
        DateTime Time,
        string CommandType,
        string Data,
        bool success
    );
}
