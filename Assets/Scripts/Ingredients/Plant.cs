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
            spriteObj.transform.SetParent(transform, false);
            SpriteRenderer spriteRenderer = spriteObj.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = plantType.stemSprite;
            spriteObj.transform.localScale = new Vector3(plantType.stemSize, plantType.stemSize, 1f);

        }

        private void CreatePetals()
        {
            float angleStep = 360f / plantType.numberOfPetals;
            float angle = 0f;

            for (int i = 0; i < plantType.numberOfPetals; i++)
            {
                // Creating the petal
                GameObject petalObj = new GameObject("Petal" + i);
                petalObj.transform.SetParent(transform, false);
                Petal petal = petalObj.AddComponent<Petal>();
                petal.Initialize(plantType, transform.position.z);
                petals.Add(petal);

                // Set the position of the petal
                petalObj.transform.Rotate(0,0, angle);
                petalObj.transform.Translate(0, plantType.distance, 0);
                angle += angleStep;
            }
        }

        /// <summary>
        /// Detaches the last petal from the plant and destroys the plant when there are no petals remaining.
        /// </summary>
        /// <returns> Returns the detached petal. </returns>
        public Petal DetachPetal()
        {
            if (petals.Count == 0)
            {
                Debug.LogError("No petals to detach");
                return null;
            }

            // Get the last petal from the list
            int index = petals.Count - 1;
            Petal lastPetal = petals[index];

            // Remove petal from the list
            petals.RemoveAt(index);

            // Unparent the petal from the plant
            lastPetal.transform.SetParent(null);

            if (petals.Count == 0)
            {
                // If no petals left, destroy the plant GameObject
                Destroy(gameObject);    
            }

            // Return the detached petal   
            return lastPetal;
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            CreatePlantSprite();
            CreatePetals();
        }

    }
}