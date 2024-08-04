using System.Reflection;
using Dedsi.Core.DependencyInjections;
using Microsoft.Extensions.DependencyInjection;

namespace Dedsi.AspNetCore;

public static class DependencyInjectionExtensions
{
    /// <summary>
    /// 添加依赖注入
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
    {
        var assemblies = GetAllAssembly();
        
        typeof(IScopedDependency).AddDependencyInjectionByType(assemblies, services, ServiceLifetime.Scoped);
        typeof(ISingletonDependency).AddDependencyInjectionByType(assemblies, services, ServiceLifetime.Singleton);
        typeof(ITransientDependency).AddDependencyInjectionByType(assemblies, services, ServiceLifetime.Transient);
        
        return services;
    }

    /// <summary>
    /// 扫描程序集，进行依赖注入
    /// </summary>
    /// <param name="baseType"></param>
    /// <param name="assemblies"></param>
    /// <param name="services"></param>
    /// <param name="lifetime"></param>
    private static void AddDependencyInjectionByType(this Type baseType,List<Assembly> assemblies,IServiceCollection services, ServiceLifetime lifetime)
    {
        foreach (var assembly in assemblies)
        {
            var interfaceTypes = assembly.DefinedTypes
                .Where(a => a.IsInterface && baseType.IsAssignableFrom(a) && a != baseType)
                .ToArray();
            
            var implementTypes = assembly.DefinedTypes
                .Where(a => a.IsClass && baseType.IsAssignableFrom(a) && a != baseType)
                .ToArray();

            foreach (var implementType in implementTypes)
            {
                var interfaceType = interfaceTypes.FirstOrDefault(a => a.IsAssignableFrom(implementType));
                if (interfaceType != null)
                {
                    switch (lifetime)
                    {
                        case ServiceLifetime.Singleton:
                            services.AddSingleton(interfaceType, implementType);
                            break;
                        case ServiceLifetime.Scoped:
                            services.AddSingleton(interfaceType, implementType);
                            break;
                        case ServiceLifetime.Transient:
                            services.AddTransient(interfaceType, implementType);
                            break;
                    }
                }
            }
        }
    }
    
    /// <summary>
    /// 获取全部 Assembly
    /// </summary>
    /// <returns></returns>
    private static List<Assembly> GetAllAssembly()
    {

        var allAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();

        HashSet<string> loadedAssemblies = new();

        foreach (var item in allAssemblies)
        {
            loadedAssemblies.Add(item.FullName!);
        }

        Queue<Assembly> assembliesToCheck = new();
        assembliesToCheck.Enqueue(Assembly.GetEntryAssembly()!);

        while (assembliesToCheck.Any())
        {
            var assemblyToCheck = assembliesToCheck.Dequeue();
            foreach (var reference in assemblyToCheck!.GetReferencedAssemblies())
            {
                if (!loadedAssemblies.Contains(reference.FullName))
                {
                    var assembly = Assembly.Load(reference);

                    assembliesToCheck.Enqueue(assembly);

                    loadedAssemblies.Add(reference.FullName);

                    allAssemblies.Add(assembly);
                }
            }
        }

        return allAssemblies;
    }
    
}