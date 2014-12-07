using UnityEngine;
using System.Collections;

public class ButtonSwitchPlaceAnimateBehaviour : MonoBehaviour {

    [Tooltip("in seconds")]
    public float duration = 1;
    private Quaternion targetRotation;
    private float timeElapsed;

	// Use this for initialization
	void Start () {
        this.targetRotation = this.transform.localRotation;//this.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
        this.timeElapsed += Time.deltaTime;
        float progress = this.timeElapsed / this.duration;

        if (progress >= 1)
        {
            progress = 1;
            //this.cameraNow.enabled = true;
            this.transform.localRotation = Quaternion.identity;
            this.targetRotation = Quaternion.identity;

        }
        this.transform.localRotation = Quaternion.Lerp(this.transform.localRotation, this.targetRotation, progress);
	}

    public void rotate()
    {
        this.transform.localRotation = Quaternion.identity;
        this.targetRotation = Quaternion.AngleAxis(180,Vector3.back);
        this.timeElapsed = 0;
    }
}
