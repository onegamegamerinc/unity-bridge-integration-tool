using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityBridgeIntegration
{
    public class ScoreResToUnityDto
    {
        public long onlinePlayers;
        public long rank;
        public int currentScore;
        public long totalScore;
        public long coinReward;
        public long scoreFrequency;


        public static ScoreResToUnityDto ParsePayload(string payload)
        {
            ScoreResToUnityDto scoreResToUnityDto = JsonUtility.FromJson<ScoreResToUnityDto>(payload);
            Debug.Log(payload);
            Debug.Log(scoreResToUnityDto);
            return scoreResToUnityDto;// new InitGameToUnityDto();
        }
    }
}