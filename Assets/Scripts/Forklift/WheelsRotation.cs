using UnityEngine;

public class WheelsRotation : MonoBehaviour
{
    [SerializeField]
    private Transform[] wheels = new Transform[4];
    [SerializeField]
    private Transform[] steerWheels = new Transform[2];
    private InputManager inputs;



    private void Start()
    {
        inputs = InputManager.Instance;
    }

    public void UpdateRotation(Vector3 relativeVelocity)
    {
        for (int i = 0; i < steerWheels.Length; i++)
        {
            steerWheels[i].localEulerAngles = new Vector3(0, inputs.RotateValue * 30, 0);
        }

        Vector3 rotationAmount = Vector3.right * (relativeVelocity.z * 1.6f * Time.deltaTime * Mathf.Rad2Deg);

        for (int i = 0; i < wheels.Length; i++)
        {
            wheels[i].Rotate(rotationAmount);
        }
    }
}
