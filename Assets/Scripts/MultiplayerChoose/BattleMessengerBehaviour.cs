using UnityEngine;
using System.Collections;

public class BattleMessengerBehaviour : MonoBehaviour
{

    public OBattleMessage battleMessage;

    public int[] chosenMonsters;

    #region Unity events
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

	// Use this for initialization
    void Start()
    {
        this.battleMessage = new OBattleMessage();
	}
	
	// Update is called once per frame
	void Update () {

    }
    #endregion //Unity events

    #region public methods

    public void SelectMonster(int monsterID)
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
    public void SwapSelectedMonsters()
    {
        int i = this.chosenMonsters[0];
        this.chosenMonsters[0] = this.chosenMonsters[1];
        this.chosenMonsters[1] = i;
    }

    public void SetParam(OBattleMessage bm)
    {
        this.battleMessage = bm;
    }
    #endregion public methods

} 


