namespace ShippingStationLogin.Database
{
    public class PostgresConfig : IConfig
    {
        private string host;
        private string database;
        private string port;
        private string user;
        private string pass;

        public string Host { get { return host; } set { this.host = value; } }
        public string Database { get { return database; } set { this.database = value; } }
        public string Port { get { return port; } set { this.port = value; } }
        public string User { get { return user; } set { this.user = value; } }

        public string Password { set { this.pass = value; } }

        public string ConnectionString() { return string.Format("Server={0};Port={1};User Id={2};Password={3};Database={4}", Host, Port, User, pass, Database); }

        public PostgresConfig(string host, string database, string port, string user, string pass)
        {
            this.host = host;
            this.database = database;
            this.port = port;
            this.user = user;
            this.pass = pass;
        }
    }
}
