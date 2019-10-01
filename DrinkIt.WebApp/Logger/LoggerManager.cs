using NLog;
using NLog.Config;
using NLog.Targets;
using System;

namespace DrinkIt.WebApp.Logger
{
    public sealed class LoggerManager
    {
        private static readonly Lazy<LoggerManager> _instance
            = new Lazy<LoggerManager>(() =>
            {
                return new LoggerManager();
            });

        public static LoggerManager Instance => _instance.Value;

        private LoggerManager()
        {
            LoggingConfiguration _config = new LoggingConfiguration();

            var fileTarget = new FileTarget("Log")
            {
                FileName = @"${basedir}\Logs\Log.txt",
                Layout = "${longdate}|${level}|${message}|${exception}"
            };

            _config.AddTarget(fileTarget);

            _config.AddRuleForAllLevels(fileTarget);

            LogManager.Configuration = _config;
        }

        public ILogger Logger => LogManager.GetCurrentClassLogger();
    }
}