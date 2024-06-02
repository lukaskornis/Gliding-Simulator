using System.Net;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

public class NetConnector : MonoBehaviour
{
    UnityTransport transport;
    NetworkManager net;
    ExampleNetworkDiscovery discovery;
    public bool autoStartServer;
    
    void Start()
    {
        transport = GetComponent<UnityTransport>();
        net = GetComponent<NetworkManager>();
        discovery = GetComponent<ExampleNetworkDiscovery>();
        discovery.OnServerFound.AddListener( OnFoundServer );
        
        if ( ParrelSync.ClonesManager.IsClone())
        {
            net.StartHost();
        }
        else
        {
            discovery.StartClient();
            discovery.ClientBroadcast( new DiscoveryBroadcastData() );
        }
    }
    
    public void OnFoundServer( IPEndPoint ip, DiscoveryResponseData info )
    {
        transport.ConnectionData.Address = ip.Address.ToString();
        transport.ConnectionData.Port = info.Port;
        
        print( "discovery : connecting to " + transport.ConnectionData.Address + ":" + transport.ConnectionData.Port );
        net.StartClient();
    }
}
