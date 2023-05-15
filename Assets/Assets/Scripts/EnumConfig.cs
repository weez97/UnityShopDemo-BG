using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class EnumConfig
{
    public enum ScreenID
    {
        NONE, MAIN_MENU, CREDITS, EXIT, SHOP
    }

    public enum GameState
    {
        GAME_RUNNING, GAME_INIT, GAME_PAUSED, GAME_DIALOGUE
    }

    public enum ResponseType 
    {
        REWARD, INTERACTABLE, SHOP_GENERIC, SHOP_TRANSACTION
    }
}
