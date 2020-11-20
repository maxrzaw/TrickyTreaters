using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIOnHoverEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Vector3 cachedScale;
    public float scale = 1.5f;
    private Vector3 big;

    void Start()
    {
        // Save the initial scale
        cachedScale = transform.localScale;
        big = new Vector3(cachedScale.x * scale, cachedScale.y * scale, cachedScale.z * scale);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Enlarge the UI Element
        transform.localScale = big;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Reset the scale
        transform.localScale = cachedScale;
    }

    private void OnDisable()
    {
        if (cachedScale.magnitude != 0)
        {
            transform.localScale = cachedScale;
        }
        else
        {
            Debug.Log("scale is zero");
        }
        
    }
}
