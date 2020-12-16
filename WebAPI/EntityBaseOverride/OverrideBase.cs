using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
using System;

namespace WebAPI.EntityBaseOverride
{
    /// <summary>
    /// 
    /// </summary>
    public class OverrideBase : CSharpEntityTypeGenerator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="annotationCodeGenerator"></param>
        /// <param name="cSharpHelper"></param>
        public OverrideBase(IAnnotationCodeGenerator annotationCodeGenerator, ICSharpHelper cSharpHelper)
            : base(annotationCodeGenerator, cSharpHelper)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="namespace"></param>
        /// <param name="useDataAnnotations"></param>
        /// <returns></returns>
        public override string WriteCode(IEntityType entityType, string @namespace, bool useDataAnnotations)
        {
            string str = base.WriteCode(entityType, @namespace, useDataAnnotations).Replace("public partial class " + entityType.Name, "public class " + entityType.Name + " : IEntity");
            string oldValue = "using System;";
            string newValue = oldValue + Environment.NewLine + "using Core.Entities;";
            return str.Replace(oldValue, newValue);
        }

    }
}
