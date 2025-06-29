using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace FractionGame.Editor.PlantPrefabCreator
{
    public class PlantPrefabCreator
    {
        public PlantPrefabCreator(Dictionary<String, VisualElement> inputs)
        {
            Debug.Log(((TextField)inputs["String"]).value);
        }
    }
}