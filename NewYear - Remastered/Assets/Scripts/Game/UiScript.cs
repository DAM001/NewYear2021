using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UiScript : MonoBehaviour
{
    [SerializeField] private Text _Counter;
    [SerializeField] private Text _QualityText;
    private int _Seconds;

    private void Start()
    {
        StartCoroutine(Counter());
    }
    private IEnumerator Counter()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            CounterText();
        }
    }
    private void CounterText()
    {
        _Seconds++;

        int minutes = _Seconds / 60;
        int seconds = _Seconds - (minutes * 60);
        if (minutes < 10) _Counter.text = "0" + minutes + ":";
        else _Counter.text = minutes + ":";
        if (seconds < 10) _Counter.text += "0" + seconds;
        else _Counter.text += seconds;
    }


    [Space(10)]
    [SerializeField] private GameObject _CameraFolder;
    [SerializeField] private GameObject _StartScreen;
    private bool _GameRunning = false;

    private void Update()
    {
        if (!_GameRunning && Input.GetKeyDown(KeyCode.Space)) StartGame(true);
        if (Input.GetKeyDown(KeyCode.K))
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().SetQuality();
            if (GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().GetHighQuality()) _QualityText.text = "High";
            else _QualityText.text = "Low";
        }
    }
    public void StartGame(bool start)
    {
        _GameRunning = start;

        _CameraFolder.GetComponent<Animator>().SetBool("GameRunning", start);
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().SetGameRunning(start);
        _StartScreen.SetActive(!start);
        if (start)
        {
            GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(0f, 0f, -2f);
            GameObject.FindGameObjectWithTag("Player").GetComponent<HealthManager>().RefillHealth();

            _Score = -1;
            AddScore();
        }
    }


    [Space(10)]
    [SerializeField] private Text _ScoreText;
    private int _Score = 0;

    public void AddScore()
    {
        _Score++;
        _ScoreText.text = "Score: " + _Score;
    }
}
