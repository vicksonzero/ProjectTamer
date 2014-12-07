using UnityEngine;
using System.Collections;

public class touchButtonsBehaviour : MonoBehaviour {


    public Transform cam;
	// Use this for initialization
	void Start () {
        if (!cam.gameObject.activeSelf)
        {
            this.enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
        this.enabled = cam.gameObject.activeSelf;
	}
}
