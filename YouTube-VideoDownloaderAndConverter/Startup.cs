using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace YouTube_VideoDownloaderAndConverter
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private IHostingEnvironment HostingEnvironment;

        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            // var physicalProvider = new PhysicalFileProvider(/*Directory.GetCurrentDirectory() +"/"+ */Configuration.GetValue<string>("StoredFilesPath"));
            //var physicalProvider = new PhysicalFileProvider(Configuration.GetValue<string>("StoredFilesPath"));
            string AssemblyPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location).ToString();


            //    int index = AssemblyPath.IndexOf("bin");

            //AssemblyPath = AssemblyPath.Remove(index, AssemblyPath.Count() - index);

            //AssemblyPath += @"wwwroot\Downloads\";

            //var physicalProvider = new PhysicalFileProvider("c:/");

            //var physicalProvider = HostingEnvironment.ContentRootFileProvider;

            var mainPhycsicalProvider = HostingEnvironment.WebRootPath;

            var physicalProvider = HostingEnvironment.WebRootFileProvider;

            var embeddedProvider = new EmbeddedFileProvider(Assembly.GetEntryAssembly());
            var compositeProvider = new CompositeFileProvider(physicalProvider, embeddedProvider);

            // PhysicalFileProvider physicalFileProvider1 = new PhysicalFileProvider(AssemblyPath,Microsoft.Extensions.FileProviders.Physical.ExclusionFilters.)
            //  C: \Users\Rufat\source\repos\YouTube - VideoDownloaderAndConverter\YouTube - VideoDownloaderAndConverter\wwwroot\Downloads

            // To list physical files in the temporary files folder, use:
            //var physicalProvider = new PhysicalFileProvider(Path.GetTempPath());

            services.AddSingleton<IFileProvider>(compositeProvider);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")),
                RequestPath = "/Downloads"
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Download}/{action=SearchFile}/{id?}");
            });

        }
    }
}
