using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

namespace FractionGame.Cauldron
{
    public class Subdivisions : MonoBehaviour
    {
        [SerializeField] private GameObject containerPrefab;
        [SerializeField] private GameObject mainTickPrefab;
        [SerializeField] private int sdCount;
        [SerializeField] private bool fraction;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            CreateContainer();
        }

        void CreateContainer()
        {
            Instantiate(containerPrefab);
            Instantiate(mainTickPrefab);
            //SpriteRenderer C_SR = containerPrefab.GetComponent<SpriteRenderer>();
            //Vector2 C_SIZE = C_SR.sprite.rect.size / C_SR.sprite.pixelsPerUnit;
            //Debug.Log("Sprite size in Unity units (before scaling): " + C_SIZE);
        }
    }
}
