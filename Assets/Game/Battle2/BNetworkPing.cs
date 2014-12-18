using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BNetworkPing : MonoBehaviour {

    public Text label;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.label.text = "ping: " + PhotonNetwork.networkingPeer.RoundTripTime.ToString();
	}
}
