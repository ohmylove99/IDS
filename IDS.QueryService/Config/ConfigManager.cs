using System;
using System.Configuration;

namespace IDS.QueryService.Config
{
    /// <summary>
    /// 
    /// </summary>
    public class ConfigManager
    {
        #region Pub Const
        public const string KeyPort = "app.port";
        public const int DefaultPort = 9000;
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
        /// Get KeyPort Value from config
        /// </summary>
        public int Port {
            get {
                return GetInt(KeyPort, DefaultPort);
            }
        }
        /// <summary>
        /// Get Int Value from Config
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultVal"></param>
        /// <returns></returns>
        public int GetInt(string key, int defaultVal = 0)
        {
            if (ConfigurationManager.AppSettings[key] == null)
                return defaultVal;
            else
            {
                int val = defaultVal;
                Int32.TryParse(ConfigurationManager.AppSettings[key].ToString(), out val);
                return val;
            }
        }
        /// <summary>
        /// Get String Value from Config
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetString(string key)
        {
            if (ConfigurationManager.AppSettings[key] == null)
                return string.Empty;
            else
            {
                return ConfigurationManager.AppSettings[key].ToString();
            }
        }
    }
}
