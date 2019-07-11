using System;

namespace Accolades.Maije.Crosscutting.Attributes
{
    /// <summary>
    /// Attribute used to generate AllOf property in swagger.json
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class SubtypeOfAttribute : Attribute
    {
    }
}
