using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Spring.Context;
using Spring.Context.Support;

namespace facedown.Models
{
    /// <summary>
    /// Clase factory que encierra las llamadas el IoC container
    /// </summary>
    public static class ServiceFactory
    {
        /// <summary>
        /// el container
        /// </summary>
        private static IApplicationContext ctx = ContextRegistry.GetContext();

        public static Object GetImpl(string impl)
        {
            return ctx.GetObject(impl);
        }
    }
}