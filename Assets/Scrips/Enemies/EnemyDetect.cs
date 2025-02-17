using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetect : MonoBehaviour
{
    public Collider collider_;

    private void OnBecameInvisible()
    {
        //Debug.LogError("OnBecameInvisible");
        collider_.enabled = false;
    }

    private void OnBecameVisible()
    {
       // Debug.LogError("OnBecameVisible");
        collider_.enabled = true;
    }


}
