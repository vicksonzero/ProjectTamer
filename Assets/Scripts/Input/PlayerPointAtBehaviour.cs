using UnityEngine;
using System.Collections;

public class PlayerPointAtBehaviour : MonoBehaviour {


    public GameObject[] monster;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void tellMove(int monsterID, Vector3 pos)
    {
        this.getMonsterById(monsterID).SendMessage("CommandMoveTo", pos);
    }

    public void tellSkillStart(int monsterID, int skillID)
    {
        //Debug.Log("PlayerController");
        this.getMonsterById(monsterID).SendMessage("CommandSkillStart", skillID);
    }

    public void tellSkillStep(int monsterID, int skillID)
    {
        this.getMonsterById(monsterID).SendMessage("CommandSkillStep", skillID);
    }

    public void tellSkillStop(int monsterID, int skillID)
    {
        this.getMonsterById(monsterID).SendMessage("CommandSkillStop", skillID);
    }

    private GameObject getMonsterById(int i)
    {
        return this.monster[i];

    }
}
