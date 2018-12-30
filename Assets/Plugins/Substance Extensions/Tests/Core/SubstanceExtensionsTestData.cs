using UnityEngine;
using UnityEditor;
using Substance.Game;

namespace SubstanceExtensions.Tests
{
    [InitializeOnLoad]
	public static class SubstanceExtensionsTestData
	{
        private const string SEARCH_CRITERIA = "t:SubstanceExtensionsTestDataCollection";
        
        static SubstanceExtensionsTestData()
        {
            FindInstance();
        }

        private static SubstanceExtensionsTestDataCollection instance;

        public static SubstanceExtensionsTestDataCollection Instance
        {
            get
            {
                if(instance == null) FindInstance();

                return instance;
            }
        }

        public static Substance.Game.Substance TestSubstance
        {
            get { return Instance.TestSubstance; }
        }


        private static void FindInstance()
        {
            string[] paths = AssetDatabase.FindAssets(SEARCH_CRITERIA);
            
            if(paths.Length > 0)
            {
                instance = AssetDatabase.LoadAssetAtPath<SubstanceExtensionsTestDataCollection>(AssetDatabase.GUIDToAssetPath(paths[0]));
            }
        }
	}
}