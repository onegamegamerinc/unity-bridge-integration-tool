using UnityEngine;

[assembly: System.Runtime.CompilerServices.InternalsVisibleToAttribute("Unity.UnityBridgeIntegration.Plugin")]

namespace UnityBridgeIntegration
{

    public class GameToUnityBridgeBroadcaster
    {
        static GameObject s_bridgeGo = null;

        private static GameObject bridgeGo
        {
            get
            {
                Debug.Log("[GameToUnityBridgeBroadcaster.s_bridgeGo]");
                s_bridgeGo = GameObject.Find("UnityBridgeManager");
                if (s_bridgeGo == null)
                {
                    Debug.LogError("[GameToUnityBridgeBroadcaster.s_bridgeGo] GameObject UnityBridgeManager not found");
                }
                return s_bridgeGo;
            }
        }

        public static void Broadcast_updateScore(int score, long sessionTimeInMs = 0)
        {
            Debug.Log("[GameToUnityBridgeBroadcaster.Broadcast_updateScore] score=" + score + ", sessionTimeInMs=" + sessionTimeInMs);

            object[] scoreobj = new object[2];
            scoreobj[0] = score;
            scoreobj[1] = sessionTimeInMs;

            bridgeGo?.BroadcastMessage("GameToUnityBridge_updateScoreObj", scoreobj, SendMessageOptions.DontRequireReceiver);
        }

        public static void Broadcast_exitGameUpdateScore(int score, long sessionTimeInMs = 0)
        {
            Debug.Log("[GameToUnityBridgeBroadcaster.Broadcast_exitGameUpdateScore] score=" + score + ", sessionTimeInMs=" + sessionTimeInMs);

            object[] scoreobj = new object[2];
            scoreobj[0] = score;
            scoreobj[1] = sessionTimeInMs;

            bridgeGo?.BroadcastMessage("GameToUnityBridge_exitGameUpdateScore", scoreobj, SendMessageOptions.DontRequireReceiver);
        }

        public static void Broadcast_exitGame()
        {
            Debug.Log("[GameToUnityBridgeBroadcaster.Broadcast_exitGame]");

            bridgeGo?.BroadcastMessage("GameToUnityBridge_exitGame", SendMessageOptions.DontRequireReceiver);
        }

        public static void Broadcast_showFullScreenAd()
        {
            Debug.Log("[GameToUnityBridgeBroadcaster.Broadcast_showFullScreenAd]");

            bridgeGo?.BroadcastMessage("GameToUnityBridge_showFullScreenAd", SendMessageOptions.DontRequireReceiver);
        }

        public static void Broadcast_showBannerAd(string param)
        {
            Debug.Log("[GameToUnityBridgeBroadcaster.Broadcast_showBannerAd]");

            bridgeGo?.BroadcastMessage("GameToUnityBridge_showBannerAd", param, SendMessageOptions.DontRequireReceiver);
        }
    }

}
