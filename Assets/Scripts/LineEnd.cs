using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LineEnd : MonoBehaviour
{
    public bool left = true;
    private TracableLine parentLine;
    // Start is called before the first frame update
    void Start()
    {
        parentLine = GetComponentInParent<TracableLine>();
    }

    private void OnMouseOver()
    {
        if (Mouse.current.leftButton.isPressed == true)
        {
            parentLine.endpointTriggered(left);
        }
        
    }
}
