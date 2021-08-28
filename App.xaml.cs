using BELTrader.Domain.Models;
using BELTrader.Domain.Services;
using BELTrader.Domain.Services.AuthenticationServices;
using BELTrader.Domain.Services.TransactionServices;
using BELTrader.EntityFramework;
using BELTrader.EntityFramework.Services;
using BELTrader.FinancialModelingPrepAPI;
using BELTrader.FinancialModelingPrepAPI.Services;
using BELTrader.WPF.State.Accounts;
using BELTrader.WPF.State.Assets;
using BELTrader.WPF.State.Authenticators;
using BELTrader.WPF.State.Navigators;
using BELTrader.WPF.ViewModels;
using BELTrader.WPF.ViewModels.Factories;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BELTrader.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IServiceProvider serviceProvider = CreateServiceProvider();

            Window window =  serviceProvider.GetRequiredService<MainWindow>();
            window.Show();

            base.OnStartup(e);
        }

        private IServiceProvider CreateServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();

            string apiKey = ConfigurationManager.AppSettings.Get("financeApiKey");

            services.AddSingleton<FinancialModelingPrepHttpClientFactory>(new FinancialModelingPrepHttpClientFactory(apiKey));
            services.AddSingleton<BELTraderDbContextFactory>();
            services.AddSingleton<IAuthenticationService, AuthenticationService>();
            services.AddSingleton<IDataService<Account>, AccountDataService>();
            services.AddSingleton<IAccountService, AccountDataService>();
            services.AddSingleton<IStockPriceService, StockPriceService>();
            services.AddSingleton<IBuyStockService, BuyStockService>();
            services.AddSingleton<IMajorIndexService, MajorIndexService>();

            services.AddSingleton<IPasswordHasher, PasswordHasher>();

            services.AddSingleton<IBELTraderVMFactory, BELTraderVMFactory>();
            services.AddSingleton<BuyVM>();
            services.AddSingleton<PortfolioVM>();
            services.AddSingleton<AssetSummaryVM>();
            services.AddSingleton<HomeVM>(services => new HomeVM(
                services.GetRequiredService<AssetSummaryVM>(),
                    MajorIndexListingVM.LoadMajorIndexVM(
                        services.GetRequiredService<IMajorIndexService>())));


            services.AddSingleton<CreateViewModel<HomeVM>>(services =>
            {
                return () => services.GetRequiredService<HomeVM>();
            });

            services.AddSingleton<CreateViewModel<BuyVM>>(services =>
            {
                return () => services.GetRequiredService<BuyVM>();
            });


            services.AddSingleton<CreateViewModel<PortfolioVM>>(services =>
            {
                return () => services.GetRequiredService<PortfolioVM>();
            });

            services.AddSingleton<VMDelegateRenavigator<HomeVM>>();
            services.AddSingleton<CreateViewModel<LoginVM>>(services =>
            {
                return () => new LoginVM(
                    services.GetRequiredService<IAuthenticator>(),
                    services.GetRequiredService<VMDelegateRenavigator<HomeVM>>());
            });

            services.AddSingleton<INavigator, Navigator>();
            services.AddSingleton<IAuthenticator, Authenticator>();
            services.AddSingleton<IAccountStore, AccountStore>();
            services.AddSingleton<AssetStore>();
            services.AddScoped<MainVM>();
            services.AddScoped<BuyVM>();

            services.AddScoped<MainWindow>(s => new MainWindow(s.GetRequiredService<MainVM>()));

            return services.BuildServiceProvider();
        }
    }
}
