using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Wincpy.Helpers;
using Wincpy.ViewModels;

namespace Wincpy.Views;

public sealed partial class 设置Page : Page
{
    public static string config = "";
    private string mbin;
    public 设置ViewModel ViewModel
    {
        get;
    }

    public 设置Page()
    {
        var CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        mbin = CurrentDirectory;
        ViewModel = App.GetService<设置ViewModel>();
        InitializeComponent();
    }

    private void Button_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        string pars = " ";
        if (string.IsNullOrEmpty(caijian.Text))
        {
            pars += " --crop "+caijian.Text.Trim();
        }
        if (string.IsNullOrEmpty(bianma.Text))
        {
            pars += " --encoder " + bianma.Text.Trim();
        }
        if (string.IsNullOrEmpty(huanchong.Text))
        {
            pars += " --display-buffer=" + huanchong.Text.Trim();
        }
        if (string.IsNullOrEmpty(biaoti.Text))
        {
            pars += " --window-title " + biaoti.Text.Trim();
        }
        if (wubiank.IsChecked.ToString()=="True")
        {
            pars += " --window-borderless";
        }
        if (string.IsNullOrEmpty(lujing.Text))
        {
            pars += " --push-target=" + lujing.Text.Trim();
        }
        StreamWriter sw = new StreamWriter(mbin + "config.txt");
        sw.WriteLine(pars);
        sw.Close();
        config = pars;
    }

    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
        IPCheck.Text = CMDHelper.CMDRunner("arp -a");
    }
}
