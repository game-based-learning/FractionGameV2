using UnityEngine;

[CreateAssetMenu(fileName = "PlantType", menuName = "Scriptable Objects/PlantType")]
public class PlantType : ScriptableObject
{
    public Sprite petalSprite;
    public Sprite stemSprite;
    public int numberOfPetals = 0;
    public bool sweet = false;
}
