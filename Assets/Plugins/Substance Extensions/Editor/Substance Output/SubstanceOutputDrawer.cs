using UnityEngine;
using UnityEditor;
using Substance.Game;

namespace Substance.Editor
{
    [CustomPropertyDrawer(typeof(SubstanceOutput))]
	public class SubstanceOutputDrawer : AssetReferenceDrawer
	{
        protected override string AssetField
        {
            get { return "graph"; }
        }

        protected override string TargetField
        {
            get { return "outputName"; }
        }

        protected override void DrawTargetField(Rect position, SerializedProperty property, SerializedProperty assetProperty)
        {
            SubstanceGraph graph = assetProperty.objectReferenceValue as SubstanceGraph;

            string[] outputNames = graph.outputNames.Split(',');
            GUIContent[] labels = new GUIContent[outputNames.Length];

            for (int i = 0; i < labels.Length; i++)
            {
                labels[i] = new GUIContent(outputNames[i]);
            }

            int index = 0;

            for (int i = 0; i < outputNames.Length; i++)
            {
                if (property.stringValue == outputNames[i])
                {
                    index = i;
                    break;
                }
            }

            EditorGUI.BeginChangeCheck();
            index = EditorGUI.Popup(position, index, labels);
            if (EditorGUI.EndChangeCheck())
            {
                property.stringValue = outputNames[index];
            }
        }
    }
}