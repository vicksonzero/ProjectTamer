using UnityEngine;
using System.Collections;

public class FSkillsBehaviour {

	public static SkillsBehaviour attachSkillTo(GameObject go, OSkills oSkill)
    {
        SkillsBehaviour behaviour = null;
        if (oSkill is OSkillAOE)
        {
            //behaviour = go.AddComponent<Skill>();

        }
        else if (oSkill is OSkillBlast)
        {
            behaviour = go.AddComponent<SkillBlastBehaviour>();

            behaviour.skillID = oSkill.skillID;
            behaviour.semiauto = oSkill.semiauto;
            behaviour.cooldown = oSkill.cooldown;
            behaviour.pP = oSkill.pP;
            behaviour.ppRemaining = oSkill.ppRemaining;
            behaviour.range = oSkill.range;
            behaviour.damages = oSkill.damages;
            behaviour.debuffs = oSkill.debuffs;


            ((SkillBlastBehaviour)behaviour).blast = ((OSkillBlast)oSkill).blast;
            ((SkillBlastBehaviour)behaviour).offset = ((OSkillBlast)oSkill).offset;
            ((SkillBlastBehaviour)behaviour).blastDirection = ((OSkillBlast)oSkill).blastDirection;

        }
        else if (oSkill is OSkillCrush)
        {
            Debug.Log("OSkillCrush not ready");

        }
        else if (oSkill is OSkillMelee)
        {
            Debug.Log("OSkillMelee not ready");

        }
        else if (oSkill is OSkillProjectile)
        {
            behaviour = go.AddComponent<SkillProjectileBehaviour>();

            behaviour.skillID = oSkill.skillID;
            behaviour.semiauto = oSkill.semiauto;
            behaviour.cooldown = oSkill.cooldown;
            behaviour.pP = oSkill.pP;
            behaviour.ppRemaining = oSkill.ppRemaining;
            behaviour.range = oSkill.range;
            behaviour.damages = oSkill.damages;
            behaviour.debuffs = oSkill.debuffs;

            ((SkillProjectileBehaviour)behaviour).bullet = ((OSkillProjectile)oSkill).bullet;
            ((SkillProjectileBehaviour)behaviour).offsets = ((OSkillProjectile)oSkill).offsets;
            ((SkillProjectileBehaviour)behaviour).bulletSpeed = ((OSkillProjectile)oSkill).bulletSpeed;
            ((SkillProjectileBehaviour)behaviour).bulletSteerSpeed = ((OSkillProjectile)oSkill).bulletSteerSpeed;

        }

        return behaviour;
    }
}
