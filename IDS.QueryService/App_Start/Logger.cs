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

        private bool IgnoreRoutes(string path)
        {
            if (!string.IsNullOrEmpty(path) && path.StartsWith("/swagger"))
                return true;
            return false;
        }

        public Task Invoke(IDictionary<string, object> environment)
        {
            string path = GetValueFromEnvironment(environment, OwinConstants.RequestPath);
            bool ignore = IgnoreRoutes(path);
            if (ignore)
                return _next.Invoke(environment);

            string method = GetValueFromEnvironment(environment, OwinConstants.RequestMethod);
            string requestBody;
            Stream stream = (Stream)environment[OwinConstants.RequestBody];
            using (StreamReader sr = new StreamReader(stream))
            {
                requestBody = sr.ReadToEnd();
            }
            environment[OwinConstants.RequestBody] = new MemoryStream(Encoding.UTF8.GetBytes(requestBody));

            Log.Information("Entry\t{0}\t{1}\t{2}", method, path, requestBody);

            Stopwatch stopWatch = Stopwatch.StartNew();
            return _next(environment).ContinueWith(t =>
            {
                Log.Information("Exit\t{0}\t{1}\t{2}\t{3}\t{4}", method, path, stopWatch.ElapsedMilliseconds,
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
    }
}