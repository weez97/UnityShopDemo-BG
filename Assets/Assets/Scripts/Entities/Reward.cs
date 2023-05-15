using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : InteractableObject
{
    public int minReward;
    public int maxReward;

    private bool empty = false;

    public delegate void OnPay(int amount);
    public static OnPay onPay;

    public override void Interact()
    {
        if (!empty)
            StartCoroutine(IReward());
        else 
            StartCoroutine(IEmpty());
    }

    public IEnumerator IReward()
    {
        int amountToPay = Payout();
        onInteract?.Invoke(true);
        UiManager.instance.ShowDialogue(DialogueLibrary.instance.GetResponse(EnumConfig.ResponseType.REWARD, 0));
        yield return new WaitForSecondsRealtime(1f);
        while (!Input.GetKeyDown(KeyCode.Space))
            yield return null;
        UiManager.instance.ShowDialogue($"You obtained {amountToPay} coins!");
        yield return new WaitForSecondsRealtime(1f);
        UiManager.instance.HideDialogue();
        onPay?.Invoke(amountToPay);
        empty = true;
        onInteract?.Invoke(false);
    }

    public IEnumerator IEmpty()
    {
        onInteract?.Invoke(true);
        UiManager.instance.ShowDialogue(DialogueLibrary.instance.GetResponse(EnumConfig.ResponseType.REWARD, 1));
        yield return new WaitForSecondsRealtime(1f);
        while (!Input.GetKeyDown(KeyCode.Space))
            yield return null;
        UiManager.instance.HideDialogue();
        onInteract?.Invoke(false);
    }

    private int Payout()
    {
        return Random.Range(minReward, maxReward);
    }
}
