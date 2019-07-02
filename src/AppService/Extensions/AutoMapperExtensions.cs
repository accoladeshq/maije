using AutoMapper;
using System;
using System.Collections.Generic;

namespace Accolades.Maije.AppService.Extensions
{
    internal static class AutoMaperExtensions
    {
        public static Action<IMappingOperationOptions> ToMappingOperationOptionsAction(this Dictionary<string, string> dictionary)
        {
            return o =>
            {
                foreach (var item in dictionary)
                {
                    o.Items[item.Key] = item.Value;
                }
            };
        }
    }
}
