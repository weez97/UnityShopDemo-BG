using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ScreenShop : UIScreen
{
    [Header("Configure Manually")]
    public CanvasGroup cost_group;
    public Text price_txt;
    public CanvasGroup buttons_group;
    public Button buy_equip_btn;
    public Button sell_btn;

    private string current_selection = "";
    private int current_price = 0;
    private bool shop_selling = false;

    // INVENTORY RELATED EVENTS
    public delegate void OnShopTransaction(string id, int price, bool sell = false);
    public static OnShopTransaction onShopTransaction;

    // SHOPKEEPER RESPONSES
    public delegate void OnShopKeeperResponse(string id);
    public static OnShopKeeperResponse onShopKeeperResponse;

    protected override void Awake()
    {
        base.Awake();
        container.GetComponent<CanvasGroup>().DOFade(0, 0);
    }

    protected override void Start()
    {
        ShopButton.onClick += OnItemClick;

    }

    protected override void Update()
    {

    }

    private void OnItemClick(string id, int price)
    {
        current_selection = id;
        current_price = price;

        // one extra validation for shopkeeper dialogues
        bool owned = GameManager.instance.CheckOutfit(current_selection);
        bool canAfford = GameManager.instance.PlayerMoney >= current_price;

        if (!canAfford)
            onShopKeeperResponse?.Invoke(DialogueLibrary.instance.GetResponse(EnumConfig.ResponseType.SHOP_TRANSACTION, 1));
        else
        {
            if (!owned)
                onShopKeeperResponse?.Invoke(DialogueLibrary.instance.GetResponse(EnumConfig.ResponseType.SHOP_GENERIC));
            else
                if (current_selection == "default")
                onShopKeeperResponse?.Invoke(DialogueLibrary.instance.GetResponse(EnumConfig.ResponseType.SHOP_GENERIC, 0));
        }
        UpdateButtons();
    }

    private void UpdateButtons()
    {
        price_txt.color = GameManager.instance.PlayerMoney >= current_price ? Color.white : Color.red;
        price_txt.text = current_price.ToString();

        buttons_group.DOFade(1, 0);
        buttons_group.interactable = true;

        bool owned = GameManager.instance.CheckOutfit(current_selection);
        Text t = buy_equip_btn.GetComponentInChildren<Text>();

        sell_btn.interactable = owned && current_price != 0;

        if (owned)
        {
            t.text = "Equip";
            buy_equip_btn.interactable = true;
            shop_selling = false;
            return;
        }

        bool canAfford = GameManager.instance.PlayerMoney >= current_price;

        buy_equip_btn.interactable = canAfford;
        shop_selling = canAfford;
    }

    private void OnDestroy()
    {
        ShopButton.onClick -= OnItemClick;
    }

    public void Back()
    {
        UiManager.instance.HideDialogue();
        UiManager.instance.ExitScreen("fadeOut", () =>
        {
            Shopkeeper.onInteract?.Invoke(false);
        });
    }

    public void UseOutfit()
    {
        if (shop_selling)
            onShopKeeperResponse?.Invoke(DialogueLibrary.instance.GetResponse(EnumConfig.ResponseType.SHOP_TRANSACTION, 2));

        onShopTransaction?.Invoke(current_selection, current_price);

        UpdateButtons();
    }

    public void SellOutfit()
    {
        onShopKeeperResponse?.Invoke(DialogueLibrary.instance.GetResponse(EnumConfig.ResponseType.SHOP_TRANSACTION, 4));

        onShopTransaction?.Invoke(current_selection, current_price / 2, true);

        UpdateButtons();
    }
}
