using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ProgNet.Service
{
    public class ProgNetHostedService : IHostedService
    {
        private readonly IApplicationLifetime _appLifetime;
        private readonly ILogger<ProgNetHostedService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _environment;

        public ProgNetHostedService(
            IConfiguration configuration,
            IHostingEnvironment environment,
            ILogger<ProgNetHostedService> logger,
            IApplicationLifetime appLifetime)
        {
            _configuration = configuration;
            _logger = logger;
            _appLifetime = appLifetime;
            _environment = environment;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("StartAsync method called in {0}", _environment.ApplicationName);

            _appLifetime.ApplicationStarted.Register(OnStarted);
            _appLifetime.ApplicationStopping.Register(OnStopping);
            _appLifetime.ApplicationStopped.Register(OnStopped);

            var sb = new StringBuilder();
            sb.AppendLine("List of configuration:");
            foreach (var configurationSection in _configuration.GetChildren())
            {
                sb.AppendLine($"{configurationSection.Key}: {configurationSection.Value}");
                foreach (var section in configurationSection.GetChildren())
                {
                    sb.AppendLine($"    {section.Key}: {section.Value}");
                }
            }
            
            _logger.LogInformation(sb.ToString());

            return Task.CompletedTask;
        }


        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("StopAsync method called in {0}", _environment.ApplicationName);

            return Task.CompletedTask;
        }

        private void OnStarted()
        {
            _logger.LogInformation("OnStarted method called in {0}", _environment.ApplicationName);

            // Post-startup
        }

        private void OnStopping()
        {
            _logger.LogInformation("OnStopping method called in {0}", _environment.ApplicationName);

            // On-stopping
        }

        private void OnStopped()
        {
            _logger.LogInformation("OnStopped method called in {0}", _environment.ApplicationName);

            // Post-stopped
        }
    }
}