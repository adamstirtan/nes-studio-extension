﻿using System;
using System.ComponentModel.Design;

using Microsoft.VisualStudio.Shell;

using Task = System.Threading.Tasks.Task;

namespace NesStudio
{
    internal sealed class EmulatorWindowCommand
    {
        public const int CommandId = 0x0100;

        public static readonly Guid CommandSet = new Guid("7558f4bc-8851-4bbc-8cf9-3cdf5652e96a");

        private readonly AsyncPackage package;

        private EmulatorWindowCommand(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new MenuCommand(Execute, menuCommandID);
            commandService.AddCommand(menuItem);
        }

        public static EmulatorWindowCommand Instance
        {
            get;
            private set;
        }

        private IAsyncServiceProvider ServiceProvider
        {
            get
            {
                return package;
            }
        }

        public static async Task InitializeAsync(AsyncPackage package)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync((typeof(IMenuCommandService))) as OleMenuCommandService;
            Instance = new EmulatorWindowCommand(package, commandService);
        }

        private void Execute(object sender, EventArgs e)
        {
            _ = package.JoinableTaskFactory.RunAsync(async delegate
            {
                ToolWindowPane window = await package.ShowToolWindowAsync(typeof(EmulatorWindow), 0, true, package.DisposalToken);
                
                if ((null == window) || (null == window.Frame))
                {
                    throw new NotSupportedException("Cannot create tool window");
                }
            });
        }
    }
}
