using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shopkeeper : InteractableObject
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public override void Interact()
    {
        base.Interact();

        UiManager.instance.ShowScreen(EnumConfig.ScreenID.SHOP, "fadeIn");
        onInteract?.Invoke(true);
    }
}
