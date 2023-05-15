using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class OutfitLibrary : MonoBehaviour
{
    [System.Serializable]
    public class Outfit
    {
        public string id;
        public RuntimeAnimatorController outfit_anim;
    }

    public List<Outfit> outfits;
    private Dictionary<string, RuntimeAnimatorController> dict_outfits = new Dictionary<string, RuntimeAnimatorController>();

    public static OutfitLibrary instance;

    private void Awake()
    {
        if (instance == null) instance = this;

        foreach (Outfit o in outfits)
            dict_outfits.TryAdd(o.id, o.outfit_anim);
    }

    public RuntimeAnimatorController GetOutfit(string _id)
    {
        if (dict_outfits.TryGetValue(_id, out RuntimeAnimatorController outfit))
            return outfit;

        return null;
    }
}
