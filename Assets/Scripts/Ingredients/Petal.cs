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
