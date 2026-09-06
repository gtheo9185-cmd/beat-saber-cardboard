using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private HandTrackingManager handTrackingManager;
    [SerializeField] private Saber leftSaber;
    [SerializeField] private Saber rightSaber;
    [SerializeField] private BlockSpawner blockSpawner;
    [SerializeField] private AudioClip musicClip;
    [SerializeField] private Transform cameraTransform;
    
    private AudioSource audioSource;
    private int score = 0;
    private bool gameOver = false;
    private float gameTime = 0f;

    void Start()
    {
        InitializeGame();
    }

    void InitializeGame()
    {
        // Configura o fundo preto
        Camera.main.backgroundColor = Color.black;
        
        // Inicia a música
        audioSource = gameObject.AddComponent<AudioSource>();
        if (musicClip != null)
        {
            audioSource.clip = musicClip;
            audioSource.Play();
        }
        
        // Configura a câmera para Cardboard
        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;
        
        SetupCardboard();
        
        Debug.Log("Jogo iniciado! Use a câmera traseira para controlar os sabres.");
    }

    void SetupCardboard()
    {
        Camera.main.nearClipPlane = 0.1f;
        Camera.main.farClipPlane = 1000f;
        
        // Configura rotação para usar giroscópio
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
            Debug.Log("Giroscópio ativado");
        }
    }

    void Update()
    {
        if (gameOver) return;

        gameTime += Time.deltaTime;
        
        // Verifica se ambos os sabres foram destruídos
        if (!leftSaber.IsActive() && !rightSaber.IsActive())
        {
            EndGame();
        }
        
        // Atualiza pontuação
        UpdateScore();
        
        // Debug: pressione ESC para resetar
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void UpdateScore()
    {
        score = blockSpawner.GetScore();
    }

    void EndGame()
    {
        gameOver = true;
        audioSource.Stop();
        
        Debug.Log($"GAME OVER! Pontuação final: {score}");
        Debug.Log($"Tempo de jogo: {gameTime:F1} segundos");
        
        // Mostra UI de Game Over
        ShowGameOverUI();
    }

    void ShowGameOverUI()
    {
        GameObject canvasObject = new GameObject("GameOverCanvas");
        Canvas canvas = canvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        
        // Cria texto de Game Over
        GameObject textObject = new GameObject("GameOverText");
        textObject.transform.SetParent(canvasObject.transform);
        
        Text text = textObject.AddComponent<Text>();
        text.text = $"GAME OVER\nPontuação: {score}\nTempo: {gameTime:F1}s";
        text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        text.fontSize = 40;
        text.fontStyle = FontStyle.Bold;
        text.alignment = TextAnchor.MiddleCenter;
        text.color = Color.white;
        
        RectTransform rectTransform = textObject.GetComponent<RectTransform>();
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;
    }

    public int GetScore() => score;
    public float GetGameTime() => gameTime;
    public bool IsGameOver() => gameOver;
}
