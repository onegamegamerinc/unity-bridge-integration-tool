using UnityEngine;
using FlutterUnityIntegration;
using System;

namespace UnityBridgeIntegration
{
    /**
     * Usage:
     public class GamePlayPanel : MonoBehaviour
     {
        private void OnEnable()
        {
            UnityBridgeToGameListener.Instance.GotScoreResponse +=LevelManagerOnGotScoreResponse;
        }
        private void OnDisable()
        {
            UnityBridgeToGameListener.Instance.GotScoreResponse -= LevelManagerOnGotScoreResponse;
        }
        private void LevelManagerOnGotScoreResponse(ScoreResToUnityDto scoreResToUnityDto)
        {

        }
      }
     * 
     * 
     */


    public class UnityBridgeToGameListener : MonoBehaviour
    {
        public static UnityBridgeToGameListener Instance { get; private set; }
        public static event Action<ScoreResToUnityDto> GotScoreResponse;

        private void Awake()
        {
            Instance = this;
        }

        public void UnityBridgeToGame_OnScoreUpdateResponse(string payload)
        {
            Debug.Log("UnityBridgeToGame_OnScoreUpdateResponse: recieved " + payload);
            ScoreResToUnityDto scoreResToUnityDto = ScoreResToUnityDto.ParsePayload(payload);
            GotScoreResponse?.Invoke(scoreResToUnityDto);
        }
    }

}
