using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Api.Repositorios
{
    public class LocalizadorDeRepozitorios
    {
        public static T Pegar<T>()
        {
            return PegarEF<T>();
        }

        private static T PegarMemoria<T>()
        {
            var types = Assembly.GetExecutingAssembly().GetTypes().Where(w => typeof(T).IsAssignableFrom(w));
            var type = types.FirstOrDefault(f => f.IsInterface == false 
                                    && f.Namespace.EndsWith("Memoria", StringComparison.InvariantCultureIgnoreCase));
            var instance = Activator.CreateInstance(type);
            return (T)instance;
        }

        private static T PegarEF<T>()
        {
            var types = Assembly.GetExecutingAssembly().GetTypes().Where(w => typeof(T).IsAssignableFrom(w));
            var type = types.FirstOrDefault(f => f.IsInterface == false
                                    && f.Namespace.EndsWith("EF", StringComparison.InvariantCultureIgnoreCase));
            var instance = Activator.CreateInstance(type);
            return (T)instance;
        }


    }
}
