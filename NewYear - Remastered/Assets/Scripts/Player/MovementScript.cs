using UnityEngine;

public class MovementScript : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _MoveSpeed;
    [SerializeField] private float _RotationSpeed;

    private Rigidbody _Rigidbody;

    private void Start()
    {
        _Rigidbody = GetComponent<Rigidbody>();
        _Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    private void FixedUpdate()
    {
        if (GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().GetGameRunning())
        {
            if (Input.GetKey(KeyCode.W)) PlayerMove(Vector3.forward);
            if (Input.GetKey(KeyCode.S)) PlayerMove(-Vector3.forward);
            if (Input.GetKey(KeyCode.D)) PlayerMove(Vector3.right);
            if (Input.GetKey(KeyCode.A)) PlayerMove(-Vector3.right);

            PlayerLook();
        }
    }

    private void PlayerMove(Vector3 direction)
    {
        _Rigidbody.AddForce(direction * _MoveSpeed);
    }

    public void PlayerLook()
    {
        Vector3 localTarget = transform.InverseTransformPoint(GameObject.FindGameObjectWithTag("Cursor").transform.position);

        _Rigidbody.AddTorque(Vector3.up * _RotationSpeed * localTarget.x);
        if (localTarget.x < 1 || localTarget.x > -1) _Rigidbody.angularVelocity = Vector3.zero;
    }
}
