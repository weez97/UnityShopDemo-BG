using UnityEngine.UI;
using DG.Tweening;

public class GUIDialogue : GUIElement
{
    public Text dialogue_txt;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    public override void Show(bool immediate = false)
    {
        base.Show();
        rect.DOAnchorPos(showPosition, immediate ? 0 : .45f).SetEase(Ease.InSine).OnComplete(() =>
        {
            showing = true;
        });
    }

    protected override void _Hide(bool immediate = false)
    {

        rect.DOAnchorPos(hidePosition, immediate ? 0 : 0.45f).SetEase(Ease.OutSine).OnComplete(() =>
        {
            showing = false;
            dialogue_txt.text = "";
        });
    }

    public void Hide()
    {
        _Hide(false);
    }

    public void UpdateText(string text)
    {
        dialogue_txt.text = text;
    }
}
