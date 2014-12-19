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

    public ParticleSystem ps;
    public float radius;
    public Transform lightningPrefab;
    public float lightningFrequency = 1;
    private float lightningElapsedTime=0;
    private float lightningDuration=0;

    public float animationEndTime;

    private float elapsedTime;
    private float duration;

	// Use this for initialization
	void Start () {
        //this.animationEndTime = Time.time + 3;
        this.GetComponentInChildren<CapsuleCollider>().radius = this.radius;
        this.lightningDuration = 1 / this.lightningFrequency;
	}

    void OnPhotonInstantiate()
    {
        string targetName = this.photonView.instantiationData[(int)ProjectileParameters.targetName] as string;
        this.target = GameObject.Find(targetName).transform;
    }
	
	// Update is called once per frame
    void Update()
    {
        this.lightningElapsedTime += Time.deltaTime;
        if (this.lightningElapsedTime < this.lightningDuration)
        {
            // nothing
        }
        else
        {
            this.lightningElapsedTime = 0;
            this.spawnLightnings();
        }


        if (duration > 0)
        {
            this.elapsedTime += Time.deltaTime;
            if (this.elapsedTime < this.duration)
            {
                // nothing
            }
            else
            {
                this.lightningElapsedTime = 0;
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
        // firework moved to spawnLightnings()!

        this.ownerSkill.ApplyDamage(this.target);
    }

    public void startAlarm(float duration)
    {
        this.elapsedTime = 0;
        this.duration = duration;
    }

    private void spawnLightnings()
    {

        Vector2 newPosition = Random.insideUnitCircle * this.radius;

        Instantiate(this.lightningPrefab, this.transform.position + new Vector3(newPosition.x, 0, newPosition.y), Quaternion.identity);

        if (target != null && (target.position - this.transform.position).sqrMagnitude <= this.radius * this.radius)
        {
            Instantiate(this.lightningPrefab, this.target.position, Quaternion.identity);
            Instantiate(this.hitEffectPrefab, this.target.position, Quaternion.identity);

            this.OnHitEnemy();
        }

    }

}
