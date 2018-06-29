using UnityEngine;
using UnityEditor;

namespace Substance.Editor
{
    /// <summary>
    /// Base class for drawing asset dependent inspector fields.
    /// </summary>
	public abstract class AssetReferenceDrawer : PropertyDrawer
	{
        /// <summary>
        /// The width of the graph reference field when a graph is referenced.
        /// </summary>
        private const float ASSET_WIDTH = 35f;
        /// <summary>
        /// The space between the graph reference field and the target field when a graph is referenced.
        /// </summary>
        private const float ASSET_BUFFER = 5f;

        /// <summary>
        /// The name of the field containing a reference to an asset.
        /// </summary>
        protected virtual string AssetField
        {
            get { return "graph"; }
        }

        /// <summary>
        /// The name of the field to draw once an asset is referenced.
        /// </summary>
        protected virtual string TargetField
        {
            get { return "parameter"; }
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Rect labelRect = new Rect(position);

            labelRect.width = (EditorGUIUtility.labelWidth - (EditorGUI.indentLevel * 15f));

            EditorGUI.LabelField(labelRect, label);

            SerializedProperty assetProp = property.FindPropertyRelative(AssetField);

            if (assetProp.objectReferenceInstanceIDValue != 0)
            {
                //Context command to show/hide more of the graph reference field.
                //Activated by right clicking on the label.
                Event current = Event.current;

                if (labelRect.Contains(current.mousePosition) && current.type == EventType.ContextClick)
                {
                    GenericMenu menu = new GenericMenu();

                    if (!assetProp.isExpanded) menu.AddItem(new GUIContent("Show Asset Field"), false, delegate () { ShowAssetCallback(assetProp); });
                    else menu.AddItem(new GUIContent("Hide Asset Field"), false, delegate () { ShowAssetCallback(assetProp); });

                    menu.ShowAsContext();

                    current.Use();
                }
            }

            labelRect.x += labelRect.width;
            labelRect.width = (position.width - labelRect.width) + (assetProp.isExpanded ? (EditorGUI.indentLevel * 15f) : 0f);

            if (assetProp.objectReferenceInstanceIDValue == 0)
            {
                EditorGUI.PropertyField(labelRect, assetProp, GUIContent.none);
            }
            else
            {
                Rect fieldRect = new Rect(labelRect);

                if (!assetProp.isExpanded)
                {
                    fieldRect.width -= (ASSET_WIDTH + ASSET_BUFFER);
                }
                else
                {
                    fieldRect.width *= 0.5f;
                }

                DrawTargetField(fieldRect, property.FindPropertyRelative(TargetField), assetProp);

                fieldRect.x += fieldRect.width + ASSET_BUFFER;
                fieldRect.x -= (EditorGUI.indentLevel * 15f);

                if (!assetProp.isExpanded)
                {
                    fieldRect.width = ASSET_WIDTH;
                    fieldRect.width += (EditorGUI.indentLevel * 15f);
                }
                else
                {
                    fieldRect.width -= ASSET_BUFFER;
                }

                EditorGUI.PropertyField(fieldRect, assetProp, GUIContent.none);
            }
        }

        /// <summary>
        /// Draw the target field. This is only called if an asset is being referenced.
        /// </summary>
        /// <param name="position">The <see cref="Rect"/> to draw the field at.</param>
        /// <param name="property">The property field to draw.</param>
        /// <param name="assetProperty">The serialized property containing a reference to an asset.</param>
        protected virtual void DrawTargetField(Rect position, SerializedProperty property, SerializedProperty assetProperty)
        {
            EditorGUI.PropertyField(position, property, true);
        }

        /// <summary>
        /// Callback invoked when right clicking on the field's label.
        /// </summary>
        protected virtual void ShowAssetCallback(SerializedProperty property)
        {
            property.isExpanded = !property.isExpanded;
        }
    }
}