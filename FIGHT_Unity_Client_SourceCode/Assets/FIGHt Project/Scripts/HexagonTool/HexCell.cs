using UnityEngine;
using System.Collections;

public class HexCell : MonoBehaviour
{

    void OnTriggerStay(Collider other)
    {
        Debug.Log("trigger");
        DestroyObject(gameObject);
    }

}
