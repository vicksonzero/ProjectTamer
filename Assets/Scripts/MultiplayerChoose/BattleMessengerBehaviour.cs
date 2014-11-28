using UnityEngine;
using System.Collections;

public class BattleMessengerBehaviour : MonoBehaviour
{

    public OBattleMessage battleMessage;

    #region Unity events
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

	// Use this for initialization
    void Start()
    {
        this.battleMessage = new OBattleMessage();
	}
	
	// Update is called once per frame
	void Update () {

    }
    #endregion //Unity events

    #region public methods
    public void SetParam(OBattleMessage bm){
        this.battleMessage = bm;
    }
    #endregion public methods

} 


