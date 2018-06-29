using UnityEngine;
using UnityEditor;
using Substance.Game;

namespace Substance.Editor
{
    [CustomPropertyDrawer(typeof(SubstanceParameter))]
    public class SubstanceParameterDrawer : AssetReferenceDrawer
    {
        protected override string AssetField
        {
            get { return "graph"; }
        }

        protected override string TargetField
        {
            get { return "parameter"; }
        }

        protected override void DrawTargetField(Rect position, SerializedProperty property, SerializedProperty assetProperty)
        {
            SubstanceGraph graph = assetProperty.objectReferenceValue as SubstanceGraph;

            int inputCount = NativeFunctions.cppGetNumInputs(graph.nativeHandle);
            Globals.Input[] inputs = Globals.GetNativeInputs(graph, inputCount);
            GUIContent[] labels = new GUIContent[inputCount];
            
            for (int i = 0; i < inputCount; i++)
            {
                labels[i] = new GUIContent(string.Format("{0}{1} ({2})", (string.IsNullOrEmpty(inputs[i].group) ? "" : inputs[i].group + "/"), inputs[i].label, inputs[i].name));
            }

            int index = 0;

            for (int i = 0; i < inputCount; i++)
            {
                if(property.stringValue == inputs[i].name)
                {
                    index = i;
                    break;
                }
            }

            EditorGUI.BeginChangeCheck();
            index = EditorGUI.Popup(position, index, labels);
            if(EditorGUI.EndChangeCheck())
            {
                property.stringValue = inputs[index].name;
            }
        }
    }
}