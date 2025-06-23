using UnityEngine;

namespace Ingredients
{
    [CreateAssetMenu(fileName = "PlantType", menuName = "Scriptable Objects/PlantType")]
    public class PlantType : ScriptableObject
    {
        public Sprite petalSprite;
        public Sprite stemSprite;
        public string petalName; // name of petal in recipe book
        public string plantName; // name of plant in recipe book
        public int numberOfPetals = 0;
        public bool sweet = false;
    }
}
