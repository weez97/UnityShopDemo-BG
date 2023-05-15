using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class GUICounter : GUIElement
{
    public float maxTime = 0;
    private float c;
    public Text txt;
    private int last_amount;

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
        base.Show();
        cg.DOFade(1, 0).OnComplete(() =>
        {
            rect.DOAnchorPos(showPosition, immediate ? 0 : 0.85f).SetEase(Ease.OutElastic).OnComplete(() => { showing = true; });
        });
    }

    protected override void _Hide(bool immediate = false)
    {
        showing = false;
        base._Hide();
        cg.DOFade(0, immediate ? 0 : 0.65f).OnComplete(() =>
        {
            rect.DOAnchorPos(hidePosition, 0);
        });
    }

    public void IncreaseCounter(int amount) 
    {
        Show();
        txt.DOCounter(last_amount, amount, 1.25f);
        last_amount = amount;
    }
}
