using UnityEngine;
using System.Collections;

public class BPlayerController : MonoBehaviour {

    [Tooltip("These inputs will be deactivated on Start, except for activeInput")]
    public GameObject[] inputs;
    public int activeInput = 0;

	// Use this for initialization
	void Start () {
        this.disableAllInputs();
        this.inputs[this.activeInput].SetActive(true);
	}

    private void disableAllInputs()
    {
        foreach (GameObject input in this.inputs)
        {
            input.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    public void Move(int monsterID, Vector3 pos)
    {

    }
    public void SkillStart(int monsterID, int skillID)
    {

    }
    public void SkillStep(int monsterID, int skillID)
    {

    }
    public void SkillStop(int monsterID, int skillID)
    {

    }

}
