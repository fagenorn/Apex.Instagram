using System.ComponentModel.Composition;

namespace Apex.Instagram.UI.Contracts
{
    [InheritedExport]
    public interface IShell
    {
        void ShowAccountGridScreen();
    }
}