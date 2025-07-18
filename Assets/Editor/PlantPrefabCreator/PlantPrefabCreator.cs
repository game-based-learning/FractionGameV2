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
        private readonly string plantCollider;

        private readonly Sprite petalSprite;
        private readonly float petalSize;
        private readonly string petalCollider;

        private readonly float distance;


        public PlantPrefabCreator(Dictionary<String, VisualElement> inputs)
        {
            plantName = ((TextField)inputs["Name"]).value;
            plantType = (PlantType)((ObjectField)inputs["PlantType"]).value;

            stemSprite = (Sprite)((ObjectField)inputs["StemSprite"]).value;
            stemSize = ((FloatField)inputs["StemSize"]).value;
            plantCollider = ((DropdownField)inputs["PlantCollider"]).value;

            petalSprite = (Sprite)((ObjectField)inputs["PetalSprite"]).value;
            petalSize = ((FloatField)inputs["PetalSize"]).value;
            petalCollider = ((DropdownField)inputs["PetalCollider"]).value;

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
            plantComp.PlantType = plantType;

            //Add the sprite
            SpriteRenderer spriteRenderer = plantObj.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = stemSprite;
            plantObj.transform.localScale = new Vector3(stemSize, stemSize, 1f);

            Rigidbody2D rb = plantObj.AddComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Kinematic;

            //Add collider so that Draggable will work
            AddCollider(plantObj, plantCollider);

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
                petalComp.PlantType = plantType;    

                //Add the sprite
                SpriteRenderer spriteRenderer = petalObj.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = petalSprite;
                //petalSize / stemSize to account for increase in scale already present from the Plant scaling
                petalObj.transform.localScale = new Vector3(petalSize / stemSize, petalSize / stemSize, 1f);

                //Add collider so that Draggable will work
                AddCollider(petalObj, petalCollider);

                // Set the position of the petal
                petalObj.transform.Rotate(0, 0, angle);
                petalObj.transform.Translate(0, distance, 0);
                angle += angleStep;

                //Move the Petal in front of the Plant
                //float petalZPos = plantObj.transform.position.z - 0.1f;
                //petalObj.transform.position 
                    //= new Vector3(petalObj.transform.position.x, petalObj.transform.position.y, petalZPos);
            }
        }

        private void AddCollider(GameObject obj, string colliderType)
        {
            switch (colliderType)
            {
                case "Circle Collider 2D":
                    obj.AddComponent<CircleCollider2D>();
                    break;
                case "Box Collider 2D":
                    obj.AddComponent<BoxCollider2D>();
                    break;
                case "Capsule Collider 2D":
                    obj.AddComponent<CapsuleCollider2D>();
                    break;
                case "No Collider":
                    break;
                default:
                    Debug.LogError("Collider type not implemented");
                    break;
            }
        }
    }
}