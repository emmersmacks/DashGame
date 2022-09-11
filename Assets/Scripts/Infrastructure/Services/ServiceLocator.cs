using System;
using System.Collections.Generic;

namespace Infrastructure.Services
{
    public class ServiceLocator
    {
        private static Dictionary<Type, IService> _services;

        public ServiceLocator()
        {
            _services = new Dictionary<Type, IService>();
        }

        public void RegisterService<TService>(IService service) where TService : IService
        {
            _services.Add(typeof(TService), service);
        }

        public static TService GetService<TService>() where TService : IService
        {
            try
            {
                return (TService)_services[typeof(TService)];
            }
            catch (KeyNotFoundException)
            {
                throw new Exception("The requested service is not registered");
            }
        }
    }
}