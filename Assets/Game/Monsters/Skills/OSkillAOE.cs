using UnityEngine;
using System.Collections;

public class OSkillAOE : OSkills
{

    [Header("Implementation")]

    public AOEBehaviour effectPrefab;
    public float radius;

    [Tooltip("The feet of enemy, or at my feet")]
    public SpawnAt spawnAt;
    public enum SpawnAt { MYSELF, ENEMY };

    public float duration;

    public AudioClip startSound;
    public AudioClip hitSound;



}
