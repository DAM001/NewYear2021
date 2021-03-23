using UnityEngine;
using System.Collections;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _Cars;
    [SerializeField] private Transform[] _SpawnPoints;

    private void Start()
    {
        StartCoroutine(SpawnCars());
    }

    private IEnumerator SpawnCars()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1f, 5f));
            GameObject car = Instantiate(_Cars[Random.Range(0, _Cars.Length)], _SpawnPoints[Random.Range(0, _SpawnPoints.Length)]);
            car.transform.parent = null;
        }
    }
}
