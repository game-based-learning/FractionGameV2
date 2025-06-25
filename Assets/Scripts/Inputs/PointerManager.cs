using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Utility;

namespace Inputs
{
    public class PointerManager : MonoBehaviour
    {
        private Camera mainCamera;
        private InputSystem_Actions inputActions;
        private InputAction click;

        private GameObject attached = null;

        void Awake()
        {
            mainCamera = Camera.main;
            inputActions = new InputSystem_Actions();
            click = inputActions.UI.Click;

            // Subscribe the hold and release of the clicks
            click.started += mouseDown;
            click.canceled += mouseUp;

            click.Enable();
        }

        private void FixedUpdate()
        {
            transform.position = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        }

        private void mouseDown(InputAction.CallbackContext context)
        {
            Vector3 mousePos = Input.mousePosition;
            Ray ray = mainCamera.ScreenPointToRay(mousePos);
            // Creates a temporary list of hits
            List<RaycastHit2D> hits = new List<RaycastHit2D>();
            if (Physics2D.GetRayIntersection(ray, 50, hits, -5) > 0)
            {
                GameObject gameObject = hits[0].transform.gameObject;
                Draggable item = gameObject.GetComponent<Draggable>();
                if (item != null)
                {
                    // Attach the petal to the pointer object.
                    attached = gameObject;
                    attached.transform.SetParent(transform);
                    attached.GetComponent<Draggable>().Attach();
                }
            }
        }

        private void mouseUp(InputAction.CallbackContext context)
        {
            // If any gameObjects are attached, detach them
            if (attached != null)
            {
                attached.transform.SetParent(null);
                attached.GetComponent<Draggable>().Detach();
                attached = null;
            }
        }
    }
}


