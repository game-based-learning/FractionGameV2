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
            VisualElement label = new Label("Hello World! From C#");
            root.Add(label);

            //Add Fields Here:
            CreateField("Select PlantType:", "PlantType", typeof(PlantType), FieldCategory.UnityObject, root);
            CreateField("Select Sprite:", "Sprite", typeof(Sprite), FieldCategory.UnityObject, root);
            CreateField("Select String:", "String", typeof(string), FieldCategory.String, root);
            CreateField("Select Integer:", "Integer", typeof(int), FieldCategory.Integer, root);
            CreateField("Select Float:", "Float", typeof(float), FieldCategory.Float, root);
            CreateField("Select Boolean:", "Boolean", typeof(bool), FieldCategory.Boolean, root);

            //Submission button
            Button button = new Button { text = "Create Plant Prefab" };
            button.clicked += OnClick;
            root.Add(button);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="label">Label that will be displayed next to the field in the editor window.</param>
        /// <param name="key">Key that PlantPrefabCreator will use to access this field from the Dictionary</param>
        /// <param name="type">Type the user can input in the editor</param>
        /// <param name="fieldCategory">Used to determine what kind of VisualElement to create</param>
        /// <param name="root">Root Visual Element</param>
        private void CreateField(string label, string key, Type type, FieldCategory fieldCategory, VisualElement root)
        {
            /*ObjectField objectField = new ObjectField(label);
            objectField.objectType = type;
            root.Add(objectField);
            fields.Add(key, objectField);*/

            VisualElement field;

            switch (fieldCategory)
            {
                case FieldCategory.String:
                    field = new TextField();
                    ((TextField)field).label = label;
                    break;
                case FieldCategory.Float:
                    field = new FloatField(label);
                    break;
                case FieldCategory.Integer:
                    field = new IntegerField(label);
                    break;
                case FieldCategory.Boolean:
                    field = new Toggle(label);
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
        }
    }
}