namespace CodeEvents.Api.Data
{
    public static class ApplicationBuilderExtensions
    {
        public static async Task SeedDataAsync(this IApplicationBuilder app)
        {
            using ( var scope = app.ApplicationServices.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var db = serviceProvider.GetRequiredService<CodeEventsApiContext>();

                try
                {
                    await SeedData.InitAsync(db);
                }
                catch (Exception e)
                {

                    throw;
                }
            }
        }
    }
}
