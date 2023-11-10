﻿using CraftingSystem.Core;
using UnityEditor;
using UnityEngine;

namespace CraftingSystem.Editor
{
    [CustomEditor(typeof(RecipeScriptable))]
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
            EditorGUILayout.Space(25);
            
            for (var y = 0; y < _gridSize.vector2IntValue.y; y++)
            {
                EditorGUILayout.BeginHorizontal();
                for (var x = 0; x < _gridSize.vector2IntValue.x; x++)
                {
                    var index = y * _gridSize.vector2IntValue.x + x;

                    var rect = GUILayoutUtility.GetRect(RecipeCellSize, RecipeCellSize,
                        RecipeCellSize, RecipeCellSize,
                        new []{GUILayout.Width(RecipeCellSize)});

                    rect.x += x * 5;
                    rect.y += y * 5;
                    var item = ((Item)_craftingComponents.GetArrayElementAtIndex(index).objectReferenceValue);
                    if (item == null)
                    {
                        EditorGUI.DrawRect(rect, Color.grey);
                        continue;
                    }
                    var texture = item.icon.texture;
                    EditorGUI.DrawTextureTransparent(rect, texture);
                }
                EditorGUILayout.EndHorizontal(); 
            }
        }

        public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
        {
            //Code from Unity's documentation   
            RecipeScriptable example = (RecipeScriptable)target;

            
            
            if (example == null || example.Recipe == null ||
                example.Recipe.Result || example.Recipe.Result == null)
                return null;
            
            Texture2D tex = new Texture2D (width, height);
            EditorUtility.CopySerialized (example.Recipe.Result.icon.texture, tex);

            return tex;
        }
    }
}