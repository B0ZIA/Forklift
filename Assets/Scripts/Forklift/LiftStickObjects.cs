using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LiftStickObjects : MonoBehaviour
{
    [SerializeField]
    private float maxObjectMassToLift = 10;
    private CollisionDetector[] detectors;
    private LiftController lift;
    private List<StickedObject> stickedObjects = new List<StickedObject>();


    private void Awake()
    {
        lift = GetComponent<LiftController>();
        lift.OnForceDrop += CheckStickedObject;

        detectors = GetComponentsInChildren<CollisionDetector>();
        for (int i = 0; i < detectors.Length; i++)
            detectors[i].OnCollison += CheckCollision;
    }

    private void CheckCollision(Transform _collided)
    {
        _collided.TryGetComponent(out Rigidbody rb);
        if(rb != null && rb.mass <= maxObjectMassToLift)
        {
            if (detectors.All(d => d.IsInCollision(_collided)) && !stickedObjects.Find(p => p.transform == _collided).transform)
            {
                StickObject(_collided);
            }
        }
    }

    private void CheckStickedObject()
    {
        foreach (var sticked in stickedObjects)
        {
            DropObject(sticked);
        }
        stickedObjects.Clear();
        foreach (var detector in detectors)
        {
            detector.Restart();
        }
    }

    private void StickObject(Transform _object)
    {
        _object.Translate(Vector3.forward * 0.03f, transform);
        var objectRigidbody = _object.GetComponent<Rigidbody>();
        stickedObjects.Add(new StickedObject(_object, objectRigidbody.mass));
        _object.parent = transform;
        Destroy(objectRigidbody);
    }

    private void DropObject(StickedObject _object)
    {
        _object.transform.parent = null;
        var rb = _object.transform.gameObject.AddComponent<Rigidbody>();
        rb.mass = _object.mass;
    }

    public struct StickedObject
    {
        public Transform transform;
        public float mass;

        public StickedObject(Transform _transform, float _mass)
        {
            transform = _transform;
            mass = _mass;
        }
    }
}
