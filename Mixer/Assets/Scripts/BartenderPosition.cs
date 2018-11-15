using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BartenderPosition : MonoBehaviour
{
	public void removeCurrentOrder()
    {
        if (this.transform.childCount != 0)
            Destroy(this.transform.GetChild(0).gameObject);
    }
}
