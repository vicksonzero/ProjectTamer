using UnityEngine;
using System.Collections;

public class MultiplayerShowcaseBehaviour : MonoBehaviour {

    public MonsterSelectControlBehaviour monsterSelectControl;
    public int monsterID = 0;
    //public bool wrapIndex = true;
    public RectTransform confirmButtonMain;
    public RectTransform confirmButtonSupp;

    public Camera cameraSet;

    public Transform pad;

    private GameObject[] monsterList;



	// Use this for initialization
	void Start () {
        this.monsterList = this.monsterSelectControl.monsterList.list;

        this.updateMonsterModel();
        this.confirmButtonMain.gameObject.SetActive(true);
        this.confirmButtonSupp.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void prevMonster()
    {
        this.prevMonster(true);
    }
    public void prevMonster(bool wrap)
    {
        print(this.monsterID);
        if (monsterSelectControl.monstersAvaliable <= 1) return;
        if (wrap)
        {
            this.monsterID = (this.monsterID > 0) ? this.monsterID - 1 : this.monsterList.Length - 1;
        }
        else
        {
            if (this.monsterID > 0)
            {
                this.monsterID--;
            }
            else
            {
                // nothing
            }
        }
        bool[] monsterIsSelected = this.monsterSelectControl.monsterIsSelected;

        if (monsterIsSelected[this.monsterID] )
        {
            print("monster #" + this.monsterID + "is selected. prev:");
            this.prevMonster(true);
        }
        print(this.monsterID);
        this.updateMonsterModel();
    }

    public void nextMonster()
    {
        nextMonster(true);
    }
    public void nextMonster(bool wrap)
    {
        print(this.monsterID);
        if (monsterSelectControl.monstersAvaliable <= 1) return;
        if (wrap)
        {
            this.monsterID = (this.monsterID < this.monsterList.Length - 1) ? this.monsterID + 1 : 0;
        }
        else
        {
            if (this.monsterID < this.monsterList.Length - 1)
            {
                this.monsterID++;
            }
            else
            {
                // nothing
            }
        } 
        bool[] monsterIsSelected = this.monsterSelectControl.monsterIsSelected;

        if (monsterIsSelected[this.monsterID] )
        {
            print("monster #" + this.monsterID + "is selected. next:");
            this.nextMonster(true);
        }
        this.updateMonsterModel();
    }
    public void pickMonsterById(int index)
    {
        this.monsterID = index;
    }
    public void confirm(int monsterPlace)
    {
        int monsterID = this.monsterID;
        this.nextMonster(true);
        this.monsterSelectControl.SelectMonster(monsterPlace,monsterID);

        if (monsterPlace == 0)
        {
            this.confirmButtonMain.gameObject.SetActive(false);
            this.confirmButtonSupp.gameObject.SetActive(true);
        }
        else if (monsterPlace == 1)
        {
            this.confirmButtonSupp.gameObject.SetActive(false);
            this.cameraSet.GetComponent<multiplayerCameraBehaviour>().nextScreen();

        }
    }

    private void updateMonsterModel()
    {
        
        for (int i = 0; i < this.pad.childCount; i++ )
        {
            Destroy(this.pad.GetChild(i).gameObject);
        }

        GameObject monster = this.monsterList[this.monsterID];

        Transform model = monster.transform.FindChild("_normalized_model");
        Transform go_model = Instantiate(model, this.transform.position, Quaternion.identity) as Transform;

        go_model.localScale = new Vector3(5,5,5);
        go_model.parent = this.pad;
        
    }
}
