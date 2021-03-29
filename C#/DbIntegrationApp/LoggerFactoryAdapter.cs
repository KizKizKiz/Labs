using System;
using System.Collections.Generic;

using Serilog;
using Serilog.Core;
using Serilog.Events;

using NHibernate;
using Microsoft.Extensions.Logging;

namespace DbIntegrationApp
{
    public sealed class LoggerFactoryAdapter : INHibernateLoggerFactory
    {
        public LoggerFactoryAdapter(Microsoft.Extensions.Logging.ILoggerFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }
        private class LoggerAdapter : INHibernateLogger
        {
            public LoggerAdapter(Microsoft.Extensions.Logging.ILogger logger)
            {
                _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            }

            public bool IsEnabled(NHibernateLogLevel logLevel)
            {
                return logLevel == NHibernateLogLevel.None || _logger.IsEnabled(MapLevels[logLevel]);
            }

            public void Log(NHibernateLogLevel logLevel, NHibernateLogValues state, Exception exception)
            {
                _logger.Log(MapLevels[logLevel], exception, state.Format, state.Args);
            }

            private static readonly Dictionary<NHibernateLogLevel, LogLevel> MapLevels = new Dictionary<NHibernateLogLevel, LogLevel>
            {
                { NHibernateLogLevel.Trace, LogLevel.Trace },
                { NHibernateLogLevel.Info, LogLevel.Information },
                { NHibernateLogLevel.Debug, LogLevel.Debug },
                { NHibernateLogLevel.Warn, LogLevel.Warning },
                { NHibernateLogLevel.Error, LogLevel.Error },
                { NHibernateLogLevel.Fatal, LogLevel.Critical }
            };

            private readonly Microsoft.Extensions.Logging.ILogger _logger;
        }

        public INHibernateLogger LoggerFor(string keyName)
            => new LoggerAdapter(_factory.CreateLogger(keyName));

        public INHibernateLogger LoggerFor(Type type)
            => new LoggerAdapter(_factory.CreateLogger(type));

        private readonly Microsoft.Extensions.Logging.ILoggerFactory _factory;
    }
}