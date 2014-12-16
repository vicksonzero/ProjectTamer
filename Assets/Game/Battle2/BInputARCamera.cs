using UnityEngine;
using System.Collections;

public class BInputARCamera : MonoBehaviour
{

    private Vector3 p;
    public Transform positionMarker;
    [HideInInspector]
    public BPlayerController playerController;
    [HideInInspector]
    public int monsterID;


    private int fingerMoveTo;
    private bool[] buttonsHold = new bool[3];
    private bool iconMoveIsDown = false;

    void Update()
    {
        //Debug.Log(this.iconMoveIsDown);
        if (this.iconMoveIsDown)
        {
            // tell monster to move to touch position
            Ray ray;
            if (Input.touchSupported)
            {
                Touch finger = Input.GetTouch(fingerMoveTo);
                if ((finger.phase == TouchPhase.Ended))
                {
                    this.iconMoveIsDown = false;
                }
                ray = Camera.main.ScreenPointToRay(finger.position);
            }
            else
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            }
            RaycastHit hitInfo;
            int layerMask = 1 << 8;

            bool rayhits = Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layerMask);
            if (rayhits)
            {
                this.p = hitInfo.point;
                this.positionMarker.transform.position = hitInfo.point;
            }
            else { }

            this.IconMove(0);
        }
        for (int i = 0; i < this.buttonsHold.Length; i++)
        {
            if (this.buttonsHold[i])
            {
                this.IconSkillStep(0,i);
            }
        }
    }

    public void IconMoveDown()
    {
        this.iconMoveIsDown = true;
        if (Input.touchSupported)
        {
            this.fingerMoveTo = Input.touches[Input.touchCount - 1].fingerId;
        }
    }

    public void IconMoveUp()
    {
        this.iconMoveIsDown = false;

    }

    public void IconMove(int monsterID)
    {
        print("3");
        this.playerController.Move(monsterID, this.p);
    }

    public void IconSkillStart(int monsterID, int skillID)
    {
        this.playerController.SkillStart(this.monsterID, skillID);
        this.buttonsHold[skillID] = true;
    }

    public void IconSkillStep(int monsterID, int skillID)
    {
        this.playerController.SkillStep(this.monsterID, skillID);
    }

    public void IconSkillStop(int monsterID, int skillID)
    {
        this.playerController.SkillStop(this.monsterID, skillID);
        this.buttonsHold[skillID] = false;
    }
    

}
