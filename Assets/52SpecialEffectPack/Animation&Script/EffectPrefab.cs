using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EffectPrefab : MonoBehaviour
{
    [SerializeField]
    private List<ParticleSystem> particles = new List<ParticleSystem>();
    
    public void EffectStart()
    {
        foreach(ParticleSystem particle in particles)
        {
            particle.Play();
        }
    }
}
