using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public delegate void OnInteract(bool b);
    public static OnInteract onInteract;

    public string flavor_text;

    public virtual void Interact()
    {
        StartCoroutine(IInteraction());
    }

    private IEnumerator IInteraction()
    {
        onInteract?.Invoke(true);
        UiManager.instance.ShowDialogue(flavor_text);
        yield return new WaitForSecondsRealtime(1f);
        while (!Input.GetKeyDown(KeyCode.Space))
            yield return null;
        UiManager.instance.HideDialogue();
        yield return new WaitForSecondsRealtime(0.5f);
        onInteract?.Invoke(false);
    }
}
