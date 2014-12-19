using UnityEngine;
using System.Collections;

public class autoSpinBehaviour : MonoBehaviour {

    public float spinningSpeed = 3;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Rotate(Vector3.up, spinningSpeed * Time.deltaTime);
	}
}
