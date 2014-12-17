using UnityEngine;
using System.Collections;

public class ARCameraPointAtBehaviour : MonoBehaviour
{

    public Transform cube;
    public float speed = 2;
    public Transform positionMarker;
    public int monsterID;


    private Vector3 p;
    private bool[] buttonsHold;
    private bool iconMoveToIsDragging;
    private int fingerMoveTo;

    public GameObject player;


	// Use this for initialization
	void Start () {
        this.iconMoveToIsDragging = false;
        this.buttonsHold = new bool[3];
        this.monsterID = MatchmakerBehaviour.getMonsterID();
	}
	
	// Update is called once per frame
	void Update () {

        // tell monster to move to screen center
        //Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        // tell monster to move to touch position
        Ray ray;
        if (Input.touchSupported)
        {
            Touch finger = Input.GetTouch(fingerMoveTo);
            if ((finger.phase == TouchPhase.Ended || finger.phase == TouchPhase.Stationary))
            {
                this.iconMoveToIsDragging = false;
            }
            ray = Camera.main.ScreenPointToRay(finger.position);
        }
        else
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        }
        RaycastHit hitInfo;
        int layerMask = 1 << 8;
        if (this.iconMoveToIsDragging)
        {
            bool rayhits = Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layerMask);
            if (rayhits)
            {
                this.p = hitInfo.point;
                positionMarker.transform.position = hitInfo.point;
            }
            else { }

            this.OnIconMoveToDrag();
        }
        for (int i = 0; i < this.buttonsHold.Length; i++)
        {
            if (this.buttonsHold[i])
            {
                this.OnIconSkillDrag(i);
            }

        }


	}

    //void OnGUI()
    //{
    //    GUI.TextArea(new Rect(0, 0, 200, 200), Input.GetTouch(fingerMoveTo).position.ToString() + Input.touches[Input.touchCount - 1].position.ToString());
    //}

    public void OnIconMoveToDown()
    {
        print("Hi");
        this.iconMoveToIsDragging = true;

        if (Input.touchSupported)
        {
            this.fingerMoveTo = Input.touches[Input.touchCount - 1].fingerId;
        }
    }

    public void OnIconMoveToDrag()
    {
        this.player.GetComponent<PlayerPointAtBehaviour>().tellMove(this.monsterID, this.p);

    }

    public void OnIconMoveToUp()
    {
        this.iconMoveToIsDragging = false;        

    }

    public void OnIconSkillDown(int skillID)
    {
        this.player.GetComponent<PlayerPointAtBehaviour>().tellSkillStart(this.monsterID, skillID);
        this.buttonsHold[skillID] = true;
    }

    public void OnIconSkillDrag(int skillID)
    {
        this.player.GetComponent<PlayerPointAtBehaviour>().tellSkillStep(this.monsterID, skillID);
    }

    public void OnIconSkillUp(int skillID)
    {
        this.player.GetComponent<PlayerPointAtBehaviour>().tellSkillStop(this.monsterID, skillID);
        this.buttonsHold[skillID] = false;
    }
}
