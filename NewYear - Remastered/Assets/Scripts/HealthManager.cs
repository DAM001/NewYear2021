using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private float _MaxHealth = 100f;
    [SerializeField] private bool _Player;

    private float _CurrentHealth;
    private GameObject _HealthBar;

    private bool _Alive = true;

    private void Start()
    {
        _HealthBar = transform.GetChild(1).gameObject;
        RefillHealth();
    }

    public void Damage(float damage)
    {
        _CurrentHealth -= damage;
        _HealthBar.GetComponent<HealthBar>().SetHealthBarValue(_CurrentHealth, _MaxHealth);
        if (_CurrentHealth <= 0f) DieFunction();
    }

    private void DieFunction()
    {
        if (!_Player && _Alive)
        {
            GetComponent<EnemyScript>().DieFunction();
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<UiScript>().AddScore();
            _Alive = false;
        }
        else if (_Player)
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().SetGameRunning(false);
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<UiScript>().StartGame(false);
        }
    }

    public void RefillHealth()
    {
        _CurrentHealth = _MaxHealth;
        _HealthBar.GetComponent<HealthBar>().SetHealthBarValue(_CurrentHealth, _MaxHealth);
    }
}
