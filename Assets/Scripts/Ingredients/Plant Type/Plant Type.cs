using UnityEngine;

namespace FractionGame.Ingredients
{
    [CreateAssetMenu(fileName = "PlantType", menuName = "Scriptable Objects/PlantType")]
    public class PlantType : ScriptableObject
    {
        public Sprite petalSprite;
        public float petalSize = 1f;
        public Sprite stemSprite;
        public float stemSize = 1f;
        [Tooltip("name of petal in recipe book")]
        public string petalName;
        [Tooltip("name of plant in recipe book")]
        public string plantName; 
        public int numberOfPetals = 0;
        [Tooltip("distance from the center of the plant to the petal")]
        public float distance = 1f;
        public bool sweet = false;
    }
}
