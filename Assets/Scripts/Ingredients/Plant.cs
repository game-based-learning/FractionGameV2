using NUnit.Framework;
using UnityEngine;
using FractionGame.Utility;
using System.Collections.Generic;

namespace FractionGame.Ingredients
{
    public class Plant : Draggable
    {
        [SerializeField] private PlantType plantType;
        private int numPetalsRemaining;

        public PlantType PlantType
        {
            get { return plantType; }
            set { plantType = value; }
        }

        public void HandlePetalDetaching()
        {
            numPetalsRemaining--;
            Debug.Log("Number of Petals remaining for " + name + ": " + numPetalsRemaining);

            if (numPetalsRemaining == 0)
            {
                // If no petals left, destroy the plant GameObject
                Destroy(gameObject);
            }
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            numPetalsRemaining = plantType.numberOfPetals;
        }
    }
}