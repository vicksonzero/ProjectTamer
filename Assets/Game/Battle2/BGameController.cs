using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SGameCreatePilot))]
public class BGameController : MonoBehaviour {

    public MonsterSelectControlBehaviour monsterSelectControl;

    public int[][] chosenMonsters;

    [HideInInspector]
    public GameObject[][] monstersGO = new GameObject[2][] { new GameObject[2], new GameObject[2] };

    private SGameCreatePilot pilotCreator;

	// Use this for initialization
	void Start () {
        this.pilotCreator = this.GetComponent<SGameCreatePilot>();
        GameObject monsterSelectControl_go = GameObject.Find("MonsterSelectControl");
        if (monsterSelectControl_go == null)
        {
            this.chosenMonsters = new int[2][]{new int[2]{0,1},new int[2]{2,3}};
        }
        else
        {
            MonsterSelectControlBehaviour monsterSelectControl = monsterSelectControl_go.GetComponent<MonsterSelectControlBehaviour>();
            this.chosenMonsters = monsterSelectControl.chosenMonsters;
        }
        print(this.chosenMonstersToString());

        this.monstersGO[0][0] = this.pilotCreator.CreatePilot(0);

	}
	
	// Update is called once per frame
	void Update () {
	
	}




    private string chosenMonstersToString()
    {
        string str = "";

        str += this.chosenMonsters[0][0] + " ";
        str += this.chosenMonsters[0][1] + " ";
        str += this.chosenMonsters[1][0] + " ";
        str += this.chosenMonsters[1][1] + " ";


        return str;

    }

}
