using UnityEngine;

public abstract class UIScreen : MonoBehaviour
{
    private RectTransform container;
    public RectTransform Container => container;


    [Header("Configure Manually")]
    public EnumConfig.ScreenID Id;
    public Vector2 finalPos;

    protected virtual void Awake()
    {
        container = transform.GetChild(0) as RectTransform;
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {

    }
}
