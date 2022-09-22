using System.Threading;
using System.Threading.Tasks;
using BugRepro.Modules.Invites;
using BugRepro.Modules.Members;
using Microsoft.Extensions.DependencyInjection;

namespace BugRepro
{
    internal static class Program
    {
        private static void Main(string[] args)
            => MainAsync().GetAwaiter().GetResult();
        
        private static async Task MainAsync()
        {
            ConfigureServices();
            
            await Task.Delay(Timeout.Infinite);
        }

        private static ServiceProvider ConfigureServices()
        {
            var Services = new ServiceCollection()
                .AddSingleton<MemberDB>()
                .AddSingleton<InvitesModule>()
                .BuildServiceProvider();
            
            //Pre-init modules
            Services.GetRequiredService<MemberDB>(); //This calls ConnectionPool.CreateOrGetConnection(""), which is partially responsible for the segfault

            Services.GetRequiredService<InvitesModule>().OnReady(); //This writes to non-blocking dictionary, which is partially responsible for the segfault

            return Services;
        }
    }
}
