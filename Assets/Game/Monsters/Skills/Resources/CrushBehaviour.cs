using UnityEngine;
using System.Collections;

/**
 * Heavy bullet behaviour
 * used with AutoDieBehaviour
 */
public class CrushBehaviour : PhotonView
{

    public Transform hitEffectPrefab;
    public SkillCrushBehaviour ownerSkill;
    public Transform target;    // damage checking target

    public float animationEndTime;

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
	}
    void OnCollisionEnter(Collision coln)
    {
        if (coln.collider.transform == this.target)
        {
            this.OnHitEnemy(coln.contacts[0].point);
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.transform == this.target)
        {
            this.OnHitEnemy(col.ClosestPointOnBounds(this.transform.position));
        }
    }

    void OnHitEnemy(Vector3 contactPoint)
    {
        // let there be firework!
        Instantiate(this.hitEffectPrefab, contactPoint, Quaternion.identity);

        this.ownerSkill.ApplyDamage(this.target);

        Destroy(this.gameObject);
    }

}
