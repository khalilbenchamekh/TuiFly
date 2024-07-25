namespace TuiFly.Domain.Models.Configuration;
public class SwaggerSettings
{
    public string Version { get; set; }
    public string Title { get; set; }
    public string Endpoint { get; set; }
    public string Description { get; set; }
    public BearerSettings Bearer { get; set; }

    public class BearerSettings
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public string Scheme { get; set; }
        public string BearerFormat { get; set; }
        public string In { get; set; }
        public string Type { get; set; }
    }
}