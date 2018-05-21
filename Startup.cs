using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestMediaTR.Persistence;
using AutoMapper;
using MediatR;

namespace TestMediaTR
{
    public class Startup
    {
        private IConfiguration _conf;

        public Startup(IConfiguration configuration)
        {
            _conf = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ConceptosContext>(
                options => options.UseSqlServer(_conf.GetConnectionString("Db")));

            services.AddMvc();
            services.AddAutoMapper();
            services.AddCors();
            services.AddMediatR();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvcWithDefaultRoute();
        }
    }
}
