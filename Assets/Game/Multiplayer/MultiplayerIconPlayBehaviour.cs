using UnityEngine;
using System.Collections;

public class MultiplayerIconPlayBehaviour : MonoBehaviour
{
    public MonsterSelectControlBehaviour roomChangingMessenger;

    #region Unity Events
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnMouseUp()
    {
        Application.LoadLevel("Battle");
    }
    #endregion //Unity Events

    //private void 

}
