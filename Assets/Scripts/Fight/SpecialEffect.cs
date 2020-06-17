using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEffect : MonoBehaviour {
    [SerializeField]
    public ParticleSystem fire;

    public void SkillEffect() {
        fire.Play();
    }
}
