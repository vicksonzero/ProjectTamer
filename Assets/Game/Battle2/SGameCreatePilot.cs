using UnityEngine;
using System.Collections;

public class SGameCreatePilot {

    public GameObject CreatePilot(int monsterID)
    {
        string name = "Pilot" + monsterID;
        GameObject monster = new GameObject(name);//, typeof(components));

        return monster;
    }
}
