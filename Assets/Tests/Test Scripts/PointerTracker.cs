using System.Collections.Generic;
using UnityEngine;
using Utility;

public class PointerTracker : MonoBehaviour
{
    private Camera mainCamera;

    void Awake()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            Ray ray = mainCamera.ScreenPointToRay(mousePos);
            // Creates a temporary list of hits
            List<RaycastHit2D> hits = new List<RaycastHit2D>();
            if (Physics2D.GetRayIntersection(ray,50, hits, -5) > 0)
            {
                GameObject gameObject = hits[0].transform.gameObject;
                Draggable item = gameObject.GetComponent<Draggable>();
                if (item != null)
                {
                    Debug.Log(gameObject);
                }
            }
        }
    }
}
