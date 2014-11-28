using UnityEngine;
using System.Collections;

public class IconMoveToBehaviour : MonoBehaviour {

    public Transform aRController;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {  
	
	}
    void OnMouseDrag()
    {
        this.aRController.SendMessage("OnIconMoveToDrag");
    }
}
