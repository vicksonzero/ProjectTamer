using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MonsterSelectControlBehaviour))]
public class MonsterSelectNetworkBehaviour : Photon.MonoBehaviour {

    public void clientReady(int main, int supp)
    {

        this.photonView.RPC("receiveClientMonsterChoice",PhotonTargets.MasterClient, main, supp);

    }

    [RPC]
    public void receiveClientMonsterChoice(int main, int supp)
    {
        this.GetComponent<MonsterSelectControlBehaviour>().chosenMonsters[1][0] = main;
        this.GetComponent<MonsterSelectControlBehaviour>().chosenMonsters[1][1] = supp;
        this.GetComponent<MonsterSelectControlBehaviour>().setClientIsReady(true);
    }

    public void masterSaysStartGame()
    {
        this.photonView.RPC("startGame", PhotonTargets.All);

    }

    [RPC]
    public void startGame()
    {
        PhotonNetwork.LoadLevel("Battle2");


    }
}
