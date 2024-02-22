using UnityEngine;
using FlutterUnityIntegration;
using System;

[assembly: System.Runtime.CompilerServices.InternalsVisibleToAttribute("Unity.UnityBridgeIntegration.Plugin")]

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


    public class UnityBridgeToGameListener //: MonoBehaviour
    {
        //public static UnityBridgeToGameListener Instance { get; private set; }
        //private void Awake()
        //{
        //    Instance = this;
        //}

        public static event Action<ScoreResToUnityDto> GotScoreResponse;
        public static event Action PlayAgainAction;
        public static event Action<int> EnableSoundAction;

        public static void UnityBridgeToGame_OnScoreUpdateResponse(string payload)
        {
            Debug.Log("UnityBridgeToGame_OnScoreUpdateResponse: recieved " + payload);
            ScoreResToUnityDto scoreResToUnityDto = ScoreResToUnityDto.ParsePayload(payload);
            GotScoreResponse?.Invoke(scoreResToUnityDto);
        }

        public static void UnityBridgeToGame_playAgain()
        {
            Debug.Log("UnityBridgeToGame_playAgain: recieved ");
            PlayAgainAction?.Invoke();
        }

        public static void UnityBridgeToGame_enableSound(int enable)
        {
            Debug.Log("UnityBridgeToGame_enableSound: recieved ");
            EnableSoundAction?.Invoke(enable);
        }


    }

}
