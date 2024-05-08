using System;
using System.Runtime.InteropServices;
using System.Threading;

using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

using Task = System.Threading.Tasks.Task;

namespace NesStudio
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideToolWindow(typeof(EmulatorWindow))]
    [Guid(PackageGuidString)]
    public sealed class EmulatorWindowPackage : AsyncPackage
    {
        public const string PackageGuidString = "6ed5398c-4bf8-457d-bbf5-baab4510ad14";

        public EmulatorWindowPackage()
        { }

        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
            await EmulatorWindowCommand.InitializeAsync(this);
        }

        public override IVsAsyncToolWindowFactory GetAsyncToolWindowFactory(Guid toolWindowType)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            if (toolWindowType == typeof(EmulatorWindow).GUID)
            {
                return this;
            }

            return base.GetAsyncToolWindowFactory(toolWindowType);
        }

        protected override string GetToolWindowTitle(Type toolWindowType, int id)
        {
            if (toolWindowType == typeof(EmulatorWindow))
            {
                return "EmulatorWindow loading";
            }

            return base.GetToolWindowTitle(toolWindowType, id);
        }
    }
}
