using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GUIText))]
public class ObjectLabelB : MonoBehaviour
{

    public Transform target;  // Object that this label should follow
    public Vector3 offset = Vector3.up;    // Units in world space to offset; 1 unit above object by default
    public bool clampToScreen = false;  // If true, label will be visible even if object is off screen
    public float clampBorderSize = 0.05f;  // How much viewport space to leave at the borders when a label is being clamped
    public bool useMainCamera = true;   // Use the camera tagged MainCamera
    public Camera cameraToUse;   // Only use this if useMainCamera is false
    Camera cam;

    void Start()
    {
        if (useMainCamera)
            cam = Camera.main;
        else
            cam = cameraToUse;
    }


    void Update()
    {
        //string debugMsg = "";
        //debugMsg += this.transform.position + "\n";
        if (clampToScreen)
        {
            //debugMsg += target.position + "\n";
            //debugMsg += this.offset + "\n";
            Vector3 relativePosition = cam.transform.InverseTransformPoint(target.position + this.offset);
            //debugMsg += relativePosition + "\n";

            relativePosition.z = Mathf.Max(relativePosition.z, 1.0f);
            this.transform.position = cam.WorldToViewportPoint(cam.transform.TransformPoint(relativePosition));
            //debugMsg += this.transform.position + "\n";


            this.transform.position = new Vector3(Mathf.Clamp(this.transform.position.x, clampBorderSize, 1.0f - clampBorderSize),
                                             Mathf.Clamp(this.transform.position.y, clampBorderSize, 1.0f - clampBorderSize),
                                             0);
            //debugMsg += this.transform.position + "\n";
        }
        else
        {
            this.transform.position = cam.WorldToViewportPoint(target.position + offset);
        }
        //Debug.Log(debugMsg);
    }
}