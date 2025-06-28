using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using FractionGame.Utility;

namespace FractionGame.Ingredients
{
    public class Petal : MonoBehaviour, IIngredient
    {

        private Fraction value;
        private string nameStr; // can't be 'name' because it conflicts with Unity's GameObject name property
        private PlantType plantType;

        public Fraction Value 
        { 
            get 
            {   
                if (value is null) 
                {
                    Debug.LogError("Value is not initialized");
                }
                return value; 
            } 
        }


        public IngredientType Type { get { return IngredientType.PETAL; } }

        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(nameStr))
                {
                    Debug.LogError("Name is not initialized");
                }
                return nameStr;
            }
        }

        public PlantType PlantType
        {
            get { return plantType; }
            set { plantType = value; }
        }

        /// <summary>
        /// Initialize should be called by Plant when creating Petal
        /// </summary>
        /// <param name="plantType">PlantType of the parent Plant</param>
        /// <param name="plantZPos">Position on the Z axis of the parent Plant (used to place the Petal in front of the parent Plant)</param>
        public void Initialize(PlantType plantType, float plantZPos)
        {
            this.plantType = plantType; 
            name = plantType.petalName; 
            value = new Fraction(1, plantType.numberOfPetals);

            // Create a GameObject for the sprite and set it as a child of this GameObject
            GameObject spriteObj = new GameObject("Sprite");
            spriteObj.transform.SetParent(transform, false);
            SpriteRenderer spriteRenderer = spriteObj.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = plantType.petalSprite;

            spriteObj.transform.localScale = new Vector3(plantType.petalSize, plantType.petalSize, 1f);

            //Move the Petal in front of the Plant
            float petalZPos = plantZPos - 0.1f;
            transform.position = new Vector3(transform.position.x, transform.position.y, petalZPos);
        }
    }
}
