using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SGameCreatePilot))]
public class BGameController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject.Find("MonsterSelectControl");
	}
	
	// Update is called once per frame
	void Update () {
	
	}



}
