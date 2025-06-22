using UnityEngine;

namespace Ingredients
{
    [CreateAssetMenu(fileName = "PlantType", menuName = "Scriptable Objects/PlantType")]
    public class PlantType : ScriptableObject
    {
        public Sprite petalSprite;
        public Sprite stemSprite;
        public string petalName;
        public string plantName;
        public int numberOfPetals = 0;
        public bool sweet = false;
    }
}
