using UnityEngine;

public class HumanScript : MonoBehaviour
{
    [SerializeField] private bool _Player;

    private GameObject _Weapon;

    private void Start()
    {
        _Weapon = transform.GetChild(0).GetChild(2).GetChild(0).gameObject;
    }

    private void Update()
    {
        if (_Player && GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().GetGameRunning())
        {
            if (Input.GetKey(KeyCode.Mouse0)) FireWeapon();
        }
    }

    public void FireWeapon()
    {
        _Weapon.GetComponent<WeaponScript>().FireWeapon();
    }
}
