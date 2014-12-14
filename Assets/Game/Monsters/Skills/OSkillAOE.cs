using UnityEngine;
using System.Collections;

public class OSkillAOE : OSkills
{

    [Header("Implementation")]

    public Transform effectPrefab;

    [Tooltip("The feet of enemy, or at my feet")]
    public SpawnAt spawnAt;
    public enum SpawnAt { MYSELF, ENEMY };

    [Tooltip("relative positions to spawn effect. \nNOT rotated with player.")]
    public Vector3[] offsets = new Vector3[1];


}
