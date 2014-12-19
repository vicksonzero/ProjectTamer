using UnityEngine;
using System.Collections;

/**
 * Heavy bullet behaviour
 * used with AutoDieBehaviour
 */
[RequireComponent(typeof(AudioSource))]
public class AOEEarthquakeBehaviour : AOEBehaviour
{


    public Transform hitPrefab;
    public float hitFrequency = 1;
    private float hitElapsedTime = 0;
    private float hitDuration = 0;
    public AudioClip hitPrefabSound;

    private float radius;


    public float animationEndTime;

    private float elapsedTime;
    private float duration;

	// Use this for initialization
	void Start () {
        //this.animationEndTime = Time.time + 3;
        this.hitDuration = 1 / this.hitFrequency;
        this.radius = this.GetComponent<SphereCollider>().radius;
    }

    void OnPhotonInstantiate()
    {
        string targetName = this.photonView.instantiationData[(int)ProjectileParameters.targetName] as string;
        this.target = GameObject.Find(targetName).transform;
    }
	
	// Update is called once per frame
    void Update()
    {
        this.hitElapsedTime += Time.deltaTime;
        if (this.hitElapsedTime < this.hitDuration)
        {
            // nothing
        }
        else
        {
            this.hitElapsedTime = 0;
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
                this.hitElapsedTime = 0;
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

        //Vector2 newPosition = Random.insideUnitCircle * this.radius;
        //Instantiate(this.lightningPrefab, this.transform.position + new Vector3(newPosition.x, 0, newPosition.y), Quaternion.identity);
        stoneToGround(1);
        if (target != null && (target.position - this.transform.position).sqrMagnitude <= this.radius * this.radius)
        {
            Instantiate(this.hitPrefab, this.target.position, Quaternion.identity);
            this.audio.PlayOneShot(hitPrefabSound);


            this.OnHitEnemy();
        }

    }

    private void stoneToGround(int count)
    {
        Vector2 newPosition = Random.insideUnitCircle * this.radius;
        Transform stone = Instantiate(
            this.hitPrefab, 
            this.transform.position + new Vector3(newPosition.x, 500, newPosition.y), 
            Quaternion.identity
            ) as Transform;
        stone.rigidbody.velocity = new Vector3(0, -2000, 0);

    }

}
