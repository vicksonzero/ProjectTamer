using UnityEngine;
using System.Collections;

public class BInputKeyboardMouse : MonoBehaviour {

    private Vector3 p;
    public Transform positionMarker;

    public BPlayerController playerController;
    public int monsterID;

    private KeyCode keyAnotherPlayer = KeyCode.LeftShift;
    private string swapKey = "space";
    private string[] skillsKey = { "q", "w", "e"};

	// Use this for initialization
    void Start()
    {

	}
	
	// Update is called once per frame
    void Update()
    {
        // left click
        if (Input.GetKey(this.keyAnotherPlayer))
        {
            this.monsterID = 1;
        }
        else
        {
            this.monsterID = 0;
        }
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            // get where the mouse is clicking
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            int layerMask = 1 << 8;

            bool rayHits = Physics.Raycast(ray, out hitInfo, Mathf.Infinity,layerMask);
            if (rayHits)
            {
                // edit from here:
                this.p = hitInfo.point;
                this.positionMarker.transform.position = hitInfo.point;

                //Debug.Log("click");
                this.playerController.Move(this.monsterID, hitInfo.point);
            }

        }

        if (Input.GetKeyDown(this.swapKey))
        {
        }
        int len = this.skillsKey.Length;
        for (int i = 0; i < len; i++)
        {
            if (Input.GetKeyDown(this.skillsKey[i]))
            {
                //Debug.Log("Keyboard");
                this.playerController.SkillStart(this.monsterID, i);
            }
            if (Input.GetKey(this.skillsKey[i]))
            {
                this.playerController.SkillStep(this.monsterID, i);
            }
            if (Input.GetKeyUp(this.skillsKey[i]))
            {
                this.playerController.SkillStop(this.monsterID, i);
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
