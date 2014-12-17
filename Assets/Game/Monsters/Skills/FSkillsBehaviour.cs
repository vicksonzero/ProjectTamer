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
            behaviour.state = go.GetComponent<BPilotState>();
            behaviour.controller = go.GetComponent<BPilot>();


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
            ((SkillProjectileBehaviour)behaviour).bulletSpeed = ((OSkillProjectile)oSkill).bulletSpeed;
            ((SkillProjectileBehaviour)behaviour).bulletSteerSpeed = ((OSkillProjectile)oSkill).bulletSteerSpeed;
            
            ((SkillProjectileBehaviour)behaviour).spawnPoints = new Transform[((OSkillProjectile)oSkill).spawnPoints.Length];
            for(int i= ((OSkillProjectile)oSkill).spawnPoints.Length-1; i >=0; i--){
                Transform sp = ((OSkillProjectile)oSkill).spawnPoints[i];
                Transform sp_go = Object.Instantiate(sp, go.transform.position, go.transform.rotation) as Transform;
                sp_go.localPosition = sp.localPosition;
                sp_go.localRotation = sp.localRotation;
                sp_go.SetParent(go.transform);
                ((SkillProjectileBehaviour)behaviour).spawnPoints[i] = sp_go;
            }
            
        }

        return behaviour;
    }
}
