using Microsoft.UI.Xaml.Controls;
using Wincpy.Helpers;
using Wincpy.ViewModels;

namespace Wincpy.Views;

public sealed partial class 无线连接Page : Page
{
    private string mbin;
    public 无线连接ViewModel ViewModel
    {
        get;
    }
    public List<string> devices = new() { };
    public 无线连接Page()
    {
        var CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        //mbin = CurrentDirectory + "Assets\\Scrcpy\\";
        mbin = @"D:\apps\main\Wincpy\Scrcpy\";
        ViewModel = App.GetService<无线连接ViewModel>();
        InitializeComponent();
    }

    private void BtnConn(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        string prop = $"scrcpy --max-fps {zhenlv.Text}";
        prop += string.IsNullOrEmpty(fenbianlv.Text) ? "" : " -m " + fenbianlv.Text;
        prop += string.IsNullOrEmpty(malv.Text) ? "" : " -b " + malv.Text;
        if (devis.Items.Count >= 1)
        {
            prop += string.IsNullOrEmpty(devis.SelectedValue.ToString()) ? " -d" : " -s " + devis.SelectedValue.ToString();
        }
        else
        {
            prop += " -d";
        }
        prop += zhiding.IsChecked.ToString() == "True" ? " --always-on-top" : "";
        prop += changliang.IsChecked.ToString() == "True" ? " -w" : "";
        prop += zhidu.IsChecked.ToString() == "True" ? " -n" : "";
        prop += xiping.IsChecked.ToString() == "True" ? " -S" : "";
        new Thread(new ParameterizedThreadStart(Conn)).Start(mbin + prop);
    }
    private void Conn(object cmdo)
    {
        CMDHelper.CMDRunner(cmdo as string);
    }
    private void changliang_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        zhidu.IsEnabled = changliang.IsChecked.ToString() == "False";
    }
    private void xiping_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        zhidu.IsEnabled = xiping.IsChecked.ToString() == "False";
    }
    private void zhidu_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        changliang.IsEnabled = zhidu.IsChecked.ToString() == "False";
        xiping.IsEnabled = zhidu.IsChecked.ToString() == "False";
    }
    private void btnOpenPoint(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(txtPoint.Text))
        {
            CMDHelper.CMDRunner($"adb -s {devis.SelectedValue.ToString()} tcpip {txtPoint.Text}");
        }
    }
    private void btnTestDev(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        initList();
    }
    private void devis_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        initList();
    }
    private void initList()
    {
        devices = new List<string>();
        string alldevices = CMDHelper.CMDRunner("echo 隔&adb devices").Split('隔')[2];
        for (int i = 2; i < alldevices.Split('\n').Length; i++)
        {
            string item = alldevices.Split('\n')[i].Replace("\r\n", "").Replace("\n", "");
            if (!string.IsNullOrEmpty(item.Split('	')[0]) && item.Split('	')[0].Length > 4)
            {
                devices.Add(item.Split('	')[0]);
            }
        }
        devis.ItemsSource = devices;
        devis.SelectedIndex = 0;
    }
    private void adddevis(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        CMDHelper.CMDRunner($"adb connect {txtIpandPoint.Text.Replace("：",":")}");
        initList();
    }
}
