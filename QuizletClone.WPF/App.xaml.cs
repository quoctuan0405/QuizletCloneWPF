using Microsoft.Extensions.DependencyInjection;
using QuizletClone.API;
using QuizletClone.API.Services;
using QuizletClone.Domain.Services;
using QuizletClone.WPF.State;
using QuizletClone.WPF.State.Navigators;
using QuizletClone.WPF.ViewModels;
using QuizletClone.WPF.ViewModels.Factories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace QuizletClone.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IServiceProvider serviceProvider = CreateServiceProvider();
            //ITestService testService = serviceProvider.GetService<ITestService>();

            //testService.GetUser(1).ContinueWith((task) =>
            //{
            //    var user = task.Result;

            //    Console.WriteLine(user.Id);
            //});

            SetService setService = serviceProvider.GetService<SetService>();

            Window window = serviceProvider.GetRequiredService<MainWindow>();
            window.Show();

            base.OnStartup(e);
        }

        private IServiceProvider CreateServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<QuizletCloneHTTPClientFactory>();

            services.AddSingleton<UserService, UserService>();
            services.AddSingleton<SetService, SetService>();
            services.AddSingleton<ExcelService, ExcelService>();

            services.AddSingleton<Store>();

            services.AddSingleton<IViewModelAbstractFactory, ViewModelAbstractFactory>();
            services.AddSingleton<IViewModelFactory<HomeViewModel>, ListViewModelFactory>();
            services.AddSingleton<IViewModelFactory<ProfileViewModel>, ProfileViewModelFactory>();
            services.AddSingleton<IViewModelFactory<LoginViewModel>, LoginViewModelFactory>();
            services.AddSingleton<IViewModelFactory<SetDetailViewModel>, SetDetailViewModelFactory>();
            services.AddSingleton<IViewModelFactory<CreateSetViewModel>, CreateSetViewModelFactory>();
            services.AddSingleton<IViewModelFactory<UpdateSetViewModel>, UpdateSetViewModelFactory>();
            services.AddSingleton<IViewModelFactory<SignUpViewModel>, SignupViewModelFactory>();
            services.AddSingleton<IViewModelFactory<LearningViewModel>, LearningViewModelFactory>();

            services.AddScoped<INavigator, Navigator>();
            services.AddScoped<MainViewModel>();

            services.AddScoped<MainWindow>(s => new MainWindow(s.GetRequiredService<MainViewModel>()));

            return services.BuildServiceProvider();
        }
    }
}
