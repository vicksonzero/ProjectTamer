using UnityEngine;
using System.Collections;

public class OSkillCrush : OSkills
{

    [Header("Implementation")]

    public ProjectileBehaviour bullet;

    [Tooltip("relative positions to spawn bullet. \nrotated with player. \n\nmore than 1 entry mean that more than 1 projectile per shot, \nwhich means that damage is scaled up.\n\nProjectiles will fly away from center and steer to target")]
    public Vector3[] offsets = new Vector3[1];

    public float bulletSpeed = 100;

    public float bulletSteerSpeed = 100;


}
