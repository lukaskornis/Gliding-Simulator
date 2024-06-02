using System;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class Player : NetworkBehaviour
{
    private NetworkVariable<Vector2> turnDir = new(Vector2.zero,writePerm:NetworkVariableWritePermission.Owner);
    NetworkVariable< Vector3> pos = new(Vector3.zero,writePerm:NetworkVariableWritePermission.Owner);
    private NetworkVariable<FixedString32Bytes> name = new();
    private Glider glider;
    
    private void Start()
    {
        glider = GetComponent<Glider>();
        if (IsOwner)
        {
            Inputs.OnTurn.AddListener(glider.Turn);
            FlyCam.instance.target = transform;
        }
    }

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            print( "server spawned" );
            name.Value = $"Player {OwnerClientId}";
        }
        
        gameObject.name = name.Value.ToString();
        
        if (IsOwner)
        {
            transform.position = Vector3.up * 100f;
            Inputs.OnTurn.AddListener(dir =>
            {
                turnDir.Value = dir;
            });
        } else
        {
            turnDir.OnValueChanged += (_, value) => glider.Turn(value);
            transform.position = pos.Value;
        }
    }

    private void Update()
    {
        if (IsOwner)
        {
            pos.Value = transform.position;
        }
    }
}
