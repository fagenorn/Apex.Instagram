using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;

using Apex.Instagram.UI.Contracts;

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
            var pluginAssemblies = GetPluginAssemblies()
                .ToList();

            var pluginExportProvider = CreateCatalogExportProvider(pluginAssemblies);
            var appExportProvider    = CreateCatalogExportProvider(AssemblySource.Instance);

            AssemblySource.Instance.AddRange(pluginAssemblies);

            _container                          = new CompositionContainer(pluginExportProvider, appExportProvider);
            pluginExportProvider.SourceProvider = _container;
            appExportProvider.SourceProvider    = _container;

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

        private CatalogExportProvider CreateCatalogExportProvider(IEnumerable<Assembly> assemblies)
        {
            var pluginCatalog = new AggregateCatalog(assemblies.Select(x => new AssemblyCatalog(x)));

            return new CatalogExportProvider(pluginCatalog);
        }

        private IEnumerable<Assembly> GetPluginAssemblies()
        {
            var pluginPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plugins");
            if ( !Directory.Exists(pluginPath) )
            {
                Directory.CreateDirectory(pluginPath);
            }

            var fi = new DirectoryInfo(pluginPath).GetFiles("*.dll");

            return fi.Select(fileInfo => Assembly.LoadFrom(fileInfo.FullName));
        }
    }
}