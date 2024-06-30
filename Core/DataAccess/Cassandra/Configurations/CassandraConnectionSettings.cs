namespace Core.DataAccess.Cassandra.Configurations;

public class CassandraConnectionSettings
{
    public string Host { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Keyspace { get; set; }
}