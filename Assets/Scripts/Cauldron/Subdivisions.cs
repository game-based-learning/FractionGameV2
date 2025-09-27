using System;
using TMPro;
using UnityEditor.Tilemaps;
using UnityEngine;

namespace FractionGame.Cauldron
{
    public class Subdivisions : MonoBehaviour
    {
        [SerializeField] private GameObject containerPrefab;
        [SerializeField] private GameObject tickPrefab;

        [SerializeField] private int subdivisionCount;
        [SerializeField] private float containerSize;
        [SerializeField] private bool showFractions;
        [SerializeField] private bool simplifyFractions;

        void Start()
        {
            GameObject containerObject = Instantiate(containerPrefab, this.transform);
            Bounds containerBounds = containerObject.GetComponent<SpriteRenderer>().bounds;

            // Get size for each subdivision
            //float subD = containerSize / subdivisionCount;
            float subD = containerBounds.extents.y * 2 / subdivisionCount;

            // if/else for displaying values as fractions or decimals
            if (showFractions)
            {
                // For each place a subdivion label will be (includes 0)
                for (int i = 0; i < subdivisionCount + 1; i++)
                {
                    // Don't want to show 0 as 0/? or 1 as 1/1 so don't set fraction
                    string fString = "0";
                    if (i == subdivisionCount) { fString = "1"; }
                    else if (i != 0)
                    {
                        if (simplifyFractions)
                        {
                            // If simplifying then get GCD and divide numerator and denominator by it
                            int gcd = GCD(i, subdivisionCount);
                            fString = $"{i / gcd}/{subdivisionCount / gcd}";
                        }
                        else
                        {
                            // If not simplifying simply set to current subdivision over the given count
                            fString = $"{i}/{subdivisionCount}";
                        }
                    }

                    Debug.Log($"{fString} at {subD * i:F2}");
                    GameObject tickObject = Instantiate(tickPrefab, new Vector2(0, subD * i - containerBounds.extents.y), Quaternion.identity, this.transform);
                    tickObject.GetComponentInChildren<TMP_Text>().text = fString;
                }
            } 
            else
            {
                // For each place a subdivion label will be (includes 0)
                for (int i = 0; i < subdivisionCount + 1; i++)
                {
                    // Don't want to show 0 as 0/? or 1 as 1/1 so don't set fraction
                    string dString = "0";
                    if (i == subdivisionCount) { dString = "1"; }
                    else if (i != 0)
                    {
                        // Display label with 2 decimal places
                        dString = $"{i / (float)subdivisionCount:F2}";
                    }

                    Debug.Log($"{dString} at {subD * i:F2}");
                }
            }
        }

        public void ButtonTest()
        {
            Debug.Log("test");
        }

        // Found this online
        int GCD(int a, int b) => b == 0 ? a : GCD(b, a % b);

    }
}
