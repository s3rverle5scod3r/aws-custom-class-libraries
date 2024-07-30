using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace nuget_class_library.class_library.aws.secretsManager
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class AuroraRdsSecret
    {
        /// <summary>
        /// Gets the DbClusterIdentifier.
        /// </summary>
        public string DbClusterIdentifier { get; private set; }

        ///<summary>
        /// Gets the Password.
        ///</summary>
        public string Password { get; private set; }

        /// <summary>
		/// Gets the DB Engine.
		/// </summary>
		public string Engine{ get; private set; }

        ///<summary>
        /// Gets the Port.
        ///</summary>
        public string Port { get; private set; }

        ///<summary>
        /// Gets the Cluster Host Address.
        ///</summary>
        public string Host { get; private set; }

        ///<summary>
        /// Gets the Username.
        ///</summary>
        public string Username { get; private set; }

        public AuroraRdsSecret(
            string dbClusterIdentifier,
            string password,
            string engine,
            string port,
            string host,
            string username)
        {
            DbClusterIdentifier = dbClusterIdentifier;
            Password = password;
            Engine = engine;
            Port = port;
            Host = host;
            Username = username;
        }
    }


}
