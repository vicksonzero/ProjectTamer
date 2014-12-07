using UnityEngine;
using System.Collections;

public class IconSkillBehaviour : MonoBehaviour {

    public Transform aRController;
    public int skillID;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {  
	
	}

    void OnMouseDown()
    {
        this.aRController.SendMessage("OnIconSkillDown", this.skillID);
    }
    void OnMouseDrag()
    {
        this.aRController.SendMessage("OnIconSkillDrag", this.skillID);
    }
    void OnMouseUp()
    {
        this.aRController.SendMessage("OnIconSkillUp", this.skillID);
    }
}
