using System.Data;

namespace ShippingStationLogin.Database
{
    interface IDatabaseConnection
    {
        /// <summary>
        /// Open connection to database
        /// </summary>
        void OpenConnection();

        /// <summary>
        /// Close connection to database
        /// </summary>
        void CloseConnection();

        /// <summary>
        /// Test connection to database
        /// </summary>
        /// <returns>bool</returns>
        bool TestConnection();

        /// <summary>
        /// Returns if a connection is currently made
        /// </summary>
        /// <returns>bool</returns>
        bool IsConnected();

        /// <summary>
        /// Executes a given query string and returns the 
        /// data if <param name"ensureReturn"></param> is set to true,
        /// Data is returned as a DataTable object
        /// </summary>
        /// <param name="query"></param>
        /// <param name="ensureReturn"></param>
        /// <returns>DataTable</returns>
        DataTable execute(string query, bool ensureReturn);
    }
}
