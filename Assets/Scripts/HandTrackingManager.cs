using UnityEngine;
using System.Collections.Generic;

public class HandTrackingManager : MonoBehaviour
{
    [SerializeField] private int rearCameraIndex = 0;
    [SerializeField] private Saber leftSaber;
    [SerializeField] private Saber rightSaber;
    private WebCamTexture webCamTexture;
    private bool handTrackingActive = false;
    private Vector3 leftHandPos;
    private Vector3 rightHandPos;

    void Start()
    {
        InitializeRearCamera();
    }

    void InitializeRearCamera()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        
        if (devices.Length == 0)
        {
            Debug.LogError("Nenhuma câmera encontrada!");
            return;
        }

        // Procura pela câmera traseira
        int cameraIndex = devices.Length - 1; // Geralmente a câmera traseira é a última
        
        webCamTexture = new WebCamTexture(devices[cameraIndex].name);
        webCamTexture.Play();
        handTrackingActive = true;
        Debug.Log($"Câmera traseira ativada: {devices[cameraIndex].name}");
    }

    void Update()
    {
        if (!handTrackingActive) return;

        // Rastreamento das mãos (simulado com input do acelerômetro)
        TrackHands();
        UpdateSaberPositions();
    }

    void TrackHands()
    {
        // Simulação de rastreamento das mãos usando acelerômetro e toque
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            
            // Mão esquerda (lado esquerdo da tela)
            if (touch.position.x < Screen.width * 0.5f)
            {
                leftHandPos = new Vector3(touch.position.x / Screen.width - 0.5f, 
                                        touch.position.y / Screen.height - 0.5f, 
                                        0.5f);
            }
            // Mão direita (lado direito da tela)
            else
            {
                rightHandPos = new Vector3(touch.position.x / Screen.width - 0.5f, 
                                         touch.position.y / Screen.height - 0.5f, 
                                         0.5f);
            }
        }

        // Adiciona movimento do acelerômetro
        Vector3 acceleration = Input.acceleration;
        leftHandPos += acceleration * Time.deltaTime * 0.1f;
        rightHandPos -= acceleration * Time.deltaTime * 0.1f;
    }

    void UpdateSaberPositions()
    {
        if (leftSaber != null)
            leftSaber.SetPosition(leftHandPos);

        if (rightSaber != null)
            rightSaber.SetPosition(rightHandPos);
    }

    public Vector3 GetLeftHandPosition() => leftHandPos;
    public Vector3 GetRightHandPosition() => rightHandPos;

    void OnDestroy()
    {
        if (webCamTexture != null)
            webCamTexture.Stop();
    }
}
