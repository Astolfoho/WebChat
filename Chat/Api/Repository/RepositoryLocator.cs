using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Api.Repository
{
    public class RepositoryLocator
    {
        public static T Get<T>()
        {
            return GetEF<T>();
        }

        private static T GetMemory<T>()
        {
            var types = Assembly.GetExecutingAssembly().GetTypes().Where(w => typeof(T).IsAssignableFrom(w));
            var type = types.FirstOrDefault(f => f.IsInterface == false 
                                    && f.Namespace.EndsWith("Memory", StringComparison.InvariantCultureIgnoreCase));
            var instance = Activator.CreateInstance(type);
            return (T)instance;
        }

        private static T GetEF<T>()
        {
            var types = Assembly.GetExecutingAssembly().GetTypes().Where(w => typeof(T).IsAssignableFrom(w));
            var type = types.FirstOrDefault(f => f.IsInterface == false
                                    && f.Namespace.EndsWith("EF", StringComparison.InvariantCultureIgnoreCase));
            var instance = Activator.CreateInstance(type);
            return (T)instance;
        }


    }
}
