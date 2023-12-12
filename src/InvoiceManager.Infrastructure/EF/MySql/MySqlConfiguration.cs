namespace InvoiceManager.Infrastructure.EF.MySql;

public class MySqlConfiguration
{
    public string Url { get; set; }
    public string Port { get; set; }
    public string User {  get; set; }   
    public string Password { get; set; }
    public string DatabaseName { get; set; }

    public string BuildConnectionString()
    {
        return $"Server={Url};Port={Port};Database={DatabaseName};Uid={User};Pwd={Password};";
    }
}
