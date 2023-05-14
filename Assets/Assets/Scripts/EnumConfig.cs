using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class EnumConfig
{
    public enum ScreenID
    {
        NONE, MAIN_MENU, CREDITS, EXIT
    }

    public enum GameState
    {
        GAME_RUNNING, GAME_INIT, GAME_PAUSED
    }

}
