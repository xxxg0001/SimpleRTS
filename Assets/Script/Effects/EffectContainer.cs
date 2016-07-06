using UnityEngine;
using System.Collections.Generic;

public class EffectContainer : MonoBehaviour {

    

    public List<EffectConfig> effectConfig = new List<EffectConfig>();
    public List<IEffect> effects = new List<IEffect>();


    // Use this for initialization
    void Start () {
        effects = new List<IEffect>();
        for (int i=0; i < effectConfig.Count; i ++)
        {
            IEffect effect = null;
            EffectConfig config = effectConfig[i];
            switch(config.effectType)
            {
                case EffectType.Damage:
                    effect = new DamageEffect(config);
                    break;      
            }
            if (effect != null)
            {
                effects.Add(effect);
            }
            
        }
	
	}
	
	// Update is called once per frame
	void Update () {

	}
}
