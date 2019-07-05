using System.ComponentModel.Composition;

using Caliburn.Micro;

namespace Apex.Instagram.UI.Contracts
{
    [InheritedExport]
    public interface IShell : IHaveActiveItem
    {
        void ShowAccountGridScreen();
    }
}