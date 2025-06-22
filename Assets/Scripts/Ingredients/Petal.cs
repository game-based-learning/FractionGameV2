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
        /// <param name="plantType"></param>

        public void Initialize(PlantType plantType)
        {
            
            this.plantType = plantType; 
            name = plantType.petalName; 
            value = new Fraction(1, plantType.numberOfPetals);

            // Create a GameObject for the sprite and set it as a child of this GameObject
            GameObject spriteObj = new GameObject("Sprite");
            spriteObj.transform.SetParent(transform);
            SpriteRenderer spriteRenderer = spriteObj.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = plantType.petalSprite;

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
