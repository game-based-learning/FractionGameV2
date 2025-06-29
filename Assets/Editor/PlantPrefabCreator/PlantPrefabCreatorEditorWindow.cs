using FractionGame.Ingredients;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace FractionGame.Editor.PlantPrefabCreator
{
    public class PlantPrefabCreatorEditorWindow : EditorWindow
    {
        Dictionary<String, VisualElement> fields = new Dictionary<String, VisualElement>();

        //These are the field options that have been implemented below
        //They will be used to determine what kind of VisualElement to create
        //(Unity does support others if we need to add more)
        //Field category list offered by Unity: https://docs.unity3d.com/6000.1/Documentation/Manual/UIE-ElementRef.html
        enum FieldCategory
        {
            String,
            Float,
            Integer,
            Boolean,
            UnityObject
        }
        
        [MenuItem("Window/Custom Editor Windows/Plant Prefab Creator")]
        public static void ShowWindow()
        {
            PlantPrefabCreatorEditorWindow wnd = GetWindow<PlantPrefabCreatorEditorWindow>();
            wnd.titleContent = new GUIContent("Plant Prefab Creator");
        }

        public void CreateGUI()
        {
            // Each editor window contains a root VisualElement object
            VisualElement root = rootVisualElement;

            // VisualElements objects can contain other VisualElement following a tree hierarchy.
            //VisualElement label = new Label("Hello World! From C#");
            //root.Add(label);


            /** Add Fields Here: **/

            AddSpace(root);

            CreateField("Plant Object Name:", "Name", typeof(string), FieldCategory.String, root);
            CreateField("PlantType:", "PlantType", typeof(PlantType), FieldCategory.UnityObject, root);

            AddSpace(root);

            CreateField("Stem Sprite:", "StemSprite", typeof(Sprite), FieldCategory.UnityObject, root);
            CreateField("Stem Size:", "StemSize", typeof(float), FieldCategory.Float, root, "1.0");

            AddSpace(root);

            CreateField("Petal Sprite:", "PetalSprite", typeof(Sprite), FieldCategory.UnityObject, root);
            CreateField("Petal Size:", "PetalSize", typeof(float), FieldCategory.Float, root, "1.0");

            AddSpace(root);

            CreateField("Distance from the center of the plant to the petal:", "Distance", typeof(float), FieldCategory.Float, root, "1.0");

            AddSpace(root);

            /** **************** **/


            //Submission button
            Button button = new Button { text = "Create Plant Prefab" };
            button.clicked += OnClick;
            root.Add(button);
        }

        private void AddSpace(VisualElement root)
        {
            //ToolbarSpacer space = new ToolbarSpacer();
            Label space = new Label("***");
            root.Add(space);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="label">Label that will be displayed next to the field in the editor window.</param>
        /// <param name="key">Key that PlantPrefabCreator will use to access this field from the Dictionary</param>
        /// <param name="type">Type the user can input in the editor</param>
        /// <param name="fieldCategory">Used to determine what kind of VisualElement to create</param>
        /// <param name="root">Root Visual Element</param>
        /// <param name="defaultValue">Optional Default Value (written as a string, not implemented for Unity Object)</param>
        private void CreateField(string label, string key, Type type, FieldCategory fieldCategory, VisualElement root, string defaultValue = "")
        {
            VisualElement field;

            switch (fieldCategory)
            {
                case FieldCategory.String:
                    field = new TextField();
                    ((TextField)field).label = label;
                    ((TextField)field).textEdition.placeholder = defaultValue;
                    break;
                case FieldCategory.Float:
                    field = new FloatField(label);
                    if (float.TryParse(defaultValue, out float floatVal))
                    {
                        ((FloatField)field).value = floatVal;
                    }
                    break;
                case FieldCategory.Integer:
                    field = new IntegerField(label);
                    if (int.TryParse(defaultValue, out int intVal))
                    {
                        ((IntegerField)field).value = intVal;
                    }
                    break;
                case FieldCategory.Boolean:
                    field = new Toggle(label);
                    if (bool.TryParse(defaultValue, out bool boolVal))
                    {
                        ((Toggle)field).value = boolVal;
                    }
                    break;
                case FieldCategory.UnityObject:
                    field = new ObjectField(label);
                    ((ObjectField)field).objectType = type;
                    break;
                default:
                    Debug.LogError("Field Category not implemented");
                    return;
            }

            root.Add(field);
            fields.Add(key, field);
        }

        void OnClick()
        {
            PlantPrefabCreator creator = new PlantPrefabCreator(fields);
            GameObject plantPrefab = creator.Create();

            if(plantPrefab)
            {
                StorePrefab(plantPrefab);
            }
        }

        private void StorePrefab(GameObject prefab)
        {
            string localPath = "Assets/Prefabs/PlantPrefabs/" + prefab.name + ".prefab";

            if(AssetDatabase.AssetPathExists(localPath))
            {
                Debug.LogError(prefab.name + ".prefab already exists.");
                return;
            }

            bool saveSuccess;
            PrefabUtility.SaveAsPrefabAssetAndConnect(prefab, localPath, InteractionMode.UserAction, out saveSuccess);
            if (saveSuccess)
            {
                Debug.Log(prefab.name + ".prefab was saved successfully");
            }
            else
            {
                Debug.LogError(prefab.name + ".prefab NOT SAVED");
            }
        }
    }
}