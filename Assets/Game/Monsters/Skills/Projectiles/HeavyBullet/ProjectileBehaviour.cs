using UnityEngine;
using System.Collections;

/**
 * Heavy bullet behaviour
 * used with AutoDieBehaviour
 */
public class ProjectileBehaviour : MonoBehaviour
{

    public Transform hitEffectPrefab;
    public Transform target;    // chasing target
    public float bulletSpeed = 100;
    public float bulletSteerSpeed = 100;
    [Tooltip("damages with damage type \n(0=normal, 1=Fire, \n2=Water, 3=Grass, \n4=Electric, 5=Rock)\n\nDamage is done when bullet hits target")]
    public float[] damages = new float[6];
    public GameObject[] debuffs;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    void Update()
    {
        print("3" + this.target.gameObject.name);
        Vector3 targetVector = (this.target.transform.position - this.transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(targetVector);
        this.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * bulletSteerSpeed);

        this.transform.Translate(Vector3.forward * this.bulletSpeed * Time.deltaTime,Space.Self);
	}
    void OnCollisionEnter(Collision coln)
    {
        if (coln.collider.transform == this.target)
        {
            // let there be firework!
            Instantiate(this.hitEffectPrefab, this.transform.position, Quaternion.identity);
            // bye!
            Destroy(this.gameObject);
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.transform == this.target)
        {
            // let there be firework!
            Instantiate(this.hitEffectPrefab, this.transform.position, Quaternion.identity);
            // bye!
            Destroy(this.gameObject);
        }
    }
}
