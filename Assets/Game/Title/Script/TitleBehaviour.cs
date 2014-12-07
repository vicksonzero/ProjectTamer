using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TitleBehaviour : MonoBehaviour {



    public Graphic title;
    public float titleFadingDuration = 3;
    private bool titleFading = false;

    public Text startTip;
    public float startTipFadingDuration = 3;
    private bool startTipFading = false;

    public UnityEngine.UI.Image fadePanel;
    public float fadePanelDuration = 3;
    private bool fadePanelFading = false;

    private float startTime = 0;
    private float elapsedTime = 0;

	// Use this for initialization
    void Start()
    {
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void StartGame()
    {
        this.doStartGame();
    }

    private void doStartGame(){
        Application.LoadLevel("MultiplayerLobby");

    }

}
