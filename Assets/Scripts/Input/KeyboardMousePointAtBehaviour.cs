using UnityEngine;
using System.Collections;

public class KeyboardMousePointAtBehaviour : MonoBehaviour
{


    public float speed = 2;
    private Vector3 p;
    public Transform positionMarker;

    public GameObject player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        // get where the mouse is clicking
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        int layerMask = 1 << 8;

        bool rayhits = Physics.Raycast(ray, out hitInfo, Mathf.Infinity,layerMask);
        if (rayhits)
        {
            // edit from here:
            this.p = hitInfo.point;
            positionMarker.transform.position = hitInfo.point;

            // left click
            if (Input.GetMouseButton(0))
            {
                //Debug.Log("click");
                this.player.GetComponent<PlayerPointAtBehaviour>().tellMove(0, hitInfo.point);
            }

            // right click
            if (Input.GetMouseButton(1))
            {
                //Debug.Log("click");
                this.player.GetComponent<PlayerPointAtBehaviour>().tellMove(1, hitInfo.point);
            }





            //Send(CommandMoveTo)

            // don't edit from here:
        }
        else
        {
            // not hit
        }

	}

    /*
    void OnGUI()
    {
        GUI.TextArea(new Rect(0, 0, 200, 200), this.p.ToString());
    }
    */

    

    


}
