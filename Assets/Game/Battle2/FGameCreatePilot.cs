using UnityEngine;
using System.Collections;

public class FGameCreatePilot :MonoBehaviour {

    public MonsterListBehaviour monsterDefinition;


    public GameObject CreatePilotWithMonster(int playerID, int monsterID, int networkId)
    {
        string name = "Pilot" + playerID;
        GameObject pilot_go = new GameObject(name);//, typeof(components));

        // add controller and state
        BPilot pilotBehaviour = pilot_go.AddComponent<BPilot>();
        pilotBehaviour.state = pilot_go.AddComponent<BPilotState>();

        // add photon view
        PhotonView pv = pilot_go.AddComponent<PhotonView>();
        pv.viewID = networkId;
        pv.synchronization = ViewSynchronization.ReliableDeltaCompressed;
        //pv.onSerializeTransformOption = OnSerializeTransform.PositionAndRotation;

        // Photon serializer
        pilotBehaviour.network = pilot_go.AddComponent<BPilotNetwork>();
        pilotBehaviour.network.controller = pilotBehaviour;
        pilotBehaviour.network.updateSpeed = 4;
        pv.observed = pilotBehaviour.network;

        // add label
        Transform nameTag = Instantiate(monsterDefinition.nameTagPrefab, pilot_go.transform.position, pilot_go.transform.rotation) as Transform;
        nameTag.GetComponent<ObjectLabelB>().target = pilot_go.transform;
        nameTag.SetParent(pilot_go.transform);
        pilotBehaviour.nameTag = nameTag;

        // add monster details
        this.pilotGetIntoMonster(pilot_go, monsterID);

        return pilot_go;
    }

    public void pilotGetIntoMonster(GameObject pilot_go, int monsterID)
    {
        BPilot pilotBehaviour = pilot_go.GetComponent<BPilot>();
        pilotBehaviour.data = monsterDefinition.list[monsterID].GetComponent<MonsterData>();


        // add collider
        //CapsuleCollider col = pilot_go.AddComponent<CapsuleCollider>();
        //col.center = new Vector3(0, pilotBehaviour.data.size.z / 2, 0);
        //col.radius = pilotBehaviour.data.unitRadius / 2;

        BoxCollider col = pilot_go.AddComponent<BoxCollider>();
        BoxCollider colDef = monsterDefinition.list[monsterID].GetComponent<BoxCollider>();
        col.size = colDef.size;
        col.center = colDef.center;

        // add skills
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
