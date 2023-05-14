public class ScreenCredits : UIScreen
{
    public void Back()
    {
        UiManager.instance.ExitScreen("slideOut", () =>
        {
            UiManager.instance.ShowScreen(EnumConfig.ScreenID.MAIN_MENU, "slideIn");
        });
    }
}
