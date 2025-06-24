using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using Utility;

namespace Ingredients
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
            spriteObj.transform.SetParent(transform);
            SpriteRenderer spriteRenderer = spriteObj.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = plantType.petalSprite;

            spriteObj.transform.localScale = new Vector3(plantType.petalSize, plantType.petalSize, 1f);

            //Move the Petal in front of the Plant
            //(This has to happen after creating the Sprite child object.
            //  Otherwise, Unity will change the local position of the sprite
            //  so that the sprite's world position stays at zero.
            //  However, if you do this here, then Unity will move both the Petal and Sprite objects.)
            float petalZPos = plantZPos - 0.1f;
            transform.position = new Vector3(transform.position.x, transform.position.y, petalZPos);
        }



        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
