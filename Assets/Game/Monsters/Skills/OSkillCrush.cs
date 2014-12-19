using UnityEngine;
using System.Collections;

public class OSkillCrush : OSkills
{

    [Header("Implementation")]

    public CrushBehaviour effectPrefab;
    [Tooltip("relative positions to spawn bullet. \nrotated with player. \n\nmore than 1 entry mean that more than 1 projectile per shot, \nwhich means that damage is scaled up.\n\nProjectiles will then steer to target")]
    public Transform spawnPoint;

    public float crushSpeed = 100;
    public float crushDuration = 1;

    public AudioClip startSound;



}
