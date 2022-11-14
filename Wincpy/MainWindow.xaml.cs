using Wincpy.Helpers;
using Windows.Media.Audio;

namespace Wincpy;

public sealed partial class MainWindow : WindowEx
{

    public MainWindow()
    {
        InitializeComponent();
        AppWindow.SetIcon(Path.Combine(AppContext.BaseDirectory, "Assets/icon.png"));
        Content = null;
        Title = "AppDisplayName".GetLocalized();
        Width = 670;
        Height = 580;
    }
    private void WindowEx_SizeChanged(object sender, Microsoft.UI.Xaml.WindowSizeChangedEventArgs args)
    {
        this.Width = 670;
        this.Height = 580;
    }
}
