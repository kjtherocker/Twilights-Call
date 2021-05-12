using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverworldCamera : MonoBehaviour
{
    public bool PreloadScene = false;
    public OverWorldPlayer m_OverWorldPlayer;

    public GameObject m_CameraLookAt;
    
    private bool m_CameraRotation = false;

    private Vector2 m_CameraRotationDirection;
    private float m_CameraSpeed = Constants.Helpers.m_OverworldCameraSpeed;
    private float m_DistanceAwayFromPlayer = 4;
    private float m_CameraFollowPlayerSpeed = 8;
    
    
    void Start()
    {

#if UNITY_EDITOR

        if (PreloadScene == true)
        {
            SceneManager.LoadScene("PreloadScene", LoadSceneMode.Additive);
        }
#endif
        StartCoroutine(OverWorldInitialization());
    }

    public void LateUpdate()
    {
        transform.LookAt(m_CameraLookAt.gameObject.transform);

        if (Vector3.Distance(transform.position, m_CameraLookAt.transform.position) > m_DistanceAwayFromPlayer)
        {
            transform.position = Vector3.MoveTowards(transform.position, m_OverWorldPlayer.transform.position, m_CameraFollowPlayerSpeed * Time.deltaTime);
        }
        
        if (m_CameraRotation)
        {
            transform.Translate(m_CameraRotationDirection * m_CameraSpeed * Time.deltaTime);
        }
    }
    
    public void CameraMovement(Vector2 aDirection)
    {
        m_CameraRotation = true;
        m_CameraRotationDirection = aDirection;
    }

    
    public void StopCameraMovement()
    {
        m_CameraRotation = false;
    }

    public IEnumerator OverWorldInitialization()
    {
        
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() => Preloader.Instance.m_InitializationSteps == Preloader.InitializationSteps.Finished);
 
        InputManager.Instance.m_BaseMovementControls.Player.RightStick.performed += movement => CameraMovement(movement.ReadValue<Vector2>());
        InputManager.Instance.m_BaseMovementControls.Player.RightStick.canceled += movement => StopCameraMovement();
        InputManager.Instance.m_BaseMovementControls.Enable();
        
       // UiManager.Instance.PushScreen(UiManager.Screen.ArenaMenu);
        m_OverWorldPlayer.SetupOverWorldPlayer();

    }

}
