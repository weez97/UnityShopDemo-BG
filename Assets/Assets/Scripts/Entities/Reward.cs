using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : InteractableObject
{
    public int minReward;
    public int maxReward;

    public override void Interact()
    {
        Debug.LogError(Random.Range(minReward, maxReward));
    }
}
