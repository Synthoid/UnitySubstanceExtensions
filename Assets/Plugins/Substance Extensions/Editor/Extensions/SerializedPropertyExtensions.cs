using UnityEngine;
using UnityEditor;

namespace Substance.Editor
{
    /// <summary>
    /// Class containing extension methods for the <see cref="SerializedProperty"/> class.
    /// </summary>
	public static class SerializedPropertyExtensions
	{
        /// <summary>
        /// Returns a sibling property to the given property.
        /// </summary>
        /// <param name="property">The property to get a sibling property for.</param>
        /// <param name="path">The path to the target property. This is usually just the property's name.</param>
        /// <param name="isRoot">If true, the target property will be navigated to starting at the given property's SerializedObject.</param>
        public static SerializedProperty GetSiblingProperty(this SerializedProperty property, string path, bool isRoot = false)
        {
            SerializedProperty targetProperty = null;

            if (!isRoot && property.propertyPath.IndexOf('.') > 0)
            {
                path = property.propertyPath.Substring(0, property.propertyPath.LastIndexOf('.') + 1) + path;
            }

            targetProperty = property.serializedObject.FindProperty(path);

            return targetProperty;
        }
    }
}