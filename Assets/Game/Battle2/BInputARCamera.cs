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

	// Use this for initialization
    void Start()
    {

	}

    public void IconMove(int monsterID, Vector3 pos)
    {
        // get where the mouse is clicking
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        int layerMask = 1 << 8;

        bool rayHits = Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layerMask);
        if (rayHits)
        {
            // edit from here:
            this.p = hitInfo.point;
            this.positionMarker.transform.position = hitInfo.point;

            //Debug.Log("click");
            this.playerController.Move(this.monsterID, hitInfo.point);
        }
    }

    public void IconSkillStart(int monsterID, int skillID)
    {
        this.playerController.SkillStart(this.monsterID, skillID);
    }

    public void IconSkillStep(int monsterID, int skillID)
    {
        this.playerController.SkillStep(this.monsterID, skillID);
    }

    public void IconSkillStop(int monsterID, int skillID)
    {
        this.playerController.SkillStop(this.monsterID, skillID);
    }
    

}
