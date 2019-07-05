using System.ComponentModel.Composition;

using Apex.Instagram.UI.Contracts;

using Caliburn.Micro;

namespace Apex.Instagram.UI.ViewModels
{
    public class ShellViewModel : Conductor<object>.Collection.OneActive, IShell
    {
        private readonly IAccountGrid _accountGrid;

        [ImportingConstructor]
        public ShellViewModel(IAccountGrid accountGrid)
        {
            _accountGrid = accountGrid;
            ShowAccountGridScreen();
        }

        public void ShowAccountGridScreen() { ActivateItem(_accountGrid); }
    }
}