using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class GUICounter : GUIElement
{
    public float maxTime = 0;
    private float c;
    public Text txt;
    private int last_amount;

    // to chain tweens mid animation
    Tween myTween;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        last_amount = 0;
    }

    protected override void Update()
    {
        if (showing)
        {
            c += Time.deltaTime;
            if (c >= maxTime) _Hide();
        }
    }

    public override void Show(bool immediate = false)
    {
        c = 0;
        if (myTween != null)
        {
            // catches tween if it is closing the counter, pauses and kills it, and allows the counter to start anew once again
            myTween.Pause();
            myTween.Kill();
            showing = false;
        }
        if (showing) return;

        showing = true;
        cg.DOFade(1, 0).OnComplete(() =>
        {
            rect.DOAnchorPos(showPosition, immediate ? 0 : 0.85f).SetEase(Ease.OutElastic);
        });
    }

    protected override void _Hide(bool immediate = false)
    {
        showing = false;
        myTween = cg.DOFade(0, immediate ? 0 : 0.65f).OnComplete(() =>
        {
            rect.DOAnchorPos(hidePosition, 0);
        });
    }

    public void UpdateCounter(int amount)
    {
        Show();
        txt.DOCounter(last_amount, amount, 1.25f);
        last_amount = amount;
    }
}
