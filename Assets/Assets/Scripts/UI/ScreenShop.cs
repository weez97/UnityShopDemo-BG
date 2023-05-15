using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ScreenShop : UIScreen
{
    [Header("Configure Manually")]
    public CanvasGroup cost_group;
    public Text cost_txt;
    public Text price_txt;

    private string current_selection = "";
    private int current_price = 0;

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

        price_txt.text = price.ToString();
    }

    public void Back()
    {
        UiManager.instance.ExitScreen("fadeOut", () =>
        {
            Shopkeeper.onInteract?.Invoke(false);
        });
    }
}
