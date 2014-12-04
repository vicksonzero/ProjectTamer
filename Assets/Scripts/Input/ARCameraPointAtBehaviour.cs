using UnityEngine;
using System.Collections;

public class ARCameraPointAtBehaviour : MonoBehaviour
{

    public Transform cube;
    public float speed = 2;
    private Vector3 p;
    public Transform positionMarker;
    private bool[] buttonsHold;
    private bool iconMoveToIsDragging;


    public GameObject player;


	// Use this for initialization
	void Start () {
        this.iconMoveToIsDragging = false;
        this.buttonsHold = new bool[3];
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
        }
        else { }
        for (int i = 0; i < this.buttonsHold.Length; i++)
        {
            if (this.buttonsHold[i])
            {
                this.OnIconSkillDrag(i);
            }

        }
        if (this.iconMoveToIsDragging)
        {
            this.OnIconMoveToDrag();
        }


	}

    void OnGUI()
    {
        GUI.TextArea(new Rect(0, 0, 200, 200), this.p.ToString());
    }

    public void OnIconMoveToDown()
    {
        this.iconMoveToIsDragging = true;

    }

    public void OnIconMoveToDrag()
    {
        this.player.GetComponent<PlayerPointAtBehaviour>().tellMove(0, this.p);

    }

    public void OnIconMoveToUp()
    {
        this.iconMoveToIsDragging = false;        

    }

    public void OnIconSkillDown(int skillID)
    {
        GameObject.Find("MonsterB").transform.FindChild("NameTag").guiText.text = ":P";
        this.player.GetComponent<PlayerPointAtBehaviour>().tellSkillStart(0, skillID);
        this.buttonsHold[skillID] = true;
    }

    public void OnIconSkillDrag(int skillID)
    {
        this.player.GetComponent<PlayerPointAtBehaviour>().tellSkillStep(0, skillID);
    }

    public void OnIconSkillUp(int skillID)
    {
        this.player.GetComponent<PlayerPointAtBehaviour>().tellSkillStop(0, skillID);
        this.buttonsHold[skillID] = false;
    }
}
