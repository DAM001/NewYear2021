using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool _GameRunning;

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public bool GetGameRunning()
    {
        return _GameRunning;
    }
    public void SetGameRunning(bool game)
    {
        _GameRunning = game;
    }

    private bool _HighQuality = true;
    public void SetQuality()
    {
        _HighQuality = !_HighQuality;
        if (_HighQuality) QualitySettings.SetQualityLevel(1, true);
        else QualitySettings.SetQualityLevel(0, true);
    }

    public bool GetHighQuality()
    {
        return _HighQuality;
    }
}
