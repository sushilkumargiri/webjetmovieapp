using log4net.Config;
using log4net;

namespace WebjetMovieAppBL
{
    /// <summary>
    /// Log level is defined as enum
    /// </summary>
    public enum LogLevel
    {
        DEBUG,
        ERROR,
        FATAL,
        INFO,
        WARN
    }
    public static class Logger
    {
        static readonly ILog logger = log4net.LogManager.GetLogger(typeof(Logger));

        static Logger()
        {
            XmlConfigurator.Configure();
        }
        /// <summary>
        /// Write all logs in one method. Supply LogLevel and logText as parameter
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="log"></param>
        public static void Write(LogLevel logLevel, string log)
        {
            if (logLevel.Equals(LogLevel.DEBUG))
            {
                logger.Debug(log);
            }
            else if (logLevel.Equals(LogLevel.ERROR))
            {
                logger.Error(log);
            }
            else if (logLevel.Equals(LogLevel.FATAL))
            {
                logger.Fatal(log);
            }
            else if (logLevel.Equals(LogLevel.INFO))
            {
                logger.Info(log);
            }
            else if (logLevel.Equals(LogLevel.WARN))
            {
                logger.Warn(log);
            }
        }
    }
}
