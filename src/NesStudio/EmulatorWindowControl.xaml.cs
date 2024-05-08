using System;
using System.IO;
using System.Reflection;
using System.Windows.Controls;

namespace NesStudio
{
    public partial class EmulatorWindowControl : UserControl
    {
        public EmulatorWindowControl()
        {
            InitializeComponent();

            Assembly assembly = Assembly.GetExecutingAssembly();

            string resourceName = "NesStudio.WebAssets.index.html";
            string htmlContent;

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream != null)
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        htmlContent = reader.ReadToEnd();
                    }
                }
                else
                {
                    throw new Exception("Failed to read embedded HTML resource.");
                }
            }

            webBrowser.NavigateToString(htmlContent);
        }
    }
}