using System;
using System.Runtime.InteropServices;

using Microsoft.VisualStudio.Shell;

namespace NesStudio
{
    [Guid("bbeaa736-7073-469e-80d4-090a9732e867")]
    public class EmulatorWindow : ToolWindowPane
    {
        public EmulatorWindow() : base(null)
        {
            Caption = "NES Studio";

            Content = new EmulatorWindowControl();
        }
    }
}
