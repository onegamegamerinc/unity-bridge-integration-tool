using UnityEngine;

[assembly: System.Runtime.CompilerServices.InternalsVisibleToAttribute("Unity.UnityBridgeIntegration.Plugin")]

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

            
        public static void Broadcast_updateScore(int score, long sessionTimeInMs)
        {
            Debug.Log("UnityBridgeToGame_OnScoreUpdateResponse: " + score);
            GameObject _bridgeGo = findUnityBridgeReieverGo();
            if (_bridgeGo != null)
            {
                object[] scoreobj = new object[2];
                scoreobj[0] = score;
                scoreobj[1] = sessionTimeInMs;

                _bridgeGo.BroadcastMessage("GameToUnityBridge_updateScoreObj", scoreobj, SendMessageOptions.DontRequireReceiver);
            }
            else
            {
                Debug.LogError("game object UnityBridgeManager could not be found");
            }
        }

        public static void Broadcast_exitGameUpdateScore(int score)
        {
            Debug.Log("UnityBridgeToGame_OnScoreUpdateResponse: " + score);
            GameObject _bridgeGo = findUnityBridgeReieverGo();
            if (_bridgeGo != null)
            {
                _bridgeGo.BroadcastMessage("GameToUnityBridge_exitGameUpdateScore", score, SendMessageOptions.DontRequireReceiver);
            } else
            {
                Debug.LogError("game object UnityBridgeManager could not be found");
            }
        }

            
        public static void Broadcast_exitGameUpdateScore(int score, long sessionTimeInMs)
        {
            Debug.Log("UnityBridgeToGame_OnScoreUpdateResponse: " + score);
            GameObject _bridgeGo = findUnityBridgeReieverGo();
            if (_bridgeGo != null)
            {
                object[] scoreobj = new object[2];
                scoreobj[0] = score;
                scoreobj[1] = sessionTimeInMs;

                _bridgeGo.BroadcastMessage("GameToUnityBridge_exitGameUpdateScore", scoreobj, SendMessageOptions.DontRequireReceiver);
            }
            else
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

        public static void Broadcast_showFullScreenAd()
        {
            Debug.Log("Broadcast_exitGame: ");
            GameObject _bridgeGo = findUnityBridgeReieverGo();
            if (_bridgeGo != null)
            {
                _bridgeGo.BroadcastMessage("GameToUnityBridge_showFullScreenAd", SendMessageOptions.DontRequireReceiver);
            } else
            {
                Debug.LogError("game object UnityBridgeManager could not be found");
            }
        }

        public static void Broadcast_showBannerAd(string param)
        {
            Debug.Log("Broadcast_exitGame: ");
            GameObject _bridgeGo = findUnityBridgeReieverGo();
            if (_bridgeGo != null)
            {
                _bridgeGo.BroadcastMessage("GameToUnityBridge_showBannerAd",param, SendMessageOptions.DontRequireReceiver);
            } else
            {
                Debug.LogError("game object UnityBridgeManager could not be found");
            }
        }
    }

}
