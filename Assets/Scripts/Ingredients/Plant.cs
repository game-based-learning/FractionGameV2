using UnityEngine;
using Utility;

namespace Ingredients
{
    public class Plant : MonoBehaviour
    {

        [SerializeField] private PlantType plantType;

        public PlantType PlantType
        {
            get { return plantType; }
            // set { plantType = value; }
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            Fraction testfraction = new Fraction();
            Debug.Log(testfraction.ToString());
            Debug.Log(testfraction.Value);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}