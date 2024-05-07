using System.Diagnostics;

using Microsoft;
using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.Commands;
using Microsoft.VisualStudio.Extensibility.Shell;

namespace NesStudio;

[VisualStudioContribution]
internal class NesStudioCommand(TraceSource traceSource) : Command
{
    private readonly TraceSource _logger = Requires.NotNull(traceSource, nameof(traceSource));

    public override CommandConfiguration CommandConfiguration => new("%NesStudio.NesStudioCommand.DisplayName%")
    {
        Icon = new(ImageMoniker.KnownValues.Extension, IconSettings.IconAndText),
        Placements = [CommandPlacement.KnownPlacements.ExtensionsMenu],
    };

    public override Task InitializeAsync(CancellationToken cancellationToken)
    {
        _logger.TraceInformation(_logger.Name + " initialized.");

        return base.InitializeAsync(cancellationToken);
    }

    public override async Task ExecuteCommandAsync(IClientContext context, CancellationToken cancellationToken)
    {
        await Extensibility.Shell().ShowPromptAsync("Hello from NES Studio", PromptOptions.OK, cancellationToken);
    }
}