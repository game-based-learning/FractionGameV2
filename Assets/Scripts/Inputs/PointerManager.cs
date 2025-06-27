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
        }

        private void OnEnable()
        {
            // Subscribe the hold and release of the clicks
            click.started += MouseDown;
            click.canceled += MouseUp;

            click.Enable();
        }

        private void OnDisable()
        {
            // Unsubscribe the click hold and release events
            click.started -= MouseDown;
            click.canceled -= MouseUp;

            click.Disable();
        }

        private void Update()
        {
            // Used to ensure that draggbale follows this object by tracking mouse position
            transform.position = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        }

        private void MouseDown(InputAction.CallbackContext context)
        {
            // Used to ensure that draggbale follows this object by tracking mouse position
            transform.position = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            // Get a ray from the mouse position on the screen pointing into the screen
            Vector3 mousePos = Mouse.current.position.ReadValue();
            Ray ray = mainCamera.ScreenPointToRay(mousePos);
            // Creates a temporary list of hits to store all items hit
            List<RaycastHit2D> hits = new List<RaycastHit2D>();
            if (Physics2D.GetRayIntersection(ray, 50, hits, -5) > 0)
            {
                // Grab the first hit object, ie the highest in the sorting order
                GameObject gameObject = hits[0].transform.gameObject;
                Draggable item = gameObject.GetComponent<Draggable>();
                if (item != null)
                {
                    // Attach the draggable item to the pointer object.
                    attached = gameObject;
                    attached.transform.SetParent(transform);
                    attached.GetComponent<Draggable>().Attach();
                }
            }
        }

        private void MouseUp(InputAction.CallbackContext context)
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


