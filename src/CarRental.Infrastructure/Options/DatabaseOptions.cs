namespace CarRental.Infrastructure.Options
{
    public class DatabaseOptions
    {
        public string ConnectionString { get; set; }
        public bool InMemory { get; set; }
        public bool Seed { get; set; }
        public bool ForceSeed { get; set; }
    }
}
