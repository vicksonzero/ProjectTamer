using UnityEngine;
using System.Collections;

public class SGameCreatePilot :MonoBehaviour {

    public MonsterListBehaviour monsterDefinition;


    public GameObject CreatePilotWithMonster(int playerID, int monsterID, int networkId)
    {
        string name = "Pilot" + playerID;
        GameObject pilot_go = new GameObject(name);//, typeof(components));

        //MonsterController controller = pilot.AddComponent<MonsterController>();
        BPilot pilotBehaviour = pilot_go.AddComponent<BPilot>();
        pilotBehaviour.state = pilot_go.AddComponent<BPilotState>();

        PhotonView pv = pilot_go.AddComponent<PhotonView>();
        pv.viewID = networkId;
        pv.synchronization = ViewSynchronization.ReliableDeltaCompressed;
        pv.observed = pilot_go.transform;
        pv.onSerializeTransformOption = OnSerializeTransform.PositionAndRotation;
        pilotGetIntoMonster(pilot_go, monsterID);

        return pilot_go;
    }

    public void pilotGetIntoMonster(GameObject pilot_go, int monsterID)
    {
        BPilot pilotBehaviour = pilot_go.GetComponent<BPilot>();
        pilotBehaviour.data = monsterDefinition.list[monsterID].GetComponent<MonsterData>();

        OSkills[] oSkills = monsterDefinition.list[monsterID].GetComponents<OSkills>();

        foreach (OSkills oSkill in oSkills)
        {
            int i = oSkill.skillID;
            // errors
            if (i < 0 || i > 2)
            {
                Debug.LogError("skillID should be between 0 and 2: monster" + monsterID);
            }
            if (pilotBehaviour.skillsDef[i] != null)
            {
                Debug.LogError("Overlapping SkillID " + i + ": monster" + monsterID);
            }
            // actual
            pilotBehaviour.skillsDef[i] = oSkill;
        }
        for (int i=pilotBehaviour.skillsDef.Length-1; i >=0; i--)
        {
            OSkills oSkill = pilotBehaviour.skillsDef[i];
            pilotBehaviour.skills[i] = FSkillsBehaviour.attachSkillTo(pilot_go, oSkill);
        }
        GameObject monster = this.monsterDefinition.list[monsterID];

        Transform model = monster.transform.FindChild("_normalized_model");
        Transform go_model = Instantiate(model, Vector3.zero, Quaternion.identity) as Transform;
        go_model.SetParent(pilot_go.transform);


        //pilotBehaviour.skills
    }

}
