using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private BlockSpawner blockSpawner;
    private Canvas canvas;
    private Text scoreText;
    private Text timeText;

    void Start()
    {
        InitializeUI();
    }

    void InitializeUI()
    {
        // Cria Canvas
        GameObject canvasObject = new GameObject("HUD");
        canvas = canvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        
        CanvasScaler scaler = canvasObject.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);
        
        // Cria texto de pontuação
        GameObject scoreObject = new GameObject("ScoreText");
        scoreObject.transform.SetParent(canvasObject.transform);
        scoreText = scoreObject.AddComponent<Text>();
        scoreText.text = "Pontuação: 0";
        scoreText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        scoreText.fontSize = 30;
        scoreText.color = Color.white;
        scoreText.alignment = TextAnchor.UpperLeft;
        
        RectTransform scoreRect = scoreObject.GetComponent<RectTransform>();
        scoreRect.anchoredPosition = new Vector2(10, -10);
        scoreRect.sizeDelta = new Vector2(300, 50);
        scoreRect.anchorMin = new Vector2(0, 1);
        scoreRect.anchorMax = new Vector2(0, 1);
        
        // Cria texto de tempo
        GameObject timeObject = new GameObject("TimeText");
        timeObject.transform.SetParent(canvasObject.transform);
        timeText = timeObject.AddComponent<Text>();
        timeText.text = "Tempo: 0s";
        timeText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        timeText.fontSize = 30;
        timeText.color = Color.white;
        timeText.alignment = TextAnchor.UpperRight;
        
        RectTransform timeRect = timeObject.GetComponent<RectTransform>();
        timeRect.anchoredPosition = new Vector2(-10, -10);
        timeRect.sizeDelta = new Vector2(300, 50);
        timeRect.anchorMin = new Vector2(1, 1);
        timeRect.anchorMax = new Vector2(1, 1);
        
        // Instrução de controle
        GameObject instructionObject = new GameObject("InstructionText");
        instructionObject.transform.SetParent(canvasObject.transform);
        Text instructionText = instructionObject.AddComponent<Text>();
        instructionText.text = "🎮 BEAT SABER - CARDBOARD\n\n" +
                               "📷 Use câmera traseira para hand tracking\n" +
                               "🔴 Sabre Vermelho = Blocos Vermelhos\n" +
                               "🔵 Sabre Azul = Blocos Azuis\n" +
                               "⚫ Bloco Preto = Destrói o Sabre\n\n" +
                               "Posicione suas mãos para controlar os sabres!";
        instructionText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        instructionText.fontSize = 20;
        instructionText.color = Color.white;
        instructionText.alignment = TextAnchor.MiddleCenter;
        
        RectTransform instructionRect = instructionObject.GetComponent<RectTransform>();
        instructionRect.anchoredPosition = Vector2.zero;
        instructionRect.sizeDelta = new Vector2(Screen.width, 400);
    }

    void Update()
    {
        if (gameManager != null && blockSpawner != null)
        {
            scoreText.text = $"Pontuação: {gameManager.GetScore()}";
            timeText.text = $"Tempo: {gameManager.GetGameTime():F1}s";
        }
    }
}
