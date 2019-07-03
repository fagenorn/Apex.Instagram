using Telerik.Windows.Controls;

namespace Apex.Instagram.UI
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public App()
        {
            StyleManager.ApplicationTheme = new Office2016Theme();
            InitializeComponent();
        }
    }
}