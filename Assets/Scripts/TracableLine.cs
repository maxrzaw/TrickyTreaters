using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TracableLine : Tracable
{

    private SpriteRenderer spriteRenderer;

    private bool leftTriggered = false;
    private bool rightTriggered = false;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.white;
    }

    private void OnDestroy()
    {
        return;
    }

    public void endpointTriggered(bool left_end)
    {
        if (left_end == true)
        {
            leftTriggered = true;
        }
        else
        {
            rightTriggered = true;
        }
    }

    public override void ResetTrace()
    {
        base.ResetTrace();
        leftTriggered = false;
        rightTriggered = false;
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.white;
        }
    }

    private void FixedUpdate()
    {
        if (Mouse.current.leftButton.isPressed == false && traced == false)
        {
            // if we have not finished we need to reset
            leftTriggered = false;
            rightTriggered = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (leftTriggered && rightTriggered)
        {
            // Send message somewhere that I was completed
            traced = true;
            // Change the color
            spriteRenderer.color = Color.black;
        }
    }


}
