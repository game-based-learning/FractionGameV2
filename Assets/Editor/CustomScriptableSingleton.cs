using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace FractionGame.Editor
{
    [FilePath("Assets/Editor/EditorData.txt", FilePathAttribute.Location.ProjectFolder)]
    public class CustomScriptableSingleton : ScriptableSingleton<CustomScriptableSingleton>
    {
        //For now, this class will be hardcoded for each specific use case,
        //but if we end up using this more, something more general should be implemented
        
        [SerializeField] Dictionary<string, VisualElement> plantPrefabEditorFields = new Dictionary<string, VisualElement>();

        public void SavePlantPrefabEditor(Dictionary<string, VisualElement> data)
        {
            foreach(KeyValuePair<string, VisualElement> pair in data)
            {
                plantPrefabEditorFields[pair.Key] = pair.Value;
            }

            Save(true);
            Debug.Log("ScriptableSingleton saved to " + GetFilePath());
        }

        public void LoadPlantPrefabEditor(Dictionary<string, VisualElement> fields)
        {
            foreach (KeyValuePair<string, VisualElement> pair in plantPrefabEditorFields)
            {
                fields[pair.Key] = pair.Value;
            }
        }
    }
}