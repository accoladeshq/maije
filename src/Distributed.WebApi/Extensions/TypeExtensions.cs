using Accolades.Maije.AppService;
using Accolades.Maije.Crosscutting.Exceptions;
using Accolades.Maije.Domain.Contracts;
using Accolades.Maije.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Accolades.Maije.Distributed.WebApi.Extensions
{
    internal static class TypeExtensions
    {
        /// <summary>
        /// Get value indicating if the type is a controller or not
        /// </summary>
        /// <param name="type">The type to check</param>
        /// <returns></returns>
        public static bool IsMaijeController(this Type type)
        {
            return type.IsSubclassOf(typeof(MaijeController));
        }

        /// <summary>
        /// Gets the existing <see cref="IMaijeDbContext"/>
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        public static Type GetMaijeDbContext(this IEnumerable<Type> types)
        {
            var dbContextType = types.Where(t => typeof(IMaijeDbContext).IsAssignableFrom(t)).FirstOrDefault();

            if(dbContextType == null)
            {
                throw new InfrastructureException("There is no or too many IMaijeDbContext found in the scan assemblies");
            }

            return dbContextType;
        }

        /// <summary>
        /// Get the controller required services
        /// </summary>
        /// <param name="types">All available types</param>
        /// <param name="controllerType">The controller type</param>
        /// <returns></returns>
        public static Dictionary<Type, Type> GetControllerRequiredServices(this IEnumerable<Type> types, Type controllerType)
        {
            var requiredServices = new Dictionary<Type, Type>();

            var controllerArguments = controllerType.GetControllerGenericArguments();

            var dto = controllerArguments[0];
            var identifier = controllerArguments[1];
            var entity = types.Where(t => t.Name == dto.Name.Replace("Dto", "")).First();
            
            // Repository setup
            var openRepositoryInterfaceType = typeof(IMaijeRepository<,>);
            var repositoryInterfaceType = openRepositoryInterfaceType.MakeGenericType(entity, identifier);

            var openRepositoryImplementationType = typeof(MaijeRepository<,>);
            var repositoryImplementationType = openRepositoryImplementationType.MakeGenericType(entity, identifier);

            requiredServices.Add(repositoryInterfaceType, repositoryImplementationType);
            requiredServices.Add(typeof(IMaijeRepository), repositoryImplementationType);

            // AppService Setup
            var openAppServiceInterfaceType = typeof(IMaijeAppService<,>);
            var appServiceInterfaceType = openAppServiceInterfaceType.MakeGenericType(dto, identifier);

            var openAppServiceImplementationType = typeof(MaijeAppServiceBase<,,,>);
            var appServiceImplementationType = openAppServiceImplementationType.MakeGenericType(dto, entity, identifier, repositoryInterfaceType);

            requiredServices.Add(appServiceInterfaceType, appServiceImplementationType);

            return requiredServices;
        }

        /// <summary>
        /// Gets controller generic arguments
        /// </summary>
        /// <param name="controllerType">The controller type</param>
        /// <returns></returns>
        private static Type[] GetControllerGenericArguments(this Type controllerType)
        {
            var parentType = controllerType;

            do
            {
                parentType = parentType.BaseType;

                if(parentType.BaseType == null)
                {
                    throw new Exception("The controller type not inherit from MaijeController");
                }

            } while (parentType.Name != (typeof(MaijeController<,>).Name)); // du to open generic we cannot compare types

            return parentType.GenericTypeArguments;
        }
    }
}
