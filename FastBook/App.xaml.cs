using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using FastBook.Services; 
using FastBook.ViewModels; 
using FastBook.Data;
using FastBook.Interfaces;


namespace FastBook
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static App CurrentApp => (App)Application.Current;

        public IServiceProvider ServiceProvider { get; private set; }

        public App()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();
        }


        private void ConfigureServices(IServiceCollection services)
        {
            
            services.AddDbContext<NotesDbContext>();

            
            services.AddScoped<ITagService, TagService>();
            

            
            services.AddSingleton<INoteService, NoteService>();

         
            services.AddTransient<MainViewModel>();
            services.AddTransient<NoteViewModel>();
        }
    }

}
