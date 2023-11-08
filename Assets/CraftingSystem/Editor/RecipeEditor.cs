using CraftingSystem.Core;
using UnityEditor;
using UnityEngine;

namespace CraftingSystem.Editor
{
    [CustomEditor(typeof(Recipe))]
    public class RecipeEditor : UnityEditor.Editor
    {
        private SerializedProperty _craftingComponents;
        private SerializedProperty _result;
        private SerializedProperty _gridSize;

        private const float RecipeCellSize = 100f;

        private void OnEnable()
        {
            _craftingComponents = serializedObject.FindProperty("ingredients");
            _result = serializedObject.FindProperty("result");
            _gridSize = serializedObject.FindProperty("sizeOfGrid");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.UpdateIfRequiredOrScript();
            EditorGUILayout.PropertyField(_result, new GUIContent("Result"), new GUILayoutOption[] {GUILayout.Height(50)});

            EditorGUILayout.Space(25);
            EditorGUILayout.PropertyField(_gridSize, new GUIContent("Grid Size"));
            EditorGUILayout.Space(25);
            _craftingComponents.arraySize = _gridSize.vector2IntValue.x * _gridSize.vector2IntValue.y;
            
            EditorGUILayout.LabelField("Recipe", EditorStyles.boldLabel);
            for (var y = 0; y < _gridSize.vector2IntValue.y; y++)
            {
                EditorGUILayout.BeginHorizontal();
                for (var x = 0; x < _gridSize.vector2IntValue.x; x++)
                {
                    var index = y * _gridSize.vector2IntValue.x + x;
                    EditorGUILayout.PropertyField(_craftingComponents.GetArrayElementAtIndex(index), GUIContent.none,
                        new GUILayoutOption[] {GUILayout.Height(RecipeCellSize), GUILayout.Width(RecipeCellSize)});
                }
                EditorGUILayout.EndHorizontal();
            }
            
            serializedObject.ApplyModifiedProperties();
        }

        public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
        {
            //Code from Unity's documentation   
            Recipe example = (Recipe)target;

            if (example == null || example.result == null || example.result.icon == null)
                return null;
            
            Texture2D tex = new Texture2D (width, height);
            EditorUtility.CopySerialized (example.result.icon.texture, tex);

            return tex;
        }
    }
}