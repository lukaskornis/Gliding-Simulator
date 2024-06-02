using Unity.Netcode;
using UnityEngine;

public class NetPlayer : NetworkBehaviour
{
    // rename to net id
    public Player playerPrefab;
    //public NetworkVariable<Player> player;

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            print( "server spawned" );
            //player.Value = Instantiate(playerPrefab);
            //player.Value.GetComponent<NetworkObject>().Spawn();
        }
        
        if (IsLocalPlayer)
        {
            print( "local player spawned" );
            Inputs.OnTurn.AddListener(SendInputsServerRPC);
        }
    }


    [Rpc(SendTo.Server)]
    void SendInputsServerRPC(Vector2 turnDir)
    {
        SendInputsClientRPC( turnDir );
    }
    
    [Rpc(SendTo.Everyone)]
    void SendInputsClientRPC(Vector2 turnDir)
    {
        //player.Value.GetComponent<Glider>().Turn(turnDir);
    }
}
