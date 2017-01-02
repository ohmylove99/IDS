namespace IDS.QueryService
{
    using Constant;
    using Serilog;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    /// <summary>
    /// 
    /// </summary>
    public class Logger
    {
        private readonly ILogger _log = Log.ForContext<Logger>();

        private readonly Func<IDictionary<string, object>, Task> _next;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        public Logger(Func<IDictionary<string, object>, Task> next)
        {
            if (next == null)
            {
                throw new ArgumentNullException("next");
            }
            _next = next;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="environment"></param>
        /// <returns></returns>
        public Task Invoke(IDictionary<string, object> environment)
        {
            string path = GetValueFromEnvironment(environment, OwinConstants.RequestPath);

            if (IgnoreLogRoutes(path))
                return _next.Invoke(environment);

            string method = GetValueFromEnvironment(environment, OwinConstants.RequestMethod);
            string requestBody;
            Stream stream = (Stream)environment[OwinConstants.RequestBody];
            using (StreamReader sr = new StreamReader(stream))
            {
                requestBody = sr.ReadToEnd();
            }
            environment[OwinConstants.RequestBody] = new MemoryStream(Encoding.UTF8.GetBytes(requestBody));

            _log.Information("Entry\t{0}\t{1}\t{2}", method, path, requestBody);

            Stopwatch stopWatch = Stopwatch.StartNew();
            return _next(environment).ContinueWith(t =>
            {
                _log.Information("Exit\t{0}\t{1}\t{2}\t{3}\t{4}", method, path, stopWatch.ElapsedMilliseconds,
                    GetValueFromEnvironment(environment, OwinConstants.ResponseStatusCode),
                    GetValueFromEnvironment(environment, OwinConstants.ResponseReasonPhrase));
                return t;
            });
        }

        private static string GetValueFromEnvironment(IDictionary<string, object> environment, string key)
        {
            object value;
            environment.TryGetValue(key, out value);
            return Convert.ToString(value, CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IgnoreLogRoutes(string path)
        {
            if (!string.IsNullOrEmpty(path) && (path.StartsWith("/swagger") || path.StartsWith("/metrics")))
                return true;
            return false;
        }
    }
}