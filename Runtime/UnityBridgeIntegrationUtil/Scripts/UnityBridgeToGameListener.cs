using UnityEngine;
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
        public static event Action ExitGameRequestScoreAction;
        public static event Action<int> EnableSoundAction;


        public static void OnExitGameRequestScore()
        {
            Debug.Log("[UnityBridgeToGameListener.OnExitGameRequestScore] recieved");
            ExitGameRequestScoreAction?.Invoke();
        }

        public static void OnScoreUpdateResponse(string payload)
        {
            Debug.Log("[UnityBridgeToGameListener.OnScoreUpdateResponse] recieved payload=" + payload);
            ScoreResToUnityDto scoreResToUnityDto = ScoreResToUnityDto.ParsePayload(payload);
            GotScoreResponse?.Invoke(scoreResToUnityDto);
        }

        public static void OnPlayAgain()
        {
            Debug.Log("[UnityBridgeToGameListener.OnPlayAgain]: recieved");
            PlayAgainAction?.Invoke();
        }

        public static void onEnableSound(int enable)
        {
            Debug.Log("[UnityBridgeToGameListener.onEnableSound]: recieved");
            EnableSoundAction?.Invoke(enable);
        }

    }

}
