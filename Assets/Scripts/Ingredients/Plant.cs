using UnityEngine;

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

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}