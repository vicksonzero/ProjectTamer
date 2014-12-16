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

    public void masterSaysStartGame(int main, int supp)
    {
        this.photonView.RPC("receiveMasterClientMonsterChoice", PhotonTargets.Others, main, supp);
        this.photonView.RPC("startGame", PhotonTargets.All);

    }

    [RPC]
    public void receiveMasterClientMonsterChoice(int main, int supp)
    {
        int[][] chosenMonsters = this.GetComponent<MonsterSelectControlBehaviour>().chosenMonsters;
        chosenMonsters[1][0] = chosenMonsters[0][0];
        chosenMonsters[1][1] = chosenMonsters[0][1];
        chosenMonsters[0][0] = main;
        chosenMonsters[0][1] = supp;
    }

    [RPC]
    public void startGame()
    {
        PhotonNetwork.LoadLevel("Battle2");
    }
}
