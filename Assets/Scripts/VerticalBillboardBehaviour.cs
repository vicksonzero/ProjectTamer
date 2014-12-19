using UnityEngine;
using System.Collections;

public class VerticalBillboardBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Camera.main != null)
        {
            Vector3 cameraVector = Camera.main.transform.position - this.transform.position;
            Vector2 fromVector2 = new Vector2(cameraVector.x, cameraVector.z);
            Vector2 toVector2 = new Vector2(0, 1);

            float ang = Vector2.Angle(fromVector2, toVector2);
            Vector3 cross = Vector3.Cross(fromVector2, toVector2);

            if (cross.z > 0)
                ang = 360 - ang;

            this.transform.rotation = Quaternion.AngleAxis(ang, Vector3.up);
        }
	}
}
