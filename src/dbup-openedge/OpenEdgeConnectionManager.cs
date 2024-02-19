using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text.RegularExpressions;
using DbUp.Engine.Transactions;

namespace DbUp.OpenEdge;

/// <summary>
/// Manages OpenEdge database connections.
/// </summary>
public class OpenEdgeConnectionManager : DatabaseConnectionManager
{
    /// <summary>
    /// Creates a new OpenEdge database connection via ODBC.
    /// </summary>
    /// <param name="connectionString">The ODBC connection string.</param>
    public OpenEdgeConnectionManager(string connectionString)
        : base(new DelegateConnectionFactory(l => new OdbcConnection(connectionString)))
    {
    }

    /// <summary>
    /// Splits the statements in the script using the ";" character.
    /// </summary>
    /// <param name="scriptContents">The contents of the script to split.</param>
    public override IEnumerable<string> SplitScriptIntoCommands(string scriptContents)
    {
        var scriptStatements =
            Regex.Split(scriptContents, "^\\s*;\\s*$", RegexOptions.IgnoreCase | RegexOptions.Multiline)
                .Select(x => x.Trim())
                .Where(x => x.Length > 0)
                .ToArray();

        return scriptStatements;
    }
}
