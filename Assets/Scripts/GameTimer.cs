using UnityEngine;
using TMPro; // Use "using UnityEngine.UI;" se estiver usando Texto Legado

public class GameTimer : MonoBehaviour
{
    [Header("Configurações do Tempo")]
    public float timeRemaining = 60f; // Começa em 60 segundos
    public bool timerIsRunning = false;

    [Header("Referência da UI")]
    public TextMeshProUGUI timerText; // Arraste o objeto de texto aqui no Inspector
    public GameObject gameOverUI; 
    void Start()
    {
        // Inicia o timer assim que o jogo começa
        timerIsRunning = true;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                // Subtrai o tempo que passou desde o último quadro
                timeRemaining -= Time.deltaTime;
                UpdateTimerUI(timeRemaining);
            }
            else
            {
                // O tempo acabou!
                timeRemaining = 0;
                timerIsRunning = false;
                UpdateTimerUI(timeRemaining);
                
                TimerEnded(); // Chama a função de fim de tempo
            }
        }
    }

    // Função para formatar e mostrar o texto na tela
    void UpdateTimerUI(float currentTime)
    {
        // Arredonda para o número inteiro mais próximo (ex: 19.8 vira 19)
        int seconds = Mathf.FloorToInt(currentTime);
        
        // Atualiza o texto na tela
        timerText.text = seconds.ToString();
    }

    void TimerEnded()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0f; 
    
    }
}