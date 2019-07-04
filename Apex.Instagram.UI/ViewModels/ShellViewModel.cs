using System.ComponentModel.Composition;

using Caliburn.Micro;

namespace Apex.Instagram.UI.ViewModels
{
    [Export(typeof(IShell))]
    public class ShellViewModel : Conductor<Screen>, IShell
    {
        public void ShowMainScreen() { ActivateItem(new MainViewModel()); }
    }
}