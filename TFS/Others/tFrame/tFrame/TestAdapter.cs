using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace tFrame
{
    public static class TestAdapter
    {
        static TestAdapter()
        {
            registry = new Dictionary<Type, Type>();
        }
        private static void ValidateRegistry()
        {
            if (registry == null)
                registry = new Dictionary<Type, Type>();
        }
        private static void ValidateRegistry(bool throwExceptionWhenNotInitialized)
        {
            if (registry == null)
            {
                if (throwExceptionWhenNotInitialized)
                    throw new ApplicationException("Register has not been initialized");
                else
                    registry = new Dictionary<Type, Type>();
            }
        }
        private static Dictionary<Type, Type> registry = null;
        public static void Register<TI, TC>()
        {
            ValidateRegistry();
            registry.Add(typeof(TI), typeof(TC));
        }
        public static TI GetInstance<TI>()
        {
            ValidateRegistry(true);
            if (!registry.ContainsKey(typeof(TI)))
                throw new ArgumentException(string.Format("{0} has not been registered.", typeof(TI).Name));
            var tc = registry[typeof(TI)];
            object obj = null;
            try
            {
                obj = Assembly.GetAssembly(typeof(TI)).CreateInstance(tc.Name);
                if (obj.GetType().IsAssignableFrom(typeof(TI)))
                    return (TI)obj;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(string.Format("Cannot create an instance of {0}. Inner exception may hold more details.", tc.GetType().Name), ex);
            }
            return default(TI);
        }
        public static TI Create<TI, TC>()
        {
            object obj = null;
            try
            {
                obj = Assembly.GetAssembly(typeof(TC)).CreateInstance(typeof(TC).Name);
                if (obj.GetType().IsAssignableFrom(typeof(TI)))
                    return (TI)obj;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(string.Format("Cannot create an instance of {0}.", typeof(TC).Name), ex);
            }
            throw new ArgumentException(string.Format("{0} does not implement {1}.", typeof(TC).Name, typeof(TI).Name));
        }
    }
}
