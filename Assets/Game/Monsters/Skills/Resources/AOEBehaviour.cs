using UnityEngine;
using System.Collections;

/**
 * Heavy bullet behaviour
 * used with AutoDieBehaviour
 */
public class AOEBehaviour : PhotonView
{

    public Transform hitEffectPrefab;
    public SkillAOEBehaviour ownerSkill;
    public Transform target;    // chasing target

    public float animationEndTime;

    private float elapsedTime;
    private float endTime;
    private float duration;

	// Use this for initialization
	void Start () {
        this.animationEndTime = Time.time + 3;
	}

    void OnPhotonInstantiate()
    {
        string targetName = this.photonView.instantiationData[(int)ProjectileParameters.targetName] as string;
        this.target = GameObject.Find(targetName).transform;
    }
	
	// Update is called once per frame
    void Update()
    {
        if (duration > 0)
        {
            this.elapsedTime += Time.deltaTime;
            if (this.elapsedTime < this.duration)
            {
                // nothing
            }
            else
            {
                this.duration = 0;
                Destroy(this.gameObject);
            }
        }
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
        Instantiate(this.hitEffectPrefab, this.transform.position, Quaternion.identity);

        this.ownerSkill.ApplyDamage(this.target);
    }

    public void startAlarm(float duration)
    {
        this.elapsedTime = 0;
        this.duration = duration;
    }
}
