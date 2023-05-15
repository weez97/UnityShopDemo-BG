using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shopkeeper : InteractableObject
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
        base.Interact();

        UiManager.instance.ShowScreen(EnumConfig.ScreenID.SHOP, "fadeIn");
    }
}
