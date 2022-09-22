using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NETBuddy.Modules.Invites;
using NETBuddy.Modules.Members;

namespace NETBuddy
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
