using UnityEngine;
using System.Collections;

public class CrushStaticBehaviour : MonoBehaviour {

    public Transform[] staticBalls;
    public float lightningFrequency = 10;
    private float lightningElapsedTime = 0;
    private float lightningDuration = 0;

	// Use this for initialization
	void Start () {
        this.lightningDuration = 1 / this.lightningFrequency;
	    
	}
	
	// Update is called once per frame
	void Update () {

        this.lightningElapsedTime += Time.deltaTime;
        if (this.lightningElapsedTime < this.lightningDuration)
        {
            // nothing
        }
        else
        {
            this.lightningElapsedTime = 0;
            foreach (Transform ball in staticBalls)
            {
                ball.rotation = Random.rotation;
            }
        }
	}
}
