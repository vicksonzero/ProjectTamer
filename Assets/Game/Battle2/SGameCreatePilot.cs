using UnityEngine;
using System.Collections;

public class SGameCreatePilot :MonoBehaviour {

    public GameObject CreatePilot(int monsterID)
    {
        string name = "Pilot" + monsterID;
        GameObject monster = new GameObject(name);//, typeof(components));

        MonsterController controller = monster.AddComponent<MonsterController>();


        return monster;
    }
}
