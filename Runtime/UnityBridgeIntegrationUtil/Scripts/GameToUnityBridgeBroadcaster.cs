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

        public static void UpdateScore(int score, long sessionTimeInMs = 0)
        {
            Debug.Log("[GameToUnityBridgeBroadcaster.UpdateScore] score=" + score + ", sessionTimeInMs=" + sessionTimeInMs);

            object[] scoreobj = new object[2];
            scoreobj[0] = score;
            scoreobj[1] = sessionTimeInMs;

            bridgeGo?.BroadcastMessage("GameToUnityBridge_updateScoreObj", scoreobj, SendMessageOptions.DontRequireReceiver);
        }

        public static void ExitGameUpdateScore(int score, long sessionTimeInMs = 0)
        {
            Debug.Log("[GameToUnityBridgeBroadcaster.ExitGameUpdateScore] score=" + score + ", sessionTimeInMs=" + sessionTimeInMs);

            object[] scoreobj = new object[2];
            scoreobj[0] = score;
            scoreobj[1] = sessionTimeInMs;

            bridgeGo?.BroadcastMessage("GameToUnityBridge_exitGameUpdateScore", scoreobj, SendMessageOptions.DontRequireReceiver);
        }

        public static void ExitGame()
        {
            Debug.Log("[GameToUnityBridgeBroadcaster.ExitGame]");

            bridgeGo?.BroadcastMessage("GameToUnityBridge_exitGame", SendMessageOptions.DontRequireReceiver);
        }

        public static void ShowFullScreenAd()
        {
            Debug.Log("[GameToUnityBridgeBroadcaster.ShowFullScreenAd]");

            bridgeGo?.BroadcastMessage("GameToUnityBridge_showFullScreenAd", SendMessageOptions.DontRequireReceiver);
        }

        public static void ShowBannerAd(string param)
        {
            Debug.Log("[GameToUnityBridgeBroadcaster.ShowBannerAd]");

            bridgeGo?.BroadcastMessage("GameToUnityBridge_showBannerAd", param, SendMessageOptions.DontRequireReceiver);
        }
        
        public static void UpdateSound(int soundState)
        {
            Debug.Log("[GameToUnityBridgeBroadcaster.UpdateSound]"+ soundState);

            bridgeGo?.BroadcastMessage("GameToUnityBridge_updateSound", soundState, SendMessageOptions.DontRequireReceiver);
        }

        public static void PauseGame()
        {
            Debug.Log("[GameToUnityBridgeBroadcaster.PauseGame]");

            bridgeGo?.BroadcastMessage("GameToUnityBridge_pauseGame", SendMessageOptions.DontRequireReceiver);
        }

        public static void SendGameData(string gamedata)
        {
            Debug.Log("[GameToUnityBridgeBroadcaster.SendGameData]" + gamedata);

            bridgeGo?.BroadcastMessage("GameToUnityBridge_sendGameData", gamedata,SendMessageOptions.DontRequireReceiver);
        }

        public static void SendPlayerRankPosition(string Rankposition)
        {
            Debug.Log("[GameToUnityBridgeBroadcaster.SendPlayerRankPosition]" + Rankposition);

            bridgeGo?.BroadcastMessage("GameToUnityBridge_sendPlayerRankPosition", Rankposition, SendMessageOptions.DontRequireReceiver);
        }
    }

}
