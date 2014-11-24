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

    public void tellMove(int i, Vector3 pos)
    {
        this.getMonsterById(i).SendMessage("CommandMoveTo", pos);
    }

    private GameObject getMonsterById(int i)
    {
        return this.monster[i];

    }
}
