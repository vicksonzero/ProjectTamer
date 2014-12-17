using UnityEngine;
using System.Collections;

public class AutoDieBehaviour : MonoBehaviour
{
    [Tooltip("In seconds")]
    public float timeToLive = 5.0f;
    private float birthday = 0;
	// Use this for initialization
	void Start () {
        // compute when to die
        this.birthday = Time.time + this.timeToLive;
	}
	
	// Update is called once per frame
	void Update () {
        // so sad to die on your birthday
        if (Time.time >= this.birthday)
        {
            Destroy(this.gameObject);
        }
	}
}
