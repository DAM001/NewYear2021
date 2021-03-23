using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class BulletScript : MonoBehaviour
{
    [Header("Parameters:")]
    [SerializeField] private float _Speed = 30f;
    [SerializeField] private float[] _FlyTime = {.4f, .6f };
    [Header("Effects:")]
    [SerializeField] private GameObject _ImpactEffect;
    [SerializeField] private float _FlyEffectPower;
    [SerializeField] private AudioClip _Explotion;
    [Header("Materials:")]
    [SerializeField] private Material[] _HeadMaterial;
    [SerializeField] private Material[] _TrailMaterial;
    [SerializeField] private Material[] _ExplotionMaterial;

    private float _CurrentFlyEffectPower = 0f;

    private AudioSource _Audio;


    private void Start() //start function | destroy
    {
        StartCoroutine(DestroyBulletTimer());

        _Speed *= Random.Range(.7f, 1.3f);
        _Audio = transform.GetChild(2).GetComponent<AudioSource>();
        _Audio.pitch = Random.Range(.8f, 1.2f);
        _Audio.volume = Random.Range(.5f, 1f);

        transform.GetChild(0).GetChild(0).GetComponent<ParticleSystemRenderer>().material = _HeadMaterial[Random.Range(0, _HeadMaterial.Length)];
        transform.GetChild(0).GetChild(1).GetComponent<ParticleSystemRenderer>().material = _TrailMaterial[Random.Range(0, _TrailMaterial.Length)];
        transform.GetChild(0).GetChild(2).GetComponent<ParticleSystemRenderer>().material = _HeadMaterial[Random.Range(0, _HeadMaterial.Length)];
    }

    private void FixedUpdate() //fixedUpdate function | move bullet
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * _Speed);

        _CurrentFlyEffectPower += Random.Range(-_FlyEffectPower, _FlyEffectPower) / 5f;
        if (_CurrentFlyEffectPower > _FlyEffectPower) _CurrentFlyEffectPower = _FlyEffectPower;
        else if (_CurrentFlyEffectPower < -_FlyEffectPower) _CurrentFlyEffectPower = -_FlyEffectPower;
        GetComponent<Rigidbody>().AddForce(transform.right * _CurrentFlyEffectPower);
    }

    private IEnumerator DestroyBulletTimer()
    {
        yield return new WaitForSeconds(Random.Range(_FlyTime[0], _FlyTime[1]));
        DestroyBullet();
    }

    private void DestroyBullet()
    {
        Destroy(_Audio.gameObject, 3f);
        Destroy(transform.GetChild(0).GetChild(1).gameObject, 2f);
        transform.GetChild(0).GetChild(1).parent = null;

        PlayAudio();
        PlayEffect();
        ExplotionForce();

        Destroy(gameObject);
    }

    private void PlayAudio()
    {
        _Audio.clip = _Explotion;
        _Audio.Play();
        _Audio.transform.parent = null;
    }

    private void PlayEffect()
    {
        GameObject impactEffect = Instantiate(_ImpactEffect, transform);
        impactEffect.transform.Rotate(0f, 180f, 0f);
        impactEffect.transform.parent = null;
        impactEffect.GetComponent<ParticleSystemRenderer>().material = _ExplotionMaterial[Random.Range(0, _ExplotionMaterial.Length)];
        Destroy(impactEffect, 5f);
    }

    private void ExplotionForce()
    {
        float radius = 10f;
        float power = 2000f;

        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null && rb.gameObject.tag != "Bullet") rb.AddExplosionForce(power, transform.position, radius, 3f);

            if (hit.gameObject.tag == "Enemy" || hit.gameObject.tag == "Player")
            {
                float damageDistance = 5f;
                float distance = Vector3.Distance(transform.position, hit.transform.position);
                if (distance < damageDistance)
                {
                    distance = 1f - (distance / damageDistance);
                    hit.gameObject.GetComponent<HealthManager>().Damage(distance * 50f);
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision) //bullet impact
    {
        //if (collision.gameObject.GetComponent<Rigidbody>() != null) collision.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * _ImpactForce);
        _Speed /= .95f;
        _FlyEffectPower /= .95f;
    }
}
