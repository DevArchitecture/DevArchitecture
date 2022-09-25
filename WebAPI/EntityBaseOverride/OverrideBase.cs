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
        /// <param name="useNullableReferenceTypes"></param>
        /// <returns></returns>
        public override string WriteCode(IEntityType entityType, string @namespace, bool useDataAnnotations, bool useNullableReferenceTypes)
        {
            var str = base.WriteCode(entityType, @namespace, useDataAnnotations, useNullableReferenceTypes).Replace(
                "public partial class " + entityType.Name, "public class " + entityType.Name + " : IEntity");
            var oldValue = "using System;";
            var newValue = oldValue + Environment.NewLine + "using Core.Entities;";
            return str.Replace(oldValue, newValue);
        }
    }
}