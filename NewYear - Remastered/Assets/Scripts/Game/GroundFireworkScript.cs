using System.Collections;
using UnityEngine;

public class GroundFireworkScript : MonoBehaviour
{
    private GameObject _FireWork;

    private void Start()
    {
        StartCoroutine(Fire());
        _FireWork = transform.GetChild(0).gameObject;
    }

    private IEnumerator Fire()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(.7f, 2f));
            if (!GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().GetGameRunning())
            {
                _FireWork.GetComponent<WeaponScript>().FireWeapon();
            }
        }
    }
}
