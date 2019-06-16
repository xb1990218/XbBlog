using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLib
{
    /// <summary>
    /// IServiceCollection 容器的拓展类
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 程序集依赖注入
        /// </summary>
        /// <param name="services">服务实例</param>
        /// <param name="assemblyName">程序集名称。不带DLL</param>
        /// <param name="serviceLifetime">依赖注入的类型 可为空</param>
        public static void AddAssembly(this IServiceCollection services, string assemblyName, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services) + "为空");

            if (String.IsNullOrEmpty(assemblyName))
                throw new ArgumentNullException(nameof(assemblyName) + "为空");

            var assembly = Tools.GetAssemblyByName(assemblyName);

            if (assembly == null)
                throw new DllNotFoundException(nameof(assembly) + ".dll不存在");
            //找到当前程序集的类集合
            var types = assembly.GetTypes();
            //过滤筛选（是类文件，并且不是抽象类，不是泛型）
            var list = types.Where(o => o.IsClass && !o.IsAbstract && !o.IsGenericType).ToList();
            if (list == null && !list.Any())
                return;
            //遍历获取到的类
            foreach (var type in list)
            {
                //然后获取到类对应的接口
                var interfacesList = type.GetInterfaces();
                //校验接口存在则继续
                if (interfacesList == null || !interfacesList.Any())
                    continue;
                //获取到接口（第一个）
                var inter = interfacesList.First();
                switch (serviceLifetime)
                {
                    //根据条件，选择注册依赖的方法
                    case ServiceLifetime.Scoped:
                        //将获取到的接口和类注册进去
                        services.AddScoped(inter, type);
                        break;
                    case ServiceLifetime.Singleton:
                        services.AddScoped(inter, type);
                        break;
                    case ServiceLifetime.Transient:
                        services.AddTransient(inter, type);
                        break;
                }
            }
        }
    }
}
