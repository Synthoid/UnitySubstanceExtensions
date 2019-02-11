using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using Substance.Game;
using SubstanceExtensions;

namespace SubstanceExtensionsEditor
{
    public class ExportSubstanceTexturesWindow : EditorWindow
    {
        private Substance.Game.Substance substance;
        
        private List<string> graphNames = new List<string>();
        private List<bool> graphExpansions = new List<bool>();
        private List<bool> graphExportExpansions = new List<bool>();
        private List<Vector2> graphScrolls = new List<Vector2>();
        private List<List<string>> textureNames = new List<List<string>>();
        private List<List<Texture>> textures = new List<List<Texture>>();

        private static GUIContent SubstanceLabel = new GUIContent("Substance", "The substance to export textures from.");
        private static GUIContent ButtonLabel = new GUIContent("Export Textures", "Export the textures associated with the target substance as standalone textures.");
        
        [MenuItem("Substance/Extensions/Export Substance Textures")]
        public static void ShowWindow()
        {
            GetWindow<ExportSubstanceTexturesWindow>("Export Sbs Tex", true).Show();
        }


        private void OnGUI()
        {
            EditorGUI.BeginChangeCheck();
            substance = (Substance.Game.Substance)EditorGUILayout.ObjectField(SubstanceLabel, substance, typeof(Substance.Game.Substance), false);
            if(EditorGUI.EndChangeCheck())
            {
                RegenerateTextures();
            }
            
            //Safety check since recompilation or adjustments to the substance seem to remove the reference to the substance.
            if((substance == null && graphNames.Count > 0) || (substance != null && textures.Count == 0))
            {
                RegenerateTextures();
            }

            if(graphNames.Count > 0)
            {
                DrawTextures();
            }

            EditorGUILayout.Space();

            bool cachedGUI = GUI.enabled;
            if (substance == null) GUI.enabled = false;
            if(GUILayout.Button(ButtonLabel))
            {
                ExportTextures();
            }
            if (!GUI.enabled) GUI.enabled = cachedGUI;

            if(substance != null)
            {
                EditorGUILayout.Space();

                for(int i=0; i < textureNames.Count; i++)
                {
                    graphExportExpansions[i] = EditorGUILayout.Foldout(graphExportExpansions[i], new GUIContent(graphNames[i], string.Format("Export individual textures for the [{0}] graph.", graphNames[i])), true);

                    if(graphExportExpansions[i])
                    {
                        for(int j=0; j < textureNames[i].Count; j++)
                        {
                            Rect buttonRect = EditorGUILayout.GetControlRect(GUILayout.Height(EditorGUIUtility.singleLineHeight + (EditorGUIUtility.standardVerticalSpacing * 1f)));
                            buttonRect.Set(buttonRect.x + EditorGUIUtility.singleLineHeight, buttonRect.y, buttonRect.width - EditorGUIUtility.singleLineHeight, buttonRect.height);

                            if (GUI.Button(buttonRect, new GUIContent(textureNames[i][j], string.Format("Export the [{0}] texture from the [{1}] graph.", textureNames[i][j], graphNames[i]))))
                            {
                                ExportTexture(textureNames[i][j], i, j);
                            }
                        }
                    }
                }
            }
        }


        private void DrawTextures()
        {
            for(int i=0; i < graphNames.Count; i++)
            {
                graphExpansions[i] = EditorGUILayout.Foldout(graphExpansions[i], new GUIContent(graphNames[i]), true);

                if(graphExpansions[i])
                {
                    graphScrolls[i] = EditorGUILayout.BeginScrollView(graphScrolls[i], GUILayout.ExpandHeight(false));
                    EditorGUILayout.BeginHorizontal();

                    for(int j=0; j < textures[i].Count; j++)
                    {
                        if (textures[i][j] == null) continue;

                        Rect r = EditorGUILayout.GetControlRect(GUILayout.Height(100f), GUILayout.Width(100f));
                        
                        r.height -= 20f;
                        r.width -= 20f;
                        r.x += 10f;

                        if (GUI.Button(r, AssetPreview.GetAssetPreview(textures[i][j])))
                        {
                            EditorGUIUtility.PingObject(textures[i][j]);
                        }

                        r.y += r.height;
                        r.height = 20f;

                        EditorGUI.LabelField(r, new GUIContent(textureNames[i][j], textureNames[i][j]));
                    }

                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.EndScrollView();
                }
            }
        }


        private void RegenerateTextures()
        {
            graphNames.Clear();
            graphExpansions.Clear();
            graphExportExpansions.Clear();
            graphScrolls.Clear();
            textureNames.Clear();
            textures.Clear();

            if (substance != null)
            {
                for (int i = 0; i < substance.graphs.Count; i++)
                {
                    graphNames.Add(substance.graphs[i].name);
                    graphExpansions.Add(true);
                    graphExportExpansions.Add(true);
                    graphScrolls.Add(Vector2.zero);
                    textureNames.Add(new List<string>());
                    textures.Add(new List<Texture>());
                    
                    List<Texture2D> tempTextures = substance.graphs[i].GetGeneratedTextures();

                    for(int j=0; j < tempTextures.Count; j++)
                    {
                        textureNames[i].Add(tempTextures[j].name.LastIndexOf(' ') < 0 ? tempTextures[j].name : tempTextures[j].name.Substring(tempTextures[j].name.LastIndexOf(' ')+1));
                        textures[i].Add(tempTextures[j]);
                    }
                }
            }
        }


        private void ExportTextures()
        {
            string folderPath = EditorUtility.OpenFolderPanel("Export Textures", Application.dataPath, "");

            if(!string.IsNullOrEmpty(folderPath))
            {
                List<string> paths = new List<string>();

                for(int i=0; i < textures.Count; i++)
                {
                    for(int j=0; j < textures[i].Count; j++)
                    {
                        if (textures[i][j] == null) continue;
                        
                        byte[] bytes = new byte[0];

                        if(((Texture2D)textures[i][j]).IsCompressed())
                        {
                            Texture2D newTex = new Texture2D(textures[i][j].width, textures[i][j].height, TextureFormat.ARGB32, false) { name = textures[i][j].name };
                            Color32[] colors = ((Texture2D)textures[i][j]).GetPixels32();
                            newTex.SetPixels32(colors);
                            newTex.Apply();

                            bytes = ImageConversion.EncodeToPNG(newTex);
                        }
                        else
                        {
                            bytes = ImageConversion.EncodeToPNG((Texture2D)textures[i][j]);
                        }

                        string path = folderPath + "/" + textures[i][j].name + ".png";
                        File.WriteAllBytes(path, bytes);

                        if(path.StartsWith(Application.dataPath))
                        {
                            paths.Add("Assets" + path.Replace(Application.dataPath, ""));
                        }
                    }
                }

                if(paths.Count > 0)
                {
                    Texture[] objects = new Texture[paths.Count];

                    for(int i=0; i < paths.Count; i++)
                    {
                        AssetDatabase.ImportAsset(paths[i], ImportAssetOptions.ForceUpdate);

                        objects[i] = AssetDatabase.LoadAssetAtPath<Texture>(paths[i]);
                    }

                    Selection.objects = objects;
                }
            }
        }


        private void ExportTexture(string texName, int graphIndex, int texIndex)
        {
            string path = EditorUtility.SaveFilePanel("Export Textures", Application.dataPath, graphNames[graphIndex] + " - " + texName, "png");

            if (!string.IsNullOrEmpty(path))
            {
                if (textures[graphIndex][texIndex] == null) return;

                byte[] bytes = new byte[0];

                if (((Texture2D)textures[graphIndex][texIndex]).IsCompressed())
                {
                    Texture2D newTex = new Texture2D(textures[graphIndex][texIndex].width, textures[graphIndex][texIndex].height, TextureFormat.ARGB32, false);
                    Color32[] colors = ((Texture2D)textures[graphIndex][texIndex]).GetPixels32();
                    newTex.SetPixels32(colors);
                    newTex.Apply();

                    bytes = ImageConversion.EncodeToPNG(newTex);
                }
                else
                {
                    bytes = ImageConversion.EncodeToPNG((Texture2D)textures[graphIndex][texIndex]);
                }
                
                File.WriteAllBytes(path, bytes);

                CustomEditorUtility.SelectOrReveal(path, false, true);
            }
        }
    }
}