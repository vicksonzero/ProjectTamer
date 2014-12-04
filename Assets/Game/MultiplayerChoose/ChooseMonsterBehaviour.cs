using UnityEngine;
using System.Collections;

public class ChooseMonsterBehaviour : MonoBehaviour {

    public int monsterID=-1;
    private GameObject bm;

	// Use this for initialization
	void Start () {
        this.bm = GameObject.Find("BattleMessenger");
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnMouseClick()
    {
        this.bm.SendMessage("SelectMonster", this.monsterID);

    }
}
