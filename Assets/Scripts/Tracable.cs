using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tracable : MonoBehaviour
{
    public bool traced = false;
    public virtual void ResetTrace()
    {
        traced = false;
    }

}
