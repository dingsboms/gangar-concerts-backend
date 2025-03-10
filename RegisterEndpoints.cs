using ConsertsModel;
using DBContext;
using Microsoft.EntityFrameworkCore;

namespace EndpointRegistration
{
    public static class EndpointRegistration
    {
        public static WebApplication RegisterEndpoints(this WebApplication app)
        {
            app.MapGet("/", () =>
            {
                return "Hello World";
            });

            var concerts_route = app.MapGroup("/concerts");

            concerts_route.MapGet("/", async (ConcertsDB db) =>
                await db.Concerts.ToListAsync()
            );

            concerts_route.MapGet("/{id}", async (int id, ConcertsDB db) =>
      {
          var concert = await db.Concerts.FirstOrDefaultAsync(c => c.Id == id);
          if (concert == null)
          {
              return Results.NotFound($"Concert with ID {id} not found.");
          }
          return Results.Ok(concert);
      });

            concerts_route.MapPost("/", async (Concert concert, ConcertsDB db) =>
            {
                db.Concerts.Add(concert);
                await db.SaveChangesAsync();
                return Results.Created($"/concerts/{concert.Id}", concert);
            });

            concerts_route.MapPut("/{id}", async (int id, Concert updatedConcert, ConcertsDB db) => {
                 var concert = await db.Concerts.FindAsync(id);
            if (concert is null)
            {
                return Results.NotFound();
            }
            db.Concerts.Remove(concert);
            db.Concerts.Add(updatedConcert);
            await db.SaveChangesAsync();

            return Results.Ok(updatedConcert);
            });

            concerts_route.MapDelete("/{id}", async (int id, ConcertsDB db) =>
        {
            var concert = await db.Concerts.FindAsync(id);
            if (concert is null)
            {
                return Results.NotFound();
            }

            db.Concerts.Remove(concert);
            await db.SaveChangesAsync();
            return Results.NoContent();
        });
            return app;
        }
    }
}