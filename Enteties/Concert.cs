// Enteties/Concert.cs
namespace ConsertsModel
{
    public sealed class Concert : EntityBase
    {
        public required string Title { get; set; }
        public required string Location { get; set; }
        public DateTimeOffset Date { get; set; }
    }
}