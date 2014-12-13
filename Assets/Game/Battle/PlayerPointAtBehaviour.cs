using UnityEngine;
using System.Collections;

public class PlayerPointAtBehaviour : PhotonView {


    public GameObject[] monster;
    private bool[,] skillActivating;

	// Use this for initialization
	void Start () {
        this.monster[0].GetComponent<MonsterController>().enemy = this.monster[1].transform;
        this.monster[1].GetComponent<MonsterController>().enemy = this.monster[0].transform;
        this.skillActivating = new bool[2,4];
	}
	
	// Update is called once per frame
	void Update () {
        for (int j = 0; j < 2; j++)
            for (int i = 0; i < 4; i++)
            {
                if (skillActivating[j, i])
                {
                    this.tellSkillStep(j, i);
                }
            }
	}

    [RPC]
    public void tellMove(int monsterID, Vector3 pos)
    {
        this.getMonsterById(monsterID).SendMessage("CommandMoveTo", pos);
    }

    [RPC]
    public void tellSkillStart(int monsterID, int skillID)
    {
        //Debug.Log("PlayerController");
        this.getMonsterById(monsterID).SendMessage("CommandSkillStart", skillID);
        this.skillActivating[monsterID, skillID] = true;
    }

    // not RPC
    public void tellSkillStep(int monsterID, int skillID)
    {
        this.getMonsterById(monsterID).SendMessage("CommandSkillStep", skillID);
    }

    [RPC]
    public void tellSkillStop(int monsterID, int skillID)
    {
        this.getMonsterById(monsterID).SendMessage("CommandSkillStop", skillID);
        this.skillActivating[monsterID, skillID] = false;

    }

    private GameObject getMonsterById(int i)
    {
        return this.monster[i];

    }
}
