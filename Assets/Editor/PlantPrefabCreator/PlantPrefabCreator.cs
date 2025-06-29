using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using FractionGame.Ingredients;

namespace FractionGame.Editor.PlantPrefabCreator
{
    public class PlantPrefabCreator
    {
        private readonly string plantName;
        private readonly PlantType plantType;

        private readonly Sprite stemSprite;
        private readonly float stemSize;

        private readonly Sprite petalSprite;
        private readonly float petalSize;

        private readonly float distance;


        public PlantPrefabCreator(Dictionary<String, VisualElement> inputs)
        {
            plantName = ((TextField)inputs["Name"]).value;
            plantType = (PlantType)((ObjectField)inputs["PlantType"]).value;

            stemSprite = (Sprite)((ObjectField)inputs["StemSprite"]).value;
            stemSize = ((FloatField)inputs["StemSize"]).value;

            petalSprite = (Sprite)((ObjectField)inputs["PetalSprite"]).value;
            petalSize = ((FloatField)inputs["PetalSize"]).value;

            distance = ((FloatField)inputs["Distance"]).value;
        }

        public GameObject Create()
        {
            GameObject plantObj = CreatePlant();
            CreatePetals(plantObj);

            return plantObj;
        }

        private GameObject CreatePlant()
        {
            //Create an empty game object with the given name
            GameObject plantObj = new GameObject(plantName);

            //Add the Plant script component
            Plant plantComp = plantObj.AddComponent<Plant>();
            //plantComp.Initialize(plantType);
            plantComp.PlantType = plantType;

            //Add the sprite
            SpriteRenderer spriteRenderer = plantObj.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = stemSprite;
            plantObj.transform.localScale = new Vector3(stemSize, stemSize, 1f);

            //Add collider so that Draggable will work
            plantObj.AddComponent<CircleCollider2D>();

            return plantObj;
        }

        private void CreatePetals(GameObject plantObj)
        {
            float angleStep = 360f / plantType.numberOfPetals;
            float angle = 0f;

            for (int i = 0; i < plantType.numberOfPetals; i++)
            {
                //Create a Petal (with the plantObj as its parent)
                GameObject petalObj = new GameObject("Petal" + i);
                petalObj.transform.SetParent(plantObj.transform, false);
                Petal petalComp = petalObj.AddComponent<Petal>();
                //petalComp.Initialize(plantType);
                petalComp.PlantType = plantType;    

                //Add the sprite
                SpriteRenderer spriteRenderer = petalObj.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = petalSprite;
                //petalSize / stemSize to account for increase in scale already present from the Plant scaling
                petalObj.transform.localScale = new Vector3(petalSize / stemSize, petalSize / stemSize, 1f);

                //Add collider so that Draggable will work
                petalObj.AddComponent<CircleCollider2D>();

                // Set the position of the petal
                petalObj.transform.Rotate(0, 0, angle);
                petalObj.transform.Translate(0, distance, 0);
                angle += angleStep;

                //Move the Petal in front of the Plant
                float petalZPos = plantObj.transform.position.z - 0.1f;
                petalObj.transform.position 
                    = new Vector3(petalObj.transform.position.x, petalObj.transform.position.y, petalZPos);
            }
        }
    }
}