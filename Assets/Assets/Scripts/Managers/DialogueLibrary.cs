using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueLibrary : MonoBehaviour
{
    [System.Serializable]
    public class ResponseBundle
    {
        public EnumConfig.ResponseType type;
        public string[] responses;
    }

    public List<ResponseBundle> responses = new List<ResponseBundle>();
    private Dictionary<EnumConfig.ResponseType, string[]> dict_responses = new Dictionary<EnumConfig.ResponseType, string[]>();

    public static DialogueLibrary instance;

    void Awake()
    {
        if (instance == null) instance = this;

        foreach (ResponseBundle bundle in responses)
            dict_responses.TryAdd(bundle.type, bundle.responses);
    }

    public string GetResponse(EnumConfig.ResponseType _type, int index = -1)
    {
        if (dict_responses.TryGetValue(_type, out string[] myResponses))
            return myResponses[index == -1 ? Random.Range(0, myResponses.Length) : index];

        return null;
    }
}
