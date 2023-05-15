using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScreenShop : UIScreen
{
    private string current_selection = "";

    protected override void Awake()
    {
        GetComponent<CanvasGroup>().DOFade(0, 0);
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

    }
}
