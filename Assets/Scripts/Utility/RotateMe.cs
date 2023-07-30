using UnityEngine;

public class RotateMe : MonoBehaviour
{
    public float rotationSpeed=10f;
    // Start is called before the first frame update
    void Update()
    {
        transform.Rotate(0f,Time.deltaTime* rotationSpeed, 0f, Space.Self);
    }

}
