using Microsoft.Extensions.Configuration;

namespace FitLife.API.Helpers
{
    /// <summary>
    /// Service for database connection
    /// </summary>
    public static class ConnectionService
    {
        /// <summary>
        /// Connection string
        /// </summary>
        public static string connectionString;

        /// <summary>
        /// Sets the <see cref="connectionString"/> from application configuration
        /// </summary>
        /// <param name="config"></param>
        public static void Set(IConfiguration config)
        {
            connectionString = config.GetConnectionString("IdentityConnection");
        }
    }
}
