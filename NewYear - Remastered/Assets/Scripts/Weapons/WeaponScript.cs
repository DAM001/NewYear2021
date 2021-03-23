using UnityEngine;
using UnityEngine.UIElements;

public class WeaponScript : MonoBehaviour
{
    //public variables
    [Header("Bullet:")]
    [SerializeField] private GameObject _Bullet;

    [Header("Weapon data:")]
    [SerializeField] private float _FireRate = .1f;
    [SerializeField] private float _Accuracy = 5f;

    //private variables
    private GameObject _FirePoint;
    private float nextTimeFire = 0f;
    private Transform _WeaponFolder;


    private void Start() //start function
    {
        _FirePoint = transform.GetChild(0).gameObject;

        _WeaponFolder = transform.parent;
        transform.parent = null;
    }

    private void FixedUpdate()
    {
        if (_WeaponFolder != null)
        {
            transform.position = Vector3.Lerp(transform.position, _WeaponFolder.position, Time.deltaTime * 50f);
            transform.rotation = Quaternion.Lerp(transform.rotation, _WeaponFolder.rotation, Time.deltaTime * 10f);
        }
        else Destroy(gameObject);
    }

    public void FireWeapon() //fire weapon (pull the trigger)
    {
        if (Time.time >= nextTimeFire) //fire method
        {
            nextTimeFire = Time.time + _FireRate;
            FireFunction();
        }
    }

    private void FireFunction() //create bullet and fire weapon
    {
        FireAccuracy(_Accuracy);
        var bullet = Instantiate(_Bullet, _FirePoint.transform);
        bullet.transform.SetParent(null);

        //fire effects
    }

    private void FireAccuracy(float accuracy) //calculate next fire impact position
    {
        _FirePoint.transform.rotation = transform.rotation; //reset rotation
        _FirePoint.transform.Rotate(0f, Random.Range(-accuracy, accuracy), 0f, Space.Self); //calculate next random shoot impact position
    }
}
