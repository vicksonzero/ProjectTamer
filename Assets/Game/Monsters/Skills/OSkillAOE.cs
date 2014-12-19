using UnityEngine;
using System.Collections;

public class OSkillAOE : OSkills
{

    [Header("Implementation")]

    public AOEBehaviour effectPrefab;

    [Tooltip("The feet of enemy, or at my feet")]
    public SpawnAt spawnAt;
    public enum SpawnAt { MYSELF, ENEMY };

    public float duration;


}
