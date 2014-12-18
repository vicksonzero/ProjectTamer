using UnityEngine;
//using UnityEngine.UI;
using System.Collections;

public class BEndGameAnimation : MonoBehaviour {

    public UnityEngine.UI.Image panel;
    public float fadeSpeed = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Color c = this.panel.color;
        c.a = Mathf.Lerp(c.a, 1, Time.deltaTime * this.fadeSpeed);
        this.panel.color = c;
	}
}
