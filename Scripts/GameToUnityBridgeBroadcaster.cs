using UnityEngine;
using FlutterUnityIntegration;

namespace UnityBridgeIntegration
{

    public class GameToUnityBridgeBroadcaster
    {
        static GameObject bridgeGo = null;
        private static GameObject findUnityBridgeReieverGo()
        {
            if (bridgeGo == null)
            {
                bridgeGo = GameObject.Find("UnityBridgeManager");
                //bridgeGo = GameObject.FindWithTag("GAME_SIDE_TAG_FOR_UNITY_BRIDGE");
            }
            return bridgeGo;
        }

        public static void Broadcast_updateScore(int score)
        {
            Debug.Log("UnityBridgeToGame_OnScoreUpdateResponse: " + score);
            GameObject _bridgeGo = findUnityBridgeReieverGo();
            if (_bridgeGo != null)
            {
                _bridgeGo.BroadcastMessage("GameToUnityBridge_updateScore", score, SendMessageOptions.DontRequireReceiver);
            } else
            {
                Debug.LogError("game object UnityBridgeManager could not be found");
            }
        }

        public static void Broadcast_exitGame()
        {
            Debug.Log("Broadcast_exitGame: ");
            GameObject _bridgeGo = findUnityBridgeReieverGo();
            if (_bridgeGo != null)
            {
                _bridgeGo.BroadcastMessage("GameToUnityBridge_exitGame", SendMessageOptions.DontRequireReceiver);
            } else
            {
                Debug.LogError("game object UnityBridgeManager could not be found");
            }
        }
    }

}
