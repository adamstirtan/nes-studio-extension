using System.Diagnostics;

using Microsoft;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.Commands;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Threading;
using Microsoft.Web.WebView2.Wpf;

namespace NesStudio;

[VisualStudioContribution]
internal class NesStudioCommand(TraceSource traceSource) : Command
{
    private readonly TraceSource _logger = Requires.NotNull(traceSource, nameof(traceSource));

    private WebView2? _webView;

    public ServiceProvider ServiceProvider { get; private set; }

    public override CommandConfiguration CommandConfiguration => new("%NesStudio.NesStudioCommand.DisplayName%")
    {
        Icon = new(ImageMoniker.KnownValues.Extension, IconSettings.IconAndText),
        Placements = [CommandPlacement.KnownPlacements.ExtensionsMenu]
    };

    public override Task InitializeAsync(CancellationToken cancellationToken)
    {
        return base.InitializeAsync(cancellationToken);
    }

    public override async Task ExecuteCommandAsync(IClientContext context, CancellationToken cancellationToken)
    {
        if (_webView == null)
        {
            _webView = new WebView2();

            await _webView.EnsureCoreWebView2Async(null);

            _webView.Source = new Uri("https://github.com/adamstirtan/nes-studio-extension");
            _webView.Width = 640;
            _webView.Height = 480;

            var emulatorControl = new NesEmulatorControl
            {
                WebView = _webView
            };

            var uiShell = await ServiceProvider.GetServiceAsync(typeof(SVsUIShell)) as IVsUIShell;

            if (uiShell != null)
            {
                // Create a tool window
                Guid toolWindowGuid = Guid.NewGuid();
                IVsWindowFrame windowFrame;
                uiShell.CreateToolWindow(
                    (uint)(__VSCREATETOOLWIN.CTW_fMultiInstance | __VSCREATETOOLWIN.CTW_fCanFloat),
                    0,
                    emulatorControl,
                    ref toolWindowGuid,
                    (uint)WindowFrameTypeFlags.WINDOWFRAMETYPE_Float,
                    ref toolWindowGuid,
                    null,
                    null,
                    out windowFrame);

                // Set the tool window size
                windowFrame.SetFramePos(VSSETFRAMEPOS.SFP_fSize, ref toolWindowGuid, 0, 0, 800, 600);
                windowFrame.Show();
            }
        }
        else
        {
            // If the WebView2 control already exists, just show the window
            _webView.Show();
        }
    }
}