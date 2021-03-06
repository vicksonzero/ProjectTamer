﻿using UnityEngine;
using System.Collections;

public class OSkillBlast : OSkills
{

    [Header("Implementation")]
    public BlastBehaviour blast;
    [Tooltip("relative positions to spawn bullet. \nrotated with player. Damage will be done ")]
    public Vector3 offset = Vector3.zero;
    [Tooltip("relative direction to spawn bullet. \nrotated with player. ")]
    public Vector3 blastDirection = Vector3.forward;

    public AudioClip shootSound;


}
