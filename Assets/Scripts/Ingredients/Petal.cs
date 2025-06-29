using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using FractionGame.Utility;

namespace FractionGame.Ingredients
{
    public class Petal : Draggable, IIngredient
    {

        private Fraction value;
        private string nameStr; // can't be 'name' because it conflicts with Unity's GameObject name property
        [SerializeField] private PlantType plantType;

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

        void Start()
        {
            nameStr = plantType.petalName;
            value = new Fraction(1, plantType.numberOfPetals);
        }

        /// <summary>
        /// Initialize should be called by Plant when creating Petal
        /// </summary>
        /// <param name="plantType">PlantType of the parent Plant</param>
        public void Initialize(PlantType plantType)
        {
            this.plantType = plantType; 
            nameStr = plantType.petalName; 
            value = new Fraction(1, plantType.numberOfPetals);

            /*// Create a GameObject for the sprite and set it as a child of this GameObject
            GameObject spriteObj = new GameObject("Sprite");
            spriteObj.transform.SetParent(transform, false);
            SpriteRenderer spriteRenderer = spriteObj.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = plantType.petalSprite;

            spriteObj.transform.localScale = new Vector3(plantType.petalSize, plantType.petalSize, 1f);

            //Move the Petal in front of the Plant
            float petalZPos = plantZPos - 0.1f;
            transform.position = new Vector3(transform.position.x, transform.position.y, petalZPos);*/


            // Create a GameObject for the sprite and set it as a child of this GameObject
            //transform.SetParent(transform, false);
            //SpriteRenderer spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
            //spriteRenderer.sprite = plantType.petalSprite;

            //plantType.petalSize / plantType.stemSize to account for increase in scale already present from the Plant scaling
            //gameObject.transform.localScale = new Vector3(plantType.petalSize / plantType.stemSize, plantType.petalSize / plantType.stemSize, 1f);

            //Add collider so that Draggable will work
            //gameObject.AddComponent<CircleCollider2D>();

            //Move the Petal in front of the Plant
            //float petalZPos = plantZPos - 0.1f;
            //transform.position = new Vector3(transform.position.x, transform.position.y, petalZPos);


        }

        public override bool Attach(GameObject previousParent)
        {
            if (!base.Attach(previousParent))
            {
                return false;
            }

            if (!previousParent)
            {
                //Petal has already been removed from the Plant
                return true;
            }

            Plant plant = previousParent.GetComponent<Plant>();
            if (!plant)
            {
                Debug.LogError("Petal parent was not a Plant");
                return true;
            }

            plant.HandlePetalDetaching();

            return true;
        }
    }
}
