namespace EndpointRegistration {
    public static class EndpointRegistration {
         public static WebApplication RegisterEndpoints(this WebApplication app) {
            app.MapGet("/", () => {
                return "Hello World";
            });

            return app;
        }
    }
}