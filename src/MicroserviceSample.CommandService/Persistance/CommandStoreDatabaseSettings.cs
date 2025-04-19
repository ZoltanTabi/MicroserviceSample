namespace MicroserviceSample.CommandService.Persistance;

public class CommandStoreDatabaseSettings
{
    public string ConnectionString { get; set; } = string.Empty;
    public string DatabaseName { get; set; } = string.Empty;
    public string CommandCollectionName { get; set; } = string.Empty;
    public string PlatformCollectionName { get; set; } = string.Empty;
}
