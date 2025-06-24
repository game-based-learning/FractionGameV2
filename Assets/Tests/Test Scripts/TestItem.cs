using UnityEngine;
using Utility;

public class TestItem : Draggable
{
    void Start()
    {
        Debug.Log(transform.position);
    }
}
