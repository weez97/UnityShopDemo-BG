using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shopkeeper : InteractableObject
{
    void Start()
    {
        ScreenShop.onShopKeeperResponse += OnDialogueResponse;
    }

    private void OnDestroy()
    {
        ScreenShop.onShopKeeperResponse -= OnDialogueResponse;
    }

    void Update()
    {

    }

    public override void Interact()
    {
        base.Interact();
        onInteract?.Invoke(true);

        if (!GameManager.instance.IsEverthingOwned)
            UiManager.instance.ShowDialogue(DialogueLibrary.instance.GetResponse(EnumConfig.ResponseType.SHOP_TRANSACTION, 0));
        else
            UiManager.instance.ShowDialogue(DialogueLibrary.instance.GetResponse(EnumConfig.ResponseType.SHOP_TRANSACTION, 3));

        UiManager.instance.ShowScreen(EnumConfig.ScreenID.SHOP, "fadeIn");
    }

    private void OnDialogueResponse(string dialogue)
    {
        UiManager.instance.ShowDialogue(dialogue);
    }
}
