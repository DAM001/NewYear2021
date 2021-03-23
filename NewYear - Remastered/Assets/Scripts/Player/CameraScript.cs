using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [Header("Preferences:")]
    [SerializeField] private float _SmoothSpeed = .1f;

    private GameObject _Target;

    private void Start()
    {
        _Target = GameObject.FindGameObjectWithTag("Player");
        transform.LookAt(_Target.transform);
    }

    private void FixedUpdate()
    {
        if (_Target != null)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_Target.transform.position - transform.position, Vector3.up), _SmoothSpeed);
            transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y / 1.2f, transform.rotation.z / 1.2f, 1f);
        }
    }
}