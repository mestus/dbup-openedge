using System;
using System.Collections.Generic;
using System.Data.Odbc;
using DbUp.Engine;
using DbUp.Engine.Output;
using DbUp.Engine.Transactions;
using DbUp.Support;

namespace DbUp.OpenEdge
{
    /// <summary>
    /// An implementation of <see cref="ScriptExecutor"/> that executes against an OpenEdge database.
    /// </summary>
    public class OpenEdgeScriptExecutor : ScriptExecutor
    {
        /// <summary>
        /// Initializes an instance of the <see cref="OpenEdgeScriptExecutor"/> class.
        /// </summary>
        /// <param name="connectionManagerFactory"></param>
        /// <param name="log">The logging mechanism.</param>
        /// <param name="schema">The schema that contains the table.</param>
        /// <param name="variablesEnabled">Function that returns <c>true</c> if variables should be replaced, <c>false</c> otherwise.</param>
        /// <param name="scriptPreprocessors">Script Preprocessors in addition to variable substitution</param>
        /// <param name="journalFactory">Database journal</param>
        public OpenEdgeScriptExecutor(Func<IConnectionManager> connectionManagerFactory, Func<IUpgradeLog> log, string schema, Func<bool> variablesEnabled,
            IEnumerable<IScriptPreprocessor> scriptPreprocessors, Func<IJournal> journalFactory)
            : base(connectionManagerFactory, new OpenEdgeObjectParser(), log, schema, variablesEnabled, scriptPreprocessors, journalFactory)
        {
        }

        protected override string GetVerifySchemaSql(string schema) => $"SELECT COUNT(OWNER) FROM SYSPROGRESS.SYSTABLES WHERE OWNER = '{schema}'";

        protected override void ExecuteCommandsWithinExceptionHandler(int index, SqlScript script, Action executeCommand)
        {
            try
            {
                executeCommand();
            }
            catch (OdbcException exception)
            {
                Log().WriteInformation("OdbcException has occurred in script: '{0}'", script.Name);
                Log().WriteError("Script block number: {0}; Message: {1}; Content: {2}", index, exception.Message, script.Contents);
                throw;
            }
        }
    }
}
