using System;
using UnityEngine;

namespace FractionGame.Cauldron
{
    public class Subdivisions : MonoBehaviour
    {
        [SerializeField] private int subdivisionCount;
        [SerializeField] private double containerSize;
        [SerializeField] private bool showFractions;
        [SerializeField] private bool simplifyFractions;

        void Start()
        {
            // Get size for each subdivision
            double subD = containerSize / subdivisionCount;
            
            // if/else for displaying values as fractions or decimals
            if (showFractions)
            {
                // For each place a subdivion label will be (includes 0)
                for (int i = 0; i < subdivisionCount + 1; i++)
                {
                    // Don't want to show 0 as ?/0 or 1 as 1/1 so don't set fraction
                    string fString = Convert.ToString(i);
                    if (i != 0 && i != subdivisionCount)
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
                }
            } 
            else
            {
                // For each place a subdivion label will be (includes 0)
                for (int i = 0; i < subdivisionCount + 1; i++)
                {
                    // Don't want to show 0 as 0.00 or 1 as 1.00 so don't set decimal
                    string dString = Convert.ToString(i);
                    if (i != 0 && i != subdivisionCount)
                    {
                        // Display label with 2 decimal places
                        dString = $"{i / (float)subdivisionCount:F2}";
                    }

                    Debug.Log($"{dString} at {subD * i:F2}");
                }
            }
        }
        // Found this online
        int GCD(int a, int b) => b == 0 ? a : GCD(b, a % b);

    }
}
