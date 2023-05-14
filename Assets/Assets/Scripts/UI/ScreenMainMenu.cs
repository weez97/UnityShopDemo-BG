using UnityEngine;

public class ScreenMainMenu : UIScreen
{
    public delegate void OnStartGame();
    public static OnStartGame onStartGame;

    public void StartGame()
    {
        UiManager.instance.ExitScreen("slideOut", ()=>
        {
            UiManager.instance.FadeOutBackground();
            onStartGame?.Invoke();
        });
    }

    public void ShowCredits()
    {
        UiManager.instance.ExitScreen("slideOut", () =>
        {
            UiManager.instance.ShowScreen(EnumConfig.ScreenID.CREDITS, "slideIn");
        });
    }

    public void Exit()
    {
        Application.Quit();
    }
}
