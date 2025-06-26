using System;
using UnityEngine;

namespace Utility
{
    public abstract class Draggable : MonoBehaviour
    {
        private bool isHeld = false;
        public bool IsHeld { get { return isHeld; } }

        private void Start()
        {
            isHeld = false;
        }

        /**
         * Attaches current game object to pointer
         * Requires object to not be held
         */
        public virtual void Attach()
        {
            // If already held, ignore
            if (isHeld) return;

            // Foreground
            GetComponent<SpriteRenderer>().sortingLayerName = "Foreground";
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

            // Send to Default
            GetComponent<SpriteRenderer>().sortingLayerName = "Default";
            isHeld = false;
        }
    }
}

