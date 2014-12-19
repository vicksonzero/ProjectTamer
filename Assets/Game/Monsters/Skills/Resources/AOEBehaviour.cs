using UnityEngine;
using System.Collections;

/**
 * Heavy bullet behaviour
 * used with AutoDieBehaviour
 */
[RequireComponent(typeof(AudioSource))]
public class AOEBehaviour : PhotonView
{

    public Transform hitEffectPrefab;
    public SkillAOEBehaviour ownerSkill;
    public Transform target;    // chasing target




	// Use this for initialization
	void Start () {
	}

    void OnPhotonInstantiate()
    {
        string targetName = this.photonView.instantiationData[(int)ProjectileParameters.targetName] as string;
        this.target = GameObject.Find(targetName).transform;
    }
	
	// Update is called once per frame
    void Update()
    {


	}
    void OnCollisionEnter(Collision coln)
    {
        if (coln.collider.transform == this.target)
        {
            this.OnHitEnemy();
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.transform == this.target)
        {
            this.OnHitEnemy();
        }
    }

    void OnHitEnemy()
    {
        // let there be firework!
        // firework moved to spawnLightnings()!

        this.ownerSkill.ApplyDamage(this.target);
    }

    public void startAlarm(float duration)
    {
        //this.elapsedTime = 0;
        //this.duration = duration;
    }


}
