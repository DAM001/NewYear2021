using System.Collections;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    [SerializeField] private GameObject _Soldier;
    [SerializeField] private float _SpawnInterval;
    [SerializeField] private int _NumberOfSoldiers;

    private GameObject[] _Soldiers;

    private void Start()
    {
        _Soldiers = new GameObject[_NumberOfSoldiers];
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(_SpawnInterval);
            if (GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().GetGameRunning())
            {
                for (int i = 0; i < _NumberOfSoldiers; i++)
                {
                    if (_Soldiers[i] == null)
                    {
                        _Soldiers[i] = CreateSoldier();
                        break;
                    }
                }
            }
        }
    }

    public GameObject CreateSoldier()
    {
        GameObject returnValue = Instantiate(_Soldier, transform);
        return returnValue;
    }
}
