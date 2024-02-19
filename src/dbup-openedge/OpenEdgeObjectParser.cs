using DbUp.Support;

namespace DbUp.OpenEdge;

/// <summary>
/// Parses Sql Objects and performs quoting functions.
/// </summary>
public class OpenEdgeObjectParser : SqlObjectParser
{
    public OpenEdgeObjectParser() : base("\"", "\"")
    {
    }
}
