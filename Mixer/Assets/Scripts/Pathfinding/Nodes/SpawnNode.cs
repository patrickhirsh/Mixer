using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Represents a traversable position for customers during pathing.
/// Customers can spawn and despawn from this node.
/// </summary>
public class SpawnNode : Node
{
    
    public enum SpawnType { spawn, despawn }
    public SpawnType spawnType;                     // indicates whether this is a spawn or despawn location
    public List<OrderNode> validOrderNodes;         // indicates which OrderNodes should be considered when spawning from this point
}
