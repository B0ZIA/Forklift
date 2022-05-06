using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    public Action<Transform> OnCollison;
    private List<Transform> collidedObjects = new List<Transform>();



    public bool IsInCollision(Transform _collided)
    {
        return collidedObjects.Find(p => p == _collided);
    }

    public void Restart()
    {
        collidedObjects.Clear();
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.transform.CompareTag("Box") && collidedObjects.Find(p => p == _other) == null)
        {
            Transform collidedObject = _other.transform;
            collidedObjects.Add(collidedObject);
            OnCollison.Invoke(collidedObject);
        }
    }

    private void OnTriggerExit(Collider _other)
    {
        if (_other.transform.CompareTag("Box"))
        {
            Transform collidedObject = _other.transform;
            if (collidedObjects.Find(p => p == collidedObject))
            {
                collidedObjects.Remove(collidedObject);
            }
        }
    }
}
