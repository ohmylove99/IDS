using System;
using System.Configuration;

namespace IDS.QueryService.Config
{
    /// <summary>
    /// 
    /// </summary>
    public class ConfigManager
    {
        #region Port
        private const string KeyPort = "app.port";
        private const int DefaultPort = 9001;
        private int _port;
        #endregion

        #region Singleton 
        private static readonly Lazy<ConfigManager> lazy = new Lazy<ConfigManager>(() => new ConfigManager());
        /// <summary>
        /// This is Singleton instance
        /// </summary>
        public static ConfigManager Instance { get { return lazy.Value; } }
        private ConfigManager()
        {
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public int Port {
            get {
                if (ConfigurationManager.AppSettings[KeyPort] == null)
                    _port = DefaultPort;
                else
                {
                    int port = 0;
                    Int32.TryParse(ConfigurationManager.AppSettings[KeyPort].ToString(), out port);
                    if (port > 0)
                        _port = port;
                }
                return _port;
            }
        }
    }
}
