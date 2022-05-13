using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace consulta_banco
{
    public class Startup
    {
        private readonly IConfiguration config;

        public Startup(IConfiguration config)
        {
            this.config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = config.GetConnectionString("mysqlConnection");
            services.AddDbContext<Contexto>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            services.AddHostedService<Worker>();

            services.AddScoped<InsertJob>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();

                var insertJob = new JobKey("insertJob", "jobGroup");
                q.AddJob<InsertJob>(insertJob);

                q.AddTrigger(t => t
                    .WithIdentity("Insert Trigger")
                    .ForJob(insertJob)
                    .StartNow()
                    .WithDailyTimeIntervalSchedule(x => x.WithInterval(10, IntervalUnit.Second))
                );

                var updateJob = new JobKey("updateJob", "newJobGroup");
                q.AddJob<UpdateJob>(updateJob);

                q.AddTrigger(t => t
                    .WithIdentity("Update Trigger")
                    .ForJob(updateJob)
                    .StartNow()
                    .WithDailyTimeIntervalSchedule(x => x.WithInterval(30, IntervalUnit.Second))
                //.WithCronSchedule("* 30 * ? * *")
                );

            });

            // ASP.NET Core hosting
            services.AddQuartzServer(opts => opts.WaitForJobsToComplete = true);

        }

        public void Configure(IApplicationBuilder app)
        {

        }
    }
}
