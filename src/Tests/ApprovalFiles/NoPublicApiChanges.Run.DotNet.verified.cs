
namespace DbUp.OpenEdge
{
    public class OpenEdgeConnectionManager : DbUp.Engine.Transactions.DatabaseConnectionManager, DbUp.Engine.Transactions.IConnectionManager
    {
        public OpenEdgeConnectionManager(string connectionString) { }
        public override System.Collections.Generic.IEnumerable<string> SplitScriptIntoCommands(string scriptContents) { }
    }
    public static class OpenEdgeExtensions
    {
        public static DbUp.Builder.UpgradeEngineBuilder JournalToOpenEdgeTable(this DbUp.Builder.UpgradeEngineBuilder builder, string schema, string table) { }
        public static DbUp.Builder.UpgradeEngineBuilder OpenEdgeDatabase(DbUp.Engine.Transactions.IConnectionManager connectionManager) { }
        public static DbUp.Builder.UpgradeEngineBuilder OpenEdgeDatabase(this DbUp.Builder.SupportedDatabases supported, string connectionString) { }
        public static DbUp.Builder.UpgradeEngineBuilder OpenEdgeDatabase(this DbUp.Builder.SupportedDatabases supported, DbUp.Engine.Transactions.IConnectionManager connectionManager) { }
        public static DbUp.Builder.UpgradeEngineBuilder OpenEdgeDatabase(DbUp.Engine.Transactions.IConnectionManager connectionManager, string schema) { }
        public static DbUp.Builder.UpgradeEngineBuilder OpenEdgeDatabase(this DbUp.Builder.SupportedDatabases supported, string connectionString, string schema) { }
        public static void OpenEdgeDatabase(this DbUp.SupportedDatabasesForEnsureDatabase supported, string connectionString) { }
        public static void OpenEdgeDatabase(this DbUp.SupportedDatabasesForEnsureDatabase supported, string connectionString, DbUp.Engine.Output.IUpgradeLog logger) { }
    }
    public class OpenEdgeJournal : DbUp.Support.TableJournal, DbUp.Engine.IJournal
    {
        public OpenEdgeJournal(System.Func<DbUp.Engine.Transactions.IConnectionManager> connectionManager, System.Func<DbUp.Engine.Output.IUpgradeLog> logger, string tableName) { }
        public OpenEdgeJournal(System.Func<DbUp.Engine.Transactions.IConnectionManager> connectionManager, System.Func<DbUp.Engine.Output.IUpgradeLog> logger, string schema, string tableName) { }
        protected override string CreateSchemaTableSql(string quotedPrimaryKeyName) { }
        protected override string DoesTableExistSql() { }
        protected override string GetInsertJournalEntrySql(string scriptName, string applied) { }
        protected override System.Data.IDbCommand GetInsertScriptCommand(System.Func<System.Data.IDbCommand> dbCommandFactory, DbUp.Engine.SqlScript script) { }
        protected override string GetJournalEntriesSql() { }
    }
    public class OpenEdgeObjectParser : DbUp.Support.SqlObjectParser, DbUp.Engine.ISqlObjectParser
    {
        public OpenEdgeObjectParser() { }
    }
    public class OpenEdgePreprocessor : DbUp.Engine.IScriptPreprocessor
    {
        public OpenEdgePreprocessor() { }
        public string Process(string contents) { }
    }
    public class OpenEdgeScriptExecutor : DbUp.Support.ScriptExecutor, DbUp.Engine.IScriptExecutor
    {
        public OpenEdgeScriptExecutor(System.Func<DbUp.Engine.Transactions.IConnectionManager> connectionManagerFactory, System.Func<DbUp.Engine.Output.IUpgradeLog> log, string schema, System.Func<bool> variablesEnabled, System.Collections.Generic.IEnumerable<DbUp.Engine.IScriptPreprocessor> scriptPreprocessors, System.Func<DbUp.Engine.IJournal> journalFactory) { }
        protected override void ExecuteCommandsWithinExceptionHandler(int index, DbUp.Engine.SqlScript script, System.Action executeCommand) { }
        protected override string GetVerifySchemaSql(string schema) { }
    }
}
