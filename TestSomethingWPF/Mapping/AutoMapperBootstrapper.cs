using System;
using System.Linq;
using System.Reflection;

namespace TestSomethingWPF.Mapping
{
    public class AutoMapperBootstrapper
    {
        public static void Run()
        {
            var mapCreatorTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(x => typeof(IMapCreator).IsAssignableFrom(x) && x.IsClass && !x.IsAbstract);

            foreach (var mapCreatorType in mapCreatorTypes)
            {
                var mapCreator = (IMapCreator) Activator.CreateInstance(mapCreatorType);
                mapCreator.CreateMap();
            }
        }
    }
}
