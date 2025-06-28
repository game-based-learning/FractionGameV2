using FractionGame.Ingredients;
using UnityEngine;

public class PlantTest : MonoBehaviour
{
    [SerializeField] private Plant plant;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public void OnClick()
    {
        Debug.Log("Detaching petal...");
        Debug.Log(plant.DetachPetal());
    }
}
