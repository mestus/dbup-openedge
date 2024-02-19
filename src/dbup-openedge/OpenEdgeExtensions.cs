using System;
using System.Data.Odbc;
using DbUp.Builder;
using DbUp.Engine.Output;
using DbUp.Engine.Transactions;

namespace DbUp.OpenEdge;

/// <summary>
/// Configuration extension methods for OpenEdge.
/// </summary>
public static class OpenEdgeExtensions
{
    /// <summary>
    /// Creates an upgrader for OpenEdge databases.
    /// </summary>
    /// <param name="supported">Fluent helper type.</param>
    /// <param name="connectionString">OpenEdge ODBC database connection string.</param>
    /// <returns>
    /// A builder for a database upgrader designed for OpenEdge databases.
    /// </returns>
    public static UpgradeEngineBuilder OpenEdgeDatabase(this SupportedDatabases supported, string connectionString)
        => OpenEdgeDatabase(supported, connectionString, null);

    /// <summary>
    /// Creates an upgrader for OpenEdge databases.
    /// </summary>
    /// <param name="supported">Fluent helper type.</param>
    /// <param name="connectionString">OpenEdge ODBC database connection string.</param>
    /// <param name="schema">The schema in which to check for changes</param>
    /// <returns>
    /// A builder for a database upgrader designed for OpenEdge databases.
    /// </returns>
    public static UpgradeEngineBuilder OpenEdgeDatabase(this SupportedDatabases supported, string connectionString, string schema)
        => OpenEdgeDatabase(new OpenEdgeConnectionManager(connectionString), schema);

    /// <summary>
    /// Creates an upgrader for OpenEdge databases.
    /// </summary>
    /// <param name="supported">Fluent helper type.</param>
    /// <param name="connectionManager">The <see cref="OpenEdgeConnectionManager"/> to be used during a database upgrade.</param>
    /// <returns>
    /// A builder for a database upgrader designed for OpenEdge databases.
    /// </returns>
    public static UpgradeEngineBuilder OpenEdgeDatabase(this SupportedDatabases supported, IConnectionManager connectionManager)
        => OpenEdgeDatabase(connectionManager);

    /// <summary>
    /// Creates an upgrader for OpenEdge databases.
    /// </summary>
    /// <param name="connectionManager">The <see cref="OpenEdgeConnectionManager"/> to be used during a database upgrade.</param>
    /// <returns>
    /// A builder for a database upgrader designed for OpenEdge databases.
    /// </returns>
    public static UpgradeEngineBuilder OpenEdgeDatabase(IConnectionManager connectionManager)
        => OpenEdgeDatabase(connectionManager, null);

    /// <summary>
    /// Creates an upgrader for OpenEdge databases.
    /// </summary>
    /// <param name="connectionManager">The <see cref="OpenEdgeConnectionManager"/> to be used during a database upgrade.</param>
    /// <param name="schema">The schema in which to check for changes</param>
    /// <returns>
    /// A builder for a database upgrader designed for OpenEdge databases.
    /// </returns>
    public static UpgradeEngineBuilder OpenEdgeDatabase(IConnectionManager connectionManager, string schema)
    {
        var builder = new UpgradeEngineBuilder();
        builder.Configure(c => c.ConnectionManager = connectionManager);
        builder.Configure(c => c.ScriptExecutor = new OpenEdgeScriptExecutor(() => c.ConnectionManager, () => c.Log, schema, () => c.VariablesEnabled, c.ScriptPreprocessors, () => c.Journal));
        builder.Configure(c => c.Journal = new OpenEdgeJournal(() => c.ConnectionManager, () => c.Log, schema, "SchemaVersions"));
        builder.WithPreprocessor(new OpenEdgePreprocessor());
        return builder;
    }

    /// <summary>
    /// Ensures that the database specified in the connection string exists.
    /// </summary>
    /// <param name="supported">Fluent helper type.</param>
    /// <param name="connectionString">The connection string.</param>
    /// <returns></returns>
    public static void OpenEdgeDatabase(this SupportedDatabasesForEnsureDatabase supported, string connectionString)
    {
        OpenEdgeDatabase(supported, connectionString, new ConsoleUpgradeLog());
    }

    /// <summary>
    /// Ensures that the database specified in the connection string exists.
    /// </summary>
    /// <param name="supported">Fluent helper type.</param>
    /// <param name="connectionString">The connection string.</param>
    /// <param name="logger">The <see cref="IUpgradeLog"/> used to record actions.</param>
    /// <returns></returns>
    public static void OpenEdgeDatabase(this SupportedDatabasesForEnsureDatabase supported, string connectionString, IUpgradeLog logger)
    {
        if (supported == null) throw new ArgumentNullException("supported");

        if (string.IsNullOrEmpty(connectionString) || connectionString.Trim() == string.Empty)
        {
            throw new ArgumentNullException("connectionString");
        }

        if (logger == null) throw new ArgumentNullException("logger");

        using (var connection = new OdbcConnection(connectionString))
        {
            connection.Open();

            // TODO, not sure if we can test this in OpenEdge
        }
    }

    /// <summary>
    /// Tracks the list of executed scripts in an OpenEdge table.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="schema">The schema.</param>
    /// <param name="table">The table.</param>
    /// <returns></returns>
    public static UpgradeEngineBuilder JournalToOpenEdgeTable(this UpgradeEngineBuilder builder, string schema, string table)
    {
        builder.Configure(c => c.Journal = new OpenEdgeJournal(() => c.ConnectionManager, () => c.Log, schema, table));
        return builder;
    }
}
