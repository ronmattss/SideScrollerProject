using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerParticles
{
    JumpDust = 0,
    DashImage = 1,
    GroundImpact = 2,
}
public class PlayerParticleSystemManager : MonoBehaviour
{

    public static PlayerParticleSystemManager instance;

    public ParticleSystem[] playerParticles;
    void Start()
    {
        instance = this;
    }


    // play particles passed in the parameter
    public void StartParticle(params PlayerParticles[] effects)
    {
        List<ParticleSystem> listToActivate = new List<ParticleSystem>();
        if (playerParticles != null)
        {   //iterate to the particle lists
            for (int i = 0; i < effects.Length; i++)
            {
                //iterate to the list of effects
                for (int j = 0; j < playerParticles.Length; j++)
                {
                    if (playerParticles[(int)effects[i]] == playerParticles[j])
                    {
                        listToActivate.Add(playerParticles[(int)effects[i]]);
                    }
                    else // Set all other particles to false
                    {
                        playerParticles[i].Stop();
                    }
                }
            }
            foreach (ParticleSystem effect in listToActivate)
            {
                if (effect.isPlaying)
                    effect.Stop();
                effect.gameObject.SetActive(true);
              //  effect.gameObject.transform.Find("DashImage").transform.localScale = this.transform.localScale;
                effect.Play();
            }
        }
    }
    public void StopAllParticles()
    {
        foreach (ParticleSystem effect in playerParticles)
        {
            effect.Stop();
        }
    }
}
