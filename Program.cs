using Microsoft.EntityFrameworkCore;
using WebApplication3.models;


namespace WebApplication3
{
    public class Program
    {
        public static void Main(string[] args)

        {

            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                 policy =>
                                 {
                                     policy.AllowAnyOrigin()
                                     .AllowAnyHeader()
                                     .AllowAnyMethod();
                                 });
            });

            // Add services to the container.







            builder.Services.AddControllers();
            builder.Services.AddDbContext<AdminContext>(opt =>
            opt.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDbContext")));

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(x =>
            {
                x.AllowAnyOrigin();
                x.WithOrigins("https://localhost:7124");  //https://localhost:7124/
                x.AllowAnyMethod();
                x.AllowAnyHeader();
            });

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}