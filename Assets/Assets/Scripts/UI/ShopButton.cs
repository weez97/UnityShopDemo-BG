using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButton : MonoBehaviour
{
    public string id;
    public int price;

    public delegate void OnClick(string id, int price);
    public static OnClick onClick;

    public void SelectOutfit() 
    {
        onClick?.Invoke(id, price);
    }


}
