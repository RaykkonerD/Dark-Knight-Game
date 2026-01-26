using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOvermenu;
    public GameObject gameVictoryMenu;
    public int currentPhase = 1;
    private string[] phasesNames = { "fase1", "fase2", "fase3" , "fasebonus"};

    public void Start(){
         Screen.SetResolution(Display.main.systemWidth, Display.main.systemHeight, FullScreenMode.FullScreenWindow);
    }

    public void GameOver()
    {
        gameOvermenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void GameVictory()
    {
        gameVictoryMenu.SetActive(true);
        Debug.Log("Victory!");
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
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
