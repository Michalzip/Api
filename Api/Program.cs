namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddServices();

            var app = builder.Build();

            app.UseServices();

            app.MapControllers();

            app.Run();
        }
    }
}
