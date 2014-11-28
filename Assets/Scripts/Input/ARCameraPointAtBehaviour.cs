﻿using UnityEngine;
using System.Collections;

public class ARCameraPointAtBehaviour : MonoBehaviour
{

    public Transform cube;
    public float speed = 2;
    private Vector3 p;
    public Transform positionMarker;



    public GameObject player;


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

            //this.player.GetComponent<PlayerPointAtBehaviour>().tellMove(0, hitInfo.point);

        }
        else
        {
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

    void OnIconMoveToDrag()
    {
        this.player.GetComponent<PlayerPointAtBehaviour>().tellMove(0, this.p);

    }

    void OnIconSkillDown(int skillID)
    {
        this.player.GetComponent<PlayerPointAtBehaviour>().tellSkillStart(0, skillID);
    }

    void OnIconSkillDrag(int skillID)
    {
        this.player.GetComponent<PlayerPointAtBehaviour>().tellSkillStep(0, skillID);
    }

    void OnIconSkillUp(int skillID)
    {
        this.player.GetComponent<PlayerPointAtBehaviour>().tellSkillStop(0, skillID);
    }
}