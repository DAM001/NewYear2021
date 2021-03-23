using UnityEngine;

public class CarScript : MonoBehaviour
{
    [SerializeField] private float _Speed;
    [SerializeField] private float _DespawnDistance = 50f;
    [Header("Damage")]
    [SerializeField] private float _Damage = 50f;

    private void Start()
    {
        _Speed *= Random.Range(.9f, 1.1f);
    }

    private void FixedUpdate()
    {
        transform.position += transform.forward * _Speed;

        if (transform.position.x > _DespawnDistance || transform.position.x < -_DespawnDistance) Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.tag == "Enemy" || other.transform.root.tag == "Player") 
            other.gameObject.GetComponent<HealthManager>().Damage(_Damage);
    }
}
