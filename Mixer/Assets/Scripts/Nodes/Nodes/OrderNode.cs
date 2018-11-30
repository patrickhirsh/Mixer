using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Represents a traversable position for customers during pathing.
/// Customers can place orders for the associated bartenderPosition at this node.
/// </summary>
public class OrderNode : Node
{
    public GameObject bartenderPosition;     // the bartenderPosition associated with this OrderNode

    /// <summary>
    /// Checks to see if this OrderNode has a bartenderPosition associated with it.
    /// OrderNodes should always have a bartenderPosition assigned to them in the Editor.
    /// </summary>
    public bool isLinked()
    {
        if (bartenderPosition != null)
            return true;

        return false;
    }
}
