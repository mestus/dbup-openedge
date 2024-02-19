using System;
using System.Data;
using DbUp.Engine;
using DbUp.Engine.Output;
using DbUp.Engine.Transactions;
using DbUp.Support;

namespace DbUp.OpenEdge;

/// <summary>
/// An implementation of the <see cref="IJournal"/> interface which tracks version numbers for an
/// OpenEdge database using a table called SchemaVersions.
/// </summary>
public class OpenEdgeJournal : TableJournal
{
    /// <summary>
    /// Creates a new OpenEdge table journal.
    /// </summary>
    /// <param name="connectionManager">The OpenEdge connection manager.</param>
    /// <param name="logger">The upgrade logger.</param>
    /// <param name="schema">The name of the schema the journal is stored in.</param>
    /// <param name="tableName">The name of the journal table.</param>
    public OpenEdgeJournal(Func<IConnectionManager> connectionManager, Func<IUpgradeLog> logger, string schema,
        string tableName)
        : base(connectionManager, logger, new OpenEdgeObjectParser(), schema, tableName)
    {
    }
    
    /// <summary>
    /// Creates a new OpenEdge table journal in the PUB schema.
    /// </summary>
    /// <param name="connectionManager">The PostgreSQL connection manager.</param>
    /// <param name="logger">The upgrade logger.</param>
    /// <param name="tableName">The name of the journal table.</param>
    public OpenEdgeJournal(Func<IConnectionManager> connectionManager, Func<IUpgradeLog> logger, string tableName)
        : this(connectionManager, logger, "PUB", tableName)
    {
    }

    protected override IDbCommand GetInsertScriptCommand(Func<IDbCommand> dbCommandFactory, SqlScript script)
    {
        // Use positional parameters instead of named parameters
        var command = dbCommandFactory();

        var scriptNameParam = command.CreateParameter();
        scriptNameParam.Value = script.Name;
        command.Parameters.Add(scriptNameParam);

        var appliedParam = command.CreateParameter();
        appliedParam.Value = DateTime.Now;
        command.Parameters.Add(appliedParam);

        command.CommandText = GetInsertJournalEntrySql("?", "?");
        command.CommandType = CommandType.Text;
        return command;
    }

    protected override string GetInsertJournalEntrySql(string scriptName, string applied)
    {
        return $"insert into {FqSchemaTableName} (ScriptName, Applied) values ({scriptName}, {applied})";
    }

    protected override string GetJournalEntriesSql()
    {
        return $"select ScriptName from {FqSchemaTableName} order by ScriptName";
    }

    protected override string CreateSchemaTableSql(string quotedPrimaryKeyName)
    {
        return
            $@"

CREATE TABLE {FqSchemaTableName}
(
    ""scriptname"" character varying(255) NOT NULL PRIMARY KEY,
    ""applied"" timestamp NOT NULL
)";
    }
    
    protected override string DoesTableExistSql()
    {
        return string.IsNullOrEmpty(SchemaTableSchema)
            ? string.Format("select COUNT(*) from SYSPROGRESS.SYSTABLES where TBL = '{0}'", UnquotedSchemaTableName)
            : string.Format("select COUNT(*) from SYSPROGRESS.SYSTABLES where TBL = '{0}' and OWNER = '{1}'", UnquotedSchemaTableName, SchemaTableSchema);
    }
}
