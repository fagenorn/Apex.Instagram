using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows;

using Apex.Instagram.UI.ViewModels;

using Caliburn.Micro;

using Telerik.Windows.Controls;

namespace Apex.Instagram.UI
{
    public class MefBootstrapper : BootstrapperBase
    {
        private CompositionContainer _container;

        public MefBootstrapper() { Initialize(); }

        protected override void Configure()
        {
            var catalog = new AggregateCatalog(AssemblySource.Instance.Select(x => new AssemblyCatalog(x)));

            _container = new CompositionContainer(catalog);

            var batch = new CompositionBatch();

            batch.AddExportedValue<IWindowManager>(new WindowManager());
            batch.AddExportedValue<IEventAggregator>(new EventAggregator());
            batch.AddExportedValue(_container);

            _container.Compose(batch);
        }

        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration", Justification = "Simply checking if IEnumerable is empty.")]
        protected override object GetInstance(Type service, string key)
        {
            var contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(service) : key;
            var exports  = _container.GetExportedValues<object>(contract);

            if ( exports.Any() )
            {
                return exports.First();
            }

            throw new ArgumentException($"Could not locate any instances of contract {contract}.");
        }

        protected override IEnumerable<object> GetAllInstances(Type service) { return _container.GetExportedValues<object>(AttributedModelServices.GetContractName(service)); }

        protected override void BuildUp(object instance) { _container.SatisfyImportsOnce(instance); }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            StyleManager.ApplicationTheme = new Office2016Theme();
            DisplayRootViewFor<IShell>();
        }
    }
}