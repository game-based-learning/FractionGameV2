using FractionGame.Cauldron;
using FractionGame.Utility;
using UnityEngine;

public class potion : MonoBehaviour
{
    private string PotionName;
    private Fraction PotionValue;

    // Update is called once per frame
    public void Initialize(Fraction value, string name)
    {
        PotionName = name;
        PotionValue = value;
        Debug.Log("Potion Created: " + PotionName + " with value " + PotionValue);
    }
}
