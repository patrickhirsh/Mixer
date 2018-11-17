using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNode : Node
{
    // indicates whether this is a spawn or despawn location
    public enum SpawnType { spawn, despawn }
    public SpawnType spawnType;
}
