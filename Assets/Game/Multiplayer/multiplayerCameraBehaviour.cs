using UnityEngine;
using System.Collections;

public class multiplayerCameraBehaviour : MonoBehaviour {

    public Camera[] cameras;
    [Tooltip("in seconds")]
    public float transitionLength = 3;
    public int cameraIndex = 0;
    [Tooltip("True if go back to 0 after the last camera")]
    public bool wrapsIndex = false;

    private Camera cameraOld;
    private Camera cameraNow;
    private float timeStarted = 0;
    private float timeElapsed = 0;
    private float progress = 0;


	// Use this for initialization
	void Start () {
        this.cameraOld = this.cameras[this.cameraIndex];
        this.cameraNow = this.cameras[this.cameraIndex];
        this.cameras[this.cameraIndex].enabled = true;
        this.timeElapsed = this.transitionLength;
        this.progress = 0;
        this.disableAllCameras();
	}

    private void disableAllCameras()
    {
        foreach (Camera cam in this.cameras)
        {
            //cam.enabled=false;
            cam.gameObject.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
        // lerp camera smooth switch
        if (this.progress<1)
        {
            this.timeElapsed+=Time.deltaTime;
            this.progress = this.timeElapsed/ this.transitionLength;
            if (this.progress >= 1)
            {
                this.progress = 1;
                //this.cameraNow.enabled = true;
                this.cameraNow.gameObject.SetActive(true);
            }
            this.transform.position = Vector3.Lerp(this.cameraOld.transform.position, this.cameraNow.transform.position, this.progress);
            this.transform.rotation = Quaternion.Lerp(this.cameraOld.transform.rotation, this.cameraNow.transform.rotation, this.progress);
            this.camera.fieldOfView = Mathf.Lerp(this.cameraOld.fieldOfView, this.cameraNow.fieldOfView, this.progress);
        }
	}
    

    public bool nextScreen()
    {

        bool success = false;
        if (this.wrapsIndex)
        {
            this.cameraIndex = (this.cameraIndex < this.cameras.Length - 1) ? this.cameraIndex + 1 : 0;
            success = true;
        }
        else
        {
            if (this.cameraIndex < this.cameras.Length - 1)
            {
                this.cameraIndex++;
                success = true;
            }
            else
            {
                success = false;
            }
        }
        if (success) this.startCameraTransition();
        return success;
    }

    public bool prevScreen()
    {
        bool success = false;
        if (this.wrapsIndex)
        {
            this.cameraIndex = (this.cameraIndex > 0) ? this.cameraIndex - 1 : this.cameras.Length - 1;
            success= true;
        }
        else
        {
            if (this.cameraIndex >0)
            {
                this.cameraIndex--;
                success= true;
            }
            else
            {
                success=false;
            }
        }
        if(success) this.startCameraTransition();
        return success;
    }
    private void startCameraTransition()
    {
        //this.cameraOld.enabled = false;
        this.cameraNow.gameObject.SetActive(false);
        this.cameraOld = this.cameraNow;
        this.cameraNow = this.cameras[this.cameraIndex];


        this.timeElapsed = 0;
        this.progress = 0;

    }
}
