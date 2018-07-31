using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ShippingStationLogin.Database;
using ShippingStationLogin.Forms;

namespace ShippingStationLogin
{
    static class Program
    {
        private static Postgres postgres;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            postgres = new Postgres(LoadPostgresConfig());

            TestDatabaseConnections();

            postgres.OpenConnection();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(postgres));

            postgres.CloseConnection();
        }

        private static PostgresConfig LoadPostgresConfig()
        {
            return new PostgresConfig(
                Settings1.Default.POSTGRES_HOST,
                Settings1.Default.POSTGRES_DATABASE,
                Settings1.Default.POSTGRES_PORT,
                Settings1.Default.POSTGRES_USERNAME,
                Settings1.Default.POSTGRES_PASSWORD
                );
        }

        /// <summary>
        /// Test for connection to the database
        /// </summary>
        private static void TestDatabaseConnections()
        {
            try
            {
                // Test postgres
                postgres.TestConnection();

            }
            catch (Exception c)
            {
                // Could not connect, display message and close program
                MessageBox.Show("Unable to connect to database: " + c.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Force the application to close
                Environment.Exit(0);
            }
        }
    }
}
