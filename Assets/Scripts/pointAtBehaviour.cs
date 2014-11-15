using UnityEngine;
using System.Collections;

public class pointAtBehaviour : MonoBehaviour {

    public Transform cube;
    public float speed = 2;
    private Vector3 p;
    public Material over;
    public Material normal;
    public Transform positionMarker;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hitInfo;
        int layerMask = 1 << 8;

        bool rayhits = Physics.Raycast(ray, out hitInfo, Mathf.Infinity,layerMask);
        if (rayhits)
        {
            this.p = hitInfo.point;
            positionMarker.transform.position = hitInfo.point;
            cube.renderer.material = over;
        }
        else
        {
            cube.renderer.material = normal;
        }

        //this.p = ray.origin + (ray.direction * 100);
        //this.p = camera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, camera.nearClipPlane+100));
        //Vector3 displacement = cube.transform.position - p;
        //displacement.Normalize();
        //cube.transform.Translate(displacement * this.speed);
        //cube.transform.position = p;
	}

    void OnGUI()
    {
        GUI.TextArea(new Rect(0, 0, 200, 200), this.p.ToString());
    }
}
