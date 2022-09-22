using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using NonBlocking;

namespace BugRepro.Modules.Invites
{
    public class InvitesModule
    {
        private ConcurrentDictionary<string, string> CachedInvites;

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static async Task<IReadOnlyCollection<ulong>> SimulateGetItems()
        {
            await Task.Delay(1000);

            var List = new List<ulong>(69);

            for (ulong I = 0; I < 100; I++)
            {
                List.Add(I);
            }
            
            return List;
        }

        public async Task OnReady()
        {
            var Items = await SimulateGetItems();

            CachedInvites = new ConcurrentDictionary<string, string>();
            
            foreach (var Item in Items)
            {
                CachedInvites[Item.ToString()] = default;
            }

            Console.WriteLine("Bleh");
        }
    }
}