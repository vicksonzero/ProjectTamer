using UnityEngine;
using System.Collections;

/**
 * Heavy bullet behaviour
 * used with AutoDieBehaviour
 */
public class HeavyBulletBehaviour : MonoBehaviour {

    public Transform firework;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnCollisionEnter(Collision coln)
    {
        if (coln.collider.tag == "Stage")
        {
            // let there be firework!
            Instantiate(this.firework, this.transform.position, this.transform.rotation);
            // bye!
            Destroy(this.gameObject);
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Stage")
        {
            // let there be firework!
            Instantiate(this.firework, this.transform.position, this.transform.rotation);
            // bye!
            Destroy(this.gameObject);
        }
    }
}
