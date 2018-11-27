using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNode : Node
{
    
    public enum SpawnType { spawn, despawn }
    public SpawnType spawnType;                     // indicates whether this is a spawn or despawn location
    public List<OrderNode> validOrderNodes;         // indicates which OrderNodes should be considered when spawning from this point
}
