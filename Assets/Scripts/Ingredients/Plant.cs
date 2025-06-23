using NUnit.Framework;
using UnityEngine;
using Utility;
using System.Collections.Generic;

namespace Ingredients
{
    public class Plant : MonoBehaviour
    {

        [SerializeField] private PlantType plantType;
        List<Petal> petals = new List<Petal>();

        public PlantType PlantType
        {
            get { return plantType; }
            // set { plantType = value; }
        }

        private void CreatePlantSprite()
        {
            // Create a GameObject for the sprite and set it as a child of this GameObject
            GameObject spriteObj = new GameObject("Sprite");
            spriteObj.transform.SetParent(transform);
            SpriteRenderer spriteRenderer = spriteObj.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = plantType.stemSprite;
        }

        private void CreatePetals()
        {
            for (int i = 0; i < plantType.numberOfPetals; i++)
            {
                GameObject petalObj = new GameObject("Petal" + i);
                petalObj.transform.SetParent(transform);
                Petal petal = petalObj.AddComponent<Petal>();
                petal.Initialize(plantType);
                petals.Add(petal);
            }
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            CreatePlantSprite();
            CreatePetals();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}