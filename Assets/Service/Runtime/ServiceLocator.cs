using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GigaCreation.Tools.Service
{
    public static class ServiceLocator
    {
        /// <summary>
        /// The dictionary to register services.
        /// </summary>
        private static readonly Dictionary<Type, IService> s_services = new();

        /// <summary>
        /// Register a service. If a service of the same type is already registered, you should unregister it first.
        /// </summary>
        /// <param name="service">The service to register.</param>
        /// <typeparam name="TService">The type of the service.</typeparam>
        public static void Register<TService>(TService service) where TService : class, IService
        {
            if (service is null)
            {
                Debug.LogError("It does not allow to register null.");
                return;
            }

            Type type = typeof(TService);

#if !UNITY_2021_2_OR_NEWER
            if (!s_services.TryAdd(type, service))
            {
                Debug.LogWarning($"A service of the same type is already registered: {type.Name}");
            }
#else
            if (s_services.ContainsKey(type))
            {
                Debug.LogWarning($"A service of the same type is already registered: {type.Name}");
                return;
            }

            s_services.Add(type, service);
#endif
        }

        /// <summary>
        /// Unregister a service.
        /// </summary>
        /// <param name="service">The service to unregister.</param>
        /// <typeparam name="TService">The type of the service.</typeparam>
        public static void Unregister<TService>(TService service) where TService : class, IService
        {
            KeyValuePair<Type, IService> registeredService
                = s_services.FirstOrDefault(pair => Equals(pair.Value, service));

            if (registeredService.Key == null)
            {
                Debug.LogWarning($"The passed service is not registered: {service.GetType()}");
                return;
            }

            if (registeredService.Value is IDisposable disposable)
            {
                disposable.Dispose();
            }

            s_services.Remove(registeredService.Key);
        }

        /// <summary>
        /// Checks if a service of the specified type is registered.
        /// </summary>
        /// <param name="includeDerivedType">If true, the search includes derived types.</param>
        /// <typeparam name="TService">The type of a service to check.</typeparam>
        /// <returns>Returns true if a service of the given type is registered.</returns>
        public static bool IsRegistered<TService>(bool includeDerivedType = false) where TService : class, IService
        {
            Type type = typeof(TService);

            if (s_services.ContainsKey(type))
            {
                return true;
            }

            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (includeDerivedType && s_services.Any(pair => type.IsAssignableFrom(pair.Key)))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Checks if the specified service is registered.
        /// </summary>
        /// <param name="service">The service to check.</param>
        /// <param name="includeDerivedType">If true, the search includes derived types.</param>
        /// <typeparam name="TService">The type of a service to check.</typeparam>
        /// <returns>Returns true if the given service is registered.</returns>
        public static bool IsRegistered<TService>(TService service, bool includeDerivedType = false)
            where TService : class, IService
        {
            if (s_services.ContainsValue(service))
            {
                return true;
            }

            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (includeDerivedType && s_services.Any(pair => service.GetType().IsAssignableFrom(pair.Key)))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Get a service.
        /// </summary>
        /// <param name="includeDerivedType">If true, the search includes derived types.</param>
        /// <typeparam name="TService">The type of a service.</typeparam>
        /// <returns>The service of the requested type.</returns>
        public static TService Get<TService>(bool includeDerivedType = false) where TService : class, IService
        {
            Type type = typeof(TService);

            if (s_services.TryGetValue(type, out IService service))
            {
                return service as TService;
            }

            if (includeDerivedType)
            {
                IService derivedService = s_services.FirstOrDefault(pair => type.IsAssignableFrom(pair.Key)).Value;

                if (derivedService != null)
                {
                    return derivedService as TService;
                }
            }

            Debug.LogError($"A service of the given type is not registered: {type.Name}");
            return null;
        }

        /// <summary>
        /// Try to get a service.
        /// </summary>
        /// <param name="service">The service of the requested type.</param>
        /// <param name="includeDerivedType">If true, the search includes derived types.</param>
        /// <typeparam name="TService">The type of a service.</typeparam>
        /// <returns>Returns true if the get was successful.</returns>
        public static bool TryGet<TService>(out TService service, bool includeDerivedType = false)
            where TService : class, IService
        {
            Type type = typeof(TService);

            if (s_services.TryGetValue(type, out IService registeredService))
            {
                service = registeredService as TService;
                return true;
            }

            if (includeDerivedType)
            {
                IService derivedService = s_services.FirstOrDefault(pair => type.IsAssignableFrom(pair.Key)).Value;

                if (derivedService != null)
                {
                    service = derivedService as TService;
                    return true;
                }
            }

            service = null;
            return false;
        }

        /// <summary>
        /// Unregister all services.
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ClearServices()
        {
            s_services.Clear();
        }
    }
}
