using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestMediaTR.Domain.Commands;
using TestMediaTR.Domain.Events;
using TestMediaTR.Persistence;
using TestMediaTR.Support;
using TestMediaTR.Support.Converter;

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

            services.AddDbContext<ConceptosContext>(options => options.UseSqlServer(_conf.GetConnectionString("Db")));

            services.Configure<AmqpOptions>(_conf.GetSection("amqp"));

            services.AddMediatR();

            services.AddTransient<IValidator<CreateUpdateConceptosCommand>, CreateUpdateConceptoCommandValidator>();
            services.AddTransient<ICommandEventConverter, CommandEventConverter>();

            //Se usa singleton por que la conexión es muy costosa
            services.AddSingleton<IEventEmitter, AmqpEventEmitter>();

            
            services.AddMediatR();

            services.AddAutoMapper();
            services.AddCors();
            

            services.AddMvc()
                    .AddFluentValidation();
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
