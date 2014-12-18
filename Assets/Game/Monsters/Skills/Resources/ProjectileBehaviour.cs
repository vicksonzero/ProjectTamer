using UnityEngine;
using System.Collections;

/**
 * Heavy bullet behaviour
 * used with AutoDieBehaviour
 */
public class ProjectileBehaviour : PhotonView
{

    public Transform hitEffectPrefab;
    public SkillProjectileBehaviour ownerSkill;
    public Transform target;    // chasing target
    public float bulletSpeed = 100;
    public float bulletSteerSpeed = 100;

    public float animationEndTime;

	// Use this for initialization
	void Start () {
        this.animationEndTime = Time.time + 3;
	}

    void OnPhotonInstantiate()
    {
        string targetName = this.photonView.instantiationData[(int)ProjectileParameters.targetName] as string;
        this.target = GameObject.Find(targetName).transform;
        this.bulletSpeed = (float) this.photonView.instantiationData[(int)ProjectileParameters.bulletSpeed];
        this.bulletSteerSpeed = (float) this.photonView.instantiationData[(int)ProjectileParameters.bulletSteerSpeed];
    }
	
	// Update is called once per frame
    void Update()
    {
        Vector3 targetCenter = this.target.TransformPoint( this.target.GetComponent<BoxCollider>().center );//this.target.transform.position + this.target.GetComponent<BoxCollider>().center;
        Vector3 targetVector = (targetCenter - this.transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(targetVector);
        Debug.Log("TODO: use math function to solve the lag problem");
        this.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * this.bulletSteerSpeed);

        this.bulletSteerSpeed *= 1.01f;
        //if (Time.time >= this.animationEndTime)
        //{
        //    this.transform.rotation = targetRotation;
        //}

        this.transform.Translate(Vector3.forward * this.bulletSpeed * Time.deltaTime,Space.Self);
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
        // bye!
        Destroy(this.gameObject);
    }

}
