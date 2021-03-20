using System;
using System.Threading.Tasks;

namespace DbIntegrationApp
{
    internal class Program
    {
        [STAThread]
        static async Task Main() => await Startup.InitializeAsync();
    }
}
