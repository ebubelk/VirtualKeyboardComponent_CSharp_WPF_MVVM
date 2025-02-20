using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;
using VirtualKeyboardComponent.MVVM.View;
using VirtualKeyboardComponent.MVVM.ViewModels;
using VirtualKeyboardComponent.Services;
using VirtualKeyboardComponent.Stores;

namespace VirtualKeyboardComponent
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        public App()
        {
            IServiceCollection services = new ServiceCollection();

            //Stores
            services.AddSingleton<NavigationStore>();
            services.AddSingleton<VirtualKeyboardStore>();
            services.AddSingleton<TextBoxDataStore>();

            //Services
            services.AddSingleton<INavigationService>(s => CreateHomeNavigationService(s));
            services.AddSingleton<VirtualKeyboardService>(s => new VirtualKeyboardService(s.GetRequiredService<VirtualKeyboardStore>()));

            //ViewModels
            services.AddSingleton<VirtualKeyboardVM>(s => new VirtualKeyboardVM(
                s.GetRequiredService<VirtualKeyboardStore>(),
                s.GetRequiredService<VirtualKeyboardService>()));
            services.AddSingleton<MainVM>(s => new MainVM(
                s.GetRequiredService<NavigationStore>(),
                s.GetRequiredService<VirtualKeyboardVM>()));
            services.AddTransient<HomeVM>(s => new HomeVM(
                s.GetRequiredService<VirtualKeyboardService>(),
                CreateTextBoxDataNavigationService(s),
                s.GetRequiredService<TextBoxDataStore>()));
            services.AddTransient<TextBoxDataVM>(s => new TextBoxDataVM(
                s.GetRequiredService<TextBoxDataStore>(),
                CreateHomeNavigationService(s)));

            //Window
            services.AddSingleton<MainWindow>(s => new MainWindow()
            {
                DataContext = s.GetRequiredService<MainVM>()
            });

            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            INavigationService initialNavigationService = _serviceProvider.GetRequiredService<INavigationService>();
            initialNavigationService.Navigate();

            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);
        }

        private INavigationService CreateHomeNavigationService(IServiceProvider serviceProvider)
        {
            return new NavigationService<HomeVM>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                () => serviceProvider.GetRequiredService<HomeVM>());
        }

        private INavigationService CreateTextBoxDataNavigationService(IServiceProvider serviceProvider)
        {
            return new NavigationService<TextBoxDataVM>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                () => serviceProvider.GetRequiredService<TextBoxDataVM>());
        }
    }
}
