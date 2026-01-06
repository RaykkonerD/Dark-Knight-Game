using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject gameOvermenu;

    public void Start(){
    }

    public void GameOver()
    {
        gameOvermenu.SetActive(true);
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
