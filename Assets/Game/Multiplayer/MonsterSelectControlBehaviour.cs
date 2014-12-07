using UnityEngine;
using System.Collections;

public class MonsterSelectControlBehaviour : MonoBehaviour
{

    public OBattleMessage battleMessage;
    public MonsterListBehaviour monsterList;
    [HideInInspector]
    public bool[] monsterIsSelected;
    [HideInInspector]
    public int monstersAvaliable = 0;

    public int[] chosenMonsters = new int[2]{-1,-1};

    #region Unity events

	// Use this for initialization
    void Start()
    {
        this.battleMessage = new OBattleMessage();
        this.monsterIsSelected = new bool[this.monsterList.list.Length]; // default is false
        this.monstersAvaliable = this.monsterList.list.Length; // default is false
        print("monster list length: " + this.monsterList.list.Length + ", array: " + this.monsterIsSelected);
	}
	
	// Update is called once per frame
	void Update () {

    }
    #endregion //Unity events

    #region public methods


    public void SelectMonster(int monsterID)
    {
        // choose the first empty space
        this.SelectMonster(-1, monsterID);
    }
    /**
     * when index=-1, it will choose the first empty space
     */
    public void SelectMonster(int index, int monsterID)
    {
        if (index < 0 || index > this.chosenMonsters.Length)
        {
            if (this.chosenMonsters.Length == 0)
            {
                this.chosenMonsters[0] = monsterID;
            }
            else
            {
                this.chosenMonsters[1] = monsterID;
            }
        }
        else
        {
            this.chosenMonsters[index] = monsterID;
        }
        this.monsterIsSelected[monsterID] = true;
        this.monstersAvaliable--;
        this.onMonsterSelected();
    }
    public void SwapSelectedMonsters()
    {
        int i = this.chosenMonsters[0];
        this.chosenMonsters[0] = this.chosenMonsters[1];
        this.chosenMonsters[1] = i;
        this.onMonsterSelected();
    }
    public void onMonsterSelected()
    {
        print("chosen main" + this.chosenMonsters[0].ToString() + " supp" + this.chosenMonsters[1].ToString());

    }

    public void SetParam(OBattleMessage bm)
    {
        this.battleMessage = bm;
    }

    public void startBattle()
    {
        PhotonNetwork.LoadLevel("Battle");

    }
    #endregion public methods

} 


