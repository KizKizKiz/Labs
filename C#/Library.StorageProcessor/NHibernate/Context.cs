using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;

using NHibernate;

namespace Library.StorageProcessor.NHibernateAccessor
{
    public sealed class Context : IDisposable
    {
        public ISession CurrentSession => ThrowIfDisposedOrGet();

        public Context(
            IConfiguration configuration,
            ILogger<Context> logger,
            INHibernateLoggerFactory loggerFactory)
        {
            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }
            if (string.IsNullOrWhiteSpace(configuration.GetConnectionString("SPORT_GOODS_DB")))
            {
                throw new ArgumentException("The connection string is not present.", nameof(configuration));
            }
            _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            Initialize(configuration.GetConnectionString("SPORT_GOODS_DB"));
        }

        public void Dispose()
        {
            if (!_isDisposed)
            {
                _session?.Dispose();
                _sessionFactory?.Dispose();
                _isDisposed = true;
            }
        }

        private void Initialize(string connectionString)
        {
            NHibernateLogger.SetLoggersFactory(_loggerFactory);

            _sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectionString).ShowSql())
                .Mappings(c => c.FluentMappings.AddFromAssembly(typeof(Context).Assembly))
                .Cache(c => c.UseSecondLevelCache().ProviderClass<NHibernate.Cache.HashtableCacheProvider>().UseQueryCache().QueryCacheFactory<NHibernate.Cache.StandardQueryCacheFactory>())
                .BuildSessionFactory();

            _logger.LogInformation("The session factory is built.");

            OpenSession();
        }

        private void OpenSession()
        {
            try
            {
                _session = _sessionFactory!.OpenSession();

                var aa = _session.IsConnected;

                _logger.LogInformation("The session is opened.");
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, "Error occurred while opening session. See info in details.");

                Dispose();
                throw;
            }
        }

        private ISession ThrowIfDisposedOrGet()
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException(nameof(_session));
            }
            return _session!;
        }

        private bool _isDisposed;
        private readonly INHibernateLoggerFactory? _loggerFactory;
        private readonly ILogger<Context> _logger;
        private ISessionFactory? _sessionFactory;
        private ISession? _session;
    }
}