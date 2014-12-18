using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BEndGame : MonoBehaviour {

    public Canvas endGameCanvas;

	// Use this for initialization
	void Start () {
	    this.endGameCanvas.gameObject.SetActive(false);
	    //this.endGameCanvas.enabled=false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void EndGameScreen(string msg){
	    this.endGameCanvas.gameObject.SetActive(true);
        this.endGameCanvas.transform.Find("EndGameText").GetComponent<Text>().text = msg;
    }
}
