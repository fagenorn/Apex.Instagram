using Apex.Instagram.UI.Contracts;

using Caliburn.Micro;

namespace Apex.Instagram.UI.ViewModels
{
    public class AccountGridViewModel : Screen, IAccountGrid
    {
        public AccountGridViewModel()
        {
            Accounts.Add("First");
            Accounts.Add("Second");
            Accounts.Add("Third");
        }

        public IObservableCollection<string> Accounts { get; } = new BindableCollection<string>();
    }
}