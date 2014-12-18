using UnityEngine;
using System.Collections;

public class BPilotNetwork : Photon.MonoBehaviour {

    public float updateSpeed = 1;
    [HideInInspector]
    public BPilot controller;

    public Vector3 networkPosition;
    public Quaternion networkRotation;

	// Use this for initialization
	void Start () {
        this.networkPosition = this.transform.position;
        this.networkRotation = this.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
	    //PhotonNetworkingMessage
        if (! PhotonNetwork.isMasterClient)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, networkPosition, Time.deltaTime * this.updateSpeed);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, networkRotation, Time.deltaTime * this.updateSpeed);
        }
	}

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            print("serialize write");
            stream.SendNext(this.transform.position);
            stream.SendNext(this.transform.rotation);
            stream.SendNext(this.controller.state.hp);
        }
        else
        {
            print("serialize read");
            this.networkPosition = (Vector3)stream.ReceiveNext();
            this.networkRotation = (Quaternion)stream.ReceiveNext();
            this.controller.state.hp = (float)stream.ReceiveNext();

        }

    }
}
