using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

public class NetConnector : MonoBehaviour
{
    UnityTransport transport;
    NetworkManager net;
    
    void Start()
    {
        transport = GetComponent<UnityTransport>();
        net = GetComponent<NetworkManager>();
        net.StartHost();
    }
    
    public void OnFoundLocalServer(NetDiscovery.DiscoveryInfo info)
    {
        transport.ConnectionData.Address = "127.0.0.1";
        transport.ConnectionData.Port = info.GetGameServerPort();
        
        print( "connecting to " + transport.ConnectionData.Address + ":" + transport.ConnectionData.Port );
        net.StartClient();
    }
}
