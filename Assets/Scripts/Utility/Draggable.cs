using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FractionGame.Utility
{
    public abstract class Draggable : MonoBehaviour
    {
        private bool isHeld = false;
        public bool IsHeld { get { return isHeld; } }

        private static List<Draggable> instances = new List<Draggable>();
        private static int highestSortingOrder = 0;
        private int sortingOrder;

        protected virtual void Awake()
        {
            isHeld = false;
            highestSortingOrder++;
            sortingOrder = highestSortingOrder;
            // Add self to static instances list to track sorting order
            instances.Add(this);
        }

        protected virtual void OnDestroy()
        {
            // Remove self from static instances list to free space
            instances.Remove(this);
        }

        /**
         * Attaches current game object to pointer
         * Requires object to not be held
         */
        public virtual void Attach()
        {
            // If already held, ignore
            if (isHeld) return;

            // Send sprite to foreground layer
            GetComponent<SpriteRenderer>().sortingLayerName = "Foreground";
            highestSortingOrder++;
            sortingOrder = highestSortingOrder;
            // If highestSortingOrder is too high, renormalize it
            if (highestSortingOrder > 100)
            {
                RenormalizeSortingOrder();
            }
            isHeld = true;
        }

        /**
         * Dettaches current game object from pointer
         * Requires object to be held
         */
        public virtual void Detach()
        {
            // If not held, ignore
            if (!isHeld) return;

            // Send sprite to default layer
            GetComponent<SpriteRenderer>().sortingLayerName = "Default";
            GetComponent<SpriteRenderer>().sortingOrder = sortingOrder;
            isHeld = false;
        }

        /**
         * Resets sorting order to start from 0
         */
        private static void RenormalizeSortingOrder()
        {
            List<Draggable> normInstances = instances.OrderBy(item => item.sortingOrder).ToList();
            highestSortingOrder = normInstances.Count;

            for (int i = 0; i < highestSortingOrder; i++)
            {
                normInstances[i].sortingOrder = i;
            }
        }
    }
}

