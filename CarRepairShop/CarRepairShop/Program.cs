using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using CarRepairShopBusinessLogic.BusinessLogics;
using CarRepairShopContracts.BusinessLogicsContracts;
using CarRepairShopContracts.StoragesContracts;
using CarRepairShopListImplement.Implements;
using CarRepairShop;
using System.Drawing;
using CarRepairShopBisinessLogic.BusinessLogic;

namespace CarRepairShop
{
    internal static class Program
    {
        private static ServiceProvider? _serviceProvider;
        public static ServiceProvider? ServiceProvider => _serviceProvider;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            var services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();

            Application.Run(_serviceProvider.GetRequiredService<FormMain>());
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddLogging(option =>
            {
                option.SetMinimumLevel(LogLevel.Information);
                option.AddNLog("nlog.config");
            });
            services.AddTransient<IDetailStorage, DetailStorage>();
            services.AddTransient<IOrderStorage, OrderStorage>();
            services.AddTransient<ICarStorage, CarStorage>();
            services.AddTransient<IDetailLogic, DetailLogic>();
            services.AddTransient<IOrderLogic, OrderLogic>();
            services.AddTransient<ICarLogic, CarLogic>();
            services.AddTransient<FormMain>();
            services.AddTransient<FormDetail>();
            services.AddTransient<FormDetails>();
            services.AddTransient<FormCreateOrder>();
            services.AddTransient<FormCar>();
            services.AddTransient<FormCarDetail>();
            services.AddTransient<FormCars>();
        }

    }
}