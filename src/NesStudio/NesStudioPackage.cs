using System;
using System.Runtime.InteropServices;
using System.Threading;

using Microsoft.VisualStudio.Shell;

using Task = System.Threading.Tasks.Task;

namespace NesStudio
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [Guid(PackageGuidString)]
    public sealed class NesStudioPackage : AsyncPackage
    {
        public const string PackageGuidString = "1747e367-94ed-4666-93e4-2ba21dd30bd9";

        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
        }
    }
}