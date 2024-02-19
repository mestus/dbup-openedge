using DbUp.Tests.Common;

namespace DbUp.OpenEdge.Tests;

public class NoPublicApiChanges : NoPublicApiChangesBase
{
    public NoPublicApiChanges()
        : base(typeof(OpenEdgeExtensions).Assembly)
    {
    }
}
