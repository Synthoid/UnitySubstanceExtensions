using UnityEngine;
using UnityEditor;
using Substance.Game;
using SubstanceExtensions;

namespace SubstanceExtensionsEditor
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
            SerializedProperty typeProperty = property.GetSiblingProperty("type");
            
            int inputCount = NativeFunctions.cppGetNumInputs(graph.nativeHandle);
            NativeTypes.Input[] inputs = NativeTypes.GetNativeInputs(graph, inputCount);
            GUIContent[] labels = new GUIContent[inputCount];
            
            for (int i = 0; i < inputCount; i++)
            {
                labels[i] = new GUIContent(string.Format("{0}{1} ({2} - {3})", (string.IsNullOrEmpty(inputs[i].group) ? "" : inputs[i].group + "/"), inputs[i].label, inputs[i].name, (SubstanceInputType)inputs[i].substanceInputType), inputs[i].name);
            }

            int index = -1;

            for (int i = 0; i < inputCount; i++)
            {
                if(property.stringValue == inputs[i].name)
                {
                    index = i;
                    break;
                }
            }

            if(index < 0 && inputCount > 0)
            {
                index = 0;
                property.stringValue = inputs[index].name;
                typeProperty.intValue = inputs[index].substanceInputType;
            }

            EditorGUI.BeginChangeCheck();
            index = EditorGUI.Popup(position, index, labels);
            if(EditorGUI.EndChangeCheck())
            {
                property.stringValue = inputs[index].name;
                typeProperty.intValue = inputs[index].substanceInputType;
            }
        }
    }
}