using System.ComponentModel.Composition;

using Caliburn.Micro;

namespace Apex.Instagram.UI.Contracts
{
    [InheritedExport]
    public interface IAccountGrid : IActivate
    {
        IObservableCollection<string> Accounts { get; }
    }
}