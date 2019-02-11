using UnityEngine;
using UnityEditor;

namespace SubstanceExtensionsEditor
{
	public static class CustomEditorUtility
	{
        /// <summary>
        /// Selects an object at the given path if it is inside the Assets folder, or reveals it in finder if it is outside the project.
        /// </summary>
        /// <param name="path">The path to examine.</param>
        /// <param name="relative">If true, the path is relative to the project folder. NOTE: This is the root folder for the entire project, not the Assets folder.</param>
        /// <param name="refresh">If true, will reimport the asset before selecting it. This has no effect if the path is outside the project.</param>
        public static void SelectOrReveal(string path, bool relative=false, bool refresh=false)
        {
            if (path.StartsWith(Application.dataPath) || relative)
            {
                //Select the generated file if it was created in the project.
                if (!relative) path = path.Replace(Application.dataPath, "Assets/");

                if (refresh) AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);

                Object target = AssetDatabase.LoadAssetAtPath<Object>(path);

                if (target == null)
                {
                    AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
                    target = AssetDatabase.LoadAssetAtPath<Object>(path);
                }

                if (target != null)
                {
                    Selection.activeObject = target;
                }
                else
                {
                    Debug.LogWarning("Asset could not be loaded at path: " + path);
                }
            }
            else
            {
                //Show the exported file in finder if it was created outside the project.
                EditorUtility.RevealInFinder(path);
            }
        }
    }
}