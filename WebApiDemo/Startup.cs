using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebApiDemo.Data;

namespace WebApiDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Adding AddXmlSerializerFormatters() for content negotiation through Media Type formatters.
            services.AddMvc().AddXmlSerializerFormatters();
            //Adding DBContext service
            services.AddDbContext<ProductDbContext>(option => option.UseSqlServer(@"Data Source=DESKTOP-JF39G45\MSSQLSERVER01;Initial Catalog=ProductsDb;Trusted_Connection=True;"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,ProductDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            //Ensures that the DB is created. If it is already created, then it doesnt do anything. If the DB does not exist, then it creates the DB.
            dbContext.Database.EnsureCreated();
        }
    }
}
