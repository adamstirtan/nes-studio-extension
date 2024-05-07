using System.Windows;
using System.Windows.Controls;

using Microsoft.Web.WebView2.Wpf;
using Microsoft.Win32;

namespace NesStudio;

public partial class NesEmulatorControl : UserControl
{
    public WebView2? WebView { get; set; }

    public NesEmulatorControl()
    {
        InitializeComponent();
    }

    private async void LoadRomButton_Click(object sender, RoutedEventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Filter = "NES ROM files (*.nes)|*.nes|All files (*.*)|*.*";

        if (openFileDialog.ShowDialog() == true)
        {
            string romFilePath = openFileDialog.FileName;

            await WebView!.CoreWebView2.ExecuteScriptAsync($"loadRom('{romFilePath}')");
        }
    }

    private void InitializeComponent()
    {
        var resourceLocator = new Uri("/NesStudio;component/NesEmulatorControl.xaml", UriKind.Relative);
        var resourceStream = Application.GetResourceStream(resourceLocator).Stream;
        var reader = new System.Windows.Markup.XamlReader();

        Content = (UIElement)reader.LoadAsync(resourceStream);

        WebView = FindName("webView") as WebView2;
    }
}