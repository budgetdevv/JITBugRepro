using System.Runtime.CompilerServices;
using NonBlocking;

namespace MongoModule
{
    internal static class ConnectionPool
    {
        private static readonly ConcurrentDictionary<string, string> Connections;

        static ConnectionPool()
        {
            Connections = new ConcurrentDictionary<string, string>
            {
                [""] = "Foo" //Local
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static string CreateOrGetConnection(string ConnectionString)
        {
            if (Connections.TryGetValue(ConnectionString, out var Connection))
            {
                return Connection;
            }

            return CreateConnection(ConnectionString);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static string CreateConnection(string ConnectionString)
        {
            return Connections[ConnectionString] = "Foo";
        }
    }
}