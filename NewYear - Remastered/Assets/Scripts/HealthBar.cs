using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _HealthBar;

    private GameObject _Camera;

    private void Start()
    {
        _Camera = Camera.main.gameObject;
    }

    private void Update()
    {
        transform.GetChild(0).rotation = _Camera.transform.rotation;
    }

    public void SetHealthBarValue(float currentValue, float maxValue)
    {
        _HealthBar.GetComponent<RectTransform>().sizeDelta = new Vector2((currentValue / maxValue) * _HealthBar.transform.parent.GetComponent<RectTransform>().sizeDelta.x, _HealthBar.transform.parent.GetComponent<RectTransform>().sizeDelta.y);
    }
}
