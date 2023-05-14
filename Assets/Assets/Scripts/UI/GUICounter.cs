using UnityEngine;
using DG.Tweening;

public class GUICounter : GUIElement
{
    public float maxTime = 0;
    private float c;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        if (showing)
        {
            c += Time.deltaTime;
            if (c >= maxTime) Hide();
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

    protected override void Hide(bool immediate = false)
    {
        showing = false;
        base.Hide();
        cg.DOFade(0, immediate ? 0 : 0.65f).OnComplete(() =>
        {
            rect.DOAnchorPos(hidePosition, 0);
        });
    }
}
