using System;
using System.Linq;
using AutoMapper;

namespace UmbracoFood
{
    public class AutomapperConfig
    {
        public static void Configure()
        {
            RegisterMappingProfiles();
        }

        private static void RegisterMappingProfiles()
        {
            var assemblies =  AppDomain.CurrentDomain.GetAssemblies();

            foreach (var assembly in assemblies)
            {
                var profiles = assembly.GetTypes()
                    .Where(type => type != typeof (Profile) && typeof (Profile).IsAssignableFrom(type))
                    .Select(Activator.CreateInstance)
                    .Cast<Profile>();

                foreach (var profile in profiles)
                {
                    Mapper.Configuration.AddProfile(profile);
                }
            }
        }

    }
}