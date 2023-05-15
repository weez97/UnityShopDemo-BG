using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    public Transform canvas;
    public Image background;
    public GUICounter counter;
    public GUIDialogue dialogue;
    private bool showingDialogue = false;
    public bool ShowingDialogue => showingDialogue;

    // UIScreens
    public List<UIScreen> screens;
    private Dictionary<EnumConfig.ScreenID, GameObject> screen_dict = new Dictionary<EnumConfig.ScreenID, GameObject>();
    private UIScreen active_screen = null;


    void Awake()
    {
        if (instance == null) instance = this;
        foreach (UIScreen screen in screens)
            screen_dict.TryAdd(screen.Id, screen.gameObject);
    }

    // UISCREENS

    public void ShowScreen(EnumConfig.ScreenID _id, string animation = "")
    {
        if (active_screen != null) return;

        if (screen_dict.TryGetValue(_id, out GameObject sc_obj))
        {
            active_screen = Instantiate(sc_obj, canvas).GetComponent<UIScreen>();
            AnimInTransition(animation);
        }
        else
            Debug.LogWarning($"Tried to load an unexistant UIScreen: {_id}.");
    }

    public void ExitScreen(string animation = "", TweenCallback callback = null)
    {
        if (active_screen == null) return;

        AnimOutTransition(animation, () =>
        {
            Destroy(active_screen.gameObject);
            active_screen = null;
            callback?.Invoke();
        });
    }

    // ANIMATIONS
    private Vector2 screen_start_pos; // this one is just in case we ever need to undo our transition

    private void AnimInTransition(string animation = "", TweenCallback callback = null)
    {
        RectTransform container = active_screen.Container;
        screen_start_pos = active_screen.Container.anchoredPosition;
        Vector2 targetPos = active_screen.finalPos;

        switch (animation)
        {
            case "slideIn":
                container.DOAnchorPos(targetPos, 1.67f).SetEase(Ease.OutQuint).OnComplete(callback);
                break;
            case "fadeIn":
                container.GetComponent<CanvasGroup>().DOFade(1, 0.4f).SetEase(Ease.InSine);
                break;
            case "":
                container.DOAnchorPos(targetPos, 0).OnComplete(callback);
                break;
        }
    }

    private void AnimOutTransition(string animation = "", TweenCallback callback = null)
    {
        switch (animation)
        {
            case "slideOut":
                active_screen.Container.DOAnchorPos(screen_start_pos, 1.0f).SetEase(Ease.OutQuad).OnComplete(callback);
                break;
            case "fadeOut":
                active_screen.Container.GetComponent<CanvasGroup>().DOFade(0, 0.4f).SetEase(Ease.OutSine).OnComplete(callback);
                break;
            case "":
                active_screen.Container.DOAnchorPos(screen_start_pos, 0).OnComplete(callback);
                break;
        }
    }

    public void FadeOutBackground()
    {
        background.DOFade(0, 0.75f);
    }

    // DIALOGUES
    public void ShowDialogue(string txt)
    {
        dialogue.UpdateText(txt);

        if (!dialogue.isShowing)
            dialogue.Show();
    }

    public void HideDialogue()
    {
        dialogue.UpdateText("");

        if (dialogue.isShowing)
            dialogue.Hide();
    }
}
