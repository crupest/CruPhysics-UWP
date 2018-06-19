using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Prism.Autofac.Windows;
using Prism.Windows.AppModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CruPhysics
{
    sealed partial class App : PrismAutofacApplication
    {
        public App()
        {
            InitializeComponent();
        }

        protected override void ConfigureContainer(ContainerBuilder builder)
        {
            base.ConfigureContainer(builder);
            builder.RegisterInstance<IResourceLoader>(new ResourceLoaderAdapter(new ResourceLoader()));
        }

        protected override UIElement CreateShell(Frame rootFrame)
        {
            var shell = Container.Resolve<AppShell>();
            shell.SetContentFrame(rootFrame);
            return shell;
        }

        protected override Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
        {
            return Task.FromResult(true);
        }
    }
}
