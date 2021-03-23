using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyScript : MonoBehaviour
{
    [Header("Movement:")]
    [SerializeField] private float _MoveSpeed;
    [SerializeField] private float _RotationSpeed;
    [SerializeField] private float _AttackDistance;

    private NavMeshAgent _NavMeshAgent;
    private Transform _Player;
    private bool _Attack;
    private bool _Alive = true;

    private Transform _StartPoint;

    private void Start()
    {
        _MoveSpeed *= Random.Range(.8f, 1.2f);
        _RotationSpeed *= Random.Range(.8f, 1.2f);
        _AttackDistance *= Random.Range(.8f, 1.2f);

        _StartPoint = transform.parent;
        transform.parent = null;

        _NavMeshAgent = GetComponent<NavMeshAgent>();
        _Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        float distance = Vector3.Distance(transform.position, _Player.position);

        if (_Alive && GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().GetGameRunning())
        {
            if (distance < _AttackDistance) Movement(.1f);
            else Movement(1f);
            LookTarget(1f);

            RaycastHit hit;
            Physics.Raycast(transform.position + transform.up * 1.8f, transform.forward, out hit, _AttackDistance);
            if (hit.transform != null && hit.transform.root == _Player.transform)
            {
                if (!_Attack)
                {
                    transform.GetChild(0).GetComponent<HumanScript>().FireWeapon();
                    StartCoroutine(Attack());
                }
            }
        }
        else
        {
            DieFunction();

            Movement(2f);
            LookTarget(1.5f);

            if (distance < 1f) Destroy(gameObject);
        }
    }
    private IEnumerator Attack()
    {
        _Attack = true;
        yield return new WaitForSeconds(Random.Range(.5f, 1f));
        if (_Alive)
        {
            //transform.GetChild(0).GetComponent<HumanScript>().NextFire();
            _Attack = false;
        }
    }

    private void Movement(float modValue)
    {
        _NavMeshAgent.SetDestination(_Player.transform.position);
        _NavMeshAgent.speed = _MoveSpeed * modValue;
    }
    private void LookTarget(float modValue)
    {
        Quaternion targetRot = Quaternion.LookRotation(_Player.transform.position - transform.position, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, _RotationSpeed * modValue * Time.deltaTime);
    }

    public void DieFunction()
    {
        _Player = _StartPoint;
        _AttackDistance = 0f;

        _Alive = false;
    }
}
