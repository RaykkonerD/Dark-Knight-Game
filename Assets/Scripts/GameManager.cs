using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOvermenu;
    public int currentPhase = 1;
    private string[] phasesNames = { "fase1", "fase2", "fase3" };

    public void Start(){
    }

    public void GameOver()
    {
        gameOvermenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResetGame()
    {
        Time.timeScale = 1f;
        currentPhase = 1;
        SceneManager.LoadScene(phasesNames[0]);
    }

    public void NextPhase()
    {
        Debug.Log(phasesNames[currentPhase]);
        SceneManager.LoadScene(phasesNames[currentPhase]);
        currentPhase++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
