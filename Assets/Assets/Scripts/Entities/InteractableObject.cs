using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    public delegate void OnInteract(bool b);
    public static OnInteract onInteract;

    public virtual void Interact() 
    {

    }
}
