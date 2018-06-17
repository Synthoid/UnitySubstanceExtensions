using UnityEngine;
using UnityEditor;
using Substance.Game;

namespace Substance.Editor
{
    [CustomPropertyDrawer(typeof(SubstanceParameter))]
    public class SubstanceParameterDrawer : PropertyDrawer
    {
        /// <summary>
        /// The width of the graph reference field when a graph is referenced.
        /// </summary>
        private const float ASSET_WIDTH = 35f;
        /// <summary>
        /// The space between the graph reference field and the target field when a graph is referenced.
        /// </summary>
        private const float ASSET_BUFFER = 5f;
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Rect labelRect = new Rect(position);

            labelRect.width = (EditorGUIUtility.labelWidth - (EditorGUI.indentLevel * 15f));

            EditorGUI.LabelField(labelRect, label);

            SerializedProperty graphProp = property.FindPropertyRelative("graph");

            if (graphProp.objectReferenceInstanceIDValue != 0)
            {
                //Context command to show/hide more of the graph reference field.
                //Activated by right clicking on the label.
                Event current = Event.current;

                if (labelRect.Contains(current.mousePosition) && current.type == EventType.ContextClick)
                {
                    GenericMenu menu = new GenericMenu();

                    if (!graphProp.isExpanded) menu.AddItem(new GUIContent("Show Asset Field"), false, delegate () { ShowAssetCallback(graphProp); });
                    else menu.AddItem(new GUIContent("Hide Asset Field"), false, delegate () { ShowAssetCallback(graphProp); });

                    menu.ShowAsContext();

                    current.Use();
                }
            }

            labelRect.x += labelRect.width;
            labelRect.width = (position.width - labelRect.width) + (graphProp.isExpanded ? (EditorGUI.indentLevel * 15f) : 0f);

            if (graphProp.objectReferenceInstanceIDValue == 0)
            {
                EditorGUI.PropertyField(labelRect, graphProp, GUIContent.none);
            }
            else
            {
                Rect fieldRect = new Rect(labelRect);

                if (!graphProp.isExpanded)
                {
                    fieldRect.width -= (ASSET_WIDTH + ASSET_BUFFER);
                }
                else
                {
                    fieldRect.width *= 0.5f;
                }

                DrawTargetField(fieldRect, property.FindPropertyRelative("parameter"), graphProp);

                fieldRect.x += fieldRect.width + ASSET_BUFFER;
                fieldRect.x -= (EditorGUI.indentLevel * 15f);

                if (!graphProp.isExpanded)
                {
                    fieldRect.width = ASSET_WIDTH;
                    fieldRect.width += (EditorGUI.indentLevel * 15f);
                }
                else
                {
                    fieldRect.width -= ASSET_BUFFER;
                }

                EditorGUI.PropertyField(fieldRect, graphProp, GUIContent.none);
            }
        }


        protected virtual void DrawTargetField(Rect position, SerializedProperty property, SerializedProperty assetProperty)
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


        protected virtual void ShowAssetCallback(SerializedProperty property)
        {
            property.isExpanded = !property.isExpanded;
        }
    }
}