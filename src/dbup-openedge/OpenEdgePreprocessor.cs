using DbUp.Engine;

namespace DbUp.OpenEdge;

/// <summary>
/// This preprocessor makes adjustments to your sql to make it compatible with OpenEdge.
/// </summary>
public class OpenEdgePreprocessor : IScriptPreprocessor
{
    /// <summary>
    /// Performs some preprocessing step on a OpenEdge script.
    /// </summary>
    public string Process(string contents) => contents;
}
