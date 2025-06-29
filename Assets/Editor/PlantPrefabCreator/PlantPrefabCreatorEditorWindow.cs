using FractionGame.Ingredients;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace FractionGame.Editor.PlantPrefabCreator
{
    public class PlantPrefabCreatorEditorWindow : CustomEditorWindow
    {
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

            /** Add Fields Here: **/

            AddSpace(root);

            CreateStringField("Plant Object Name:", "Name", root);
            CreateField("PlantType:", "PlantType", typeof(PlantType), root);

            AddSpace(root);

            CreateField("Stem Sprite:", "StemSprite", typeof(Sprite), root);
            CreateFloatField("Stem Size:", "StemSize", root, 1.0f);
            CreateDropdown(root, "Plant Collider Type:", "PlantCollider", "Circle Collider 2D", "Box Collider 2D", "Capsule Collider 2D", "No Collider");

            AddSpace(root);

            CreateField("Petal Sprite:", "PetalSprite", typeof(Sprite), root);
            CreateFloatField("Petal Size:", "PetalSize", root, 1.0f);
            CreateDropdown(root, "Petal Collider Type:", "PetalCollider", "Circle Collider 2D", "Box Collider 2D", "Capsule Collider 2D", "No Collider");

            AddSpace(root);

            CreateFloatField("Distance from the center of the plant to the petal:", "Distance", root, 1.0f);

            AddSpace(root);

            /** **************** **/


            //Submission button
            Button button = new Button { text = "Create Plant Prefab" };
            button.clicked += OnClick;
            root.Add(button);

            //Comment out this line to use default values instead of previous values
            //DOES NOT WORK
            //CustomScriptableSingleton.instance.LoadPlantPrefabEditor(fields);
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

        private void OnDisable()
        {
            //Comment out this line to use default values instead of previous values
            //DOES NOT WORK
            //CustomScriptableSingleton.instance.SavePlantPrefabEditor(fields);
        }
    }
}