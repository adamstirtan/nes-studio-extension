using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.Extensibility;

namespace NesStudio;

[VisualStudioContribution]
internal class ExtensionEntrypoint : Extension
{
    public override ExtensionConfiguration ExtensionConfiguration => new()
    {
        Metadata = new(
            id: "NesStudio.3e1737ad-f748-4b9d-84b4-1b3c63f5818e",
            version: ExtensionAssemblyVersion,
            publisherName: "Adam Stirtan",
            displayName: "NesStudio",
            description: "High scores while you code")
    };

    protected override void InitializeServices(IServiceCollection serviceCollection)
    {
        base.InitializeServices(serviceCollection);
    }
}