using UnityEngine;
using System.Collections;

public class KeyboardMousePointAtBehaviour : MonoBehaviour
{


    public float speed = 2;
    private Vector3 p;
    public Transform positionMarker;

    public GameObject player;
    public int monsterID;

    private string stanceKey = "q";
    private string[] skillsKey = { "w", "e", "r"};

	// Use this for initialization
    void Start()
    {
        this.monsterID = MatchmakerBehaviour.getMonsterID();
	
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

            // don't edit from here:
        }
        else
        {
            // not hit
        }

        if (Input.GetKeyDown(this.stanceKey))
        {
            //this.player.GetComponent<PlayerPointAtBehaviour>().
        }
        int len = this.skillsKey.Length;
        for (int i = 0; i < len; i++)
        {
            if (Input.GetKeyDown(this.skillsKey[i]))
            {
                //Debug.Log("Keyboard");
                this.player.GetComponent<PlayerPointAtBehaviour>().tellSkillStart(0,i);
            }
            if (Input.GetKey(this.skillsKey[i]))
            {
                this.player.GetComponent<PlayerPointAtBehaviour>().tellSkillStep(0, i);
            }
            if (Input.GetKeyUp(this.skillsKey[i]))
            {
                this.player.GetComponent<PlayerPointAtBehaviour>().tellSkillStop(0, i);
            }
        }


	}

    /*
    void OnGUI()
    {
        GUI.TextArea(new Rect(0, 0, 200, 200), this.p.ToString());
    }
    */

    

    


}
