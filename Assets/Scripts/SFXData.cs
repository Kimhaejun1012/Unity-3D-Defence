using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DF/SFX Database")]
public class SFXData : ScriptableObject
{
    public AudioClip standardHit;
    public AudioClip splashHit;
    public AudioClip lightningHit;
    public AudioClip bladeHit;
    public AudioClip slowSFX;
    public AudioClip stunSFX;
    public AudioClip bossDie;
}
