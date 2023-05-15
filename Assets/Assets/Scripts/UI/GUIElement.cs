using UnityEngine;
using DG.Tweening;

public abstract class GUIElement : MonoBehaviour
{
    [Header("Configure Manually")]
    public Vector2 showPosition;
    public Vector2 hidePosition;
    public bool startVisible = false;

    protected CanvasGroup cg;
    protected RectTransform rect;
    protected bool showing;
    public bool isShowing => showing;

    protected virtual void Awake()
    {
        cg = GetComponent<CanvasGroup>();
        rect = transform as RectTransform;
    }

    protected virtual void Start()
    {
        if (!startVisible)
            _Hide(true);
    }

    protected virtual void Update()
    {

    }

    public virtual void Show(bool immediate = false)
    {

    }

    protected virtual void _Hide(bool immediate = false)
    {

    }
}
