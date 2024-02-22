using UnityEditor;
using UnityEngine;
using UnityEditor.IMGUI.Controls;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;

namespace UnityBridgeIntegration
{
    [System.Serializable]
    internal class UnityBridgeMainTab
    {
        Rect m_Position;

        internal Editor m_Editor = null;
        [SerializeField]
        private BuildTabData m_UserData;
        [SerializeField]
        private string gameNamespaceName;
        private CsharpNamespaceInserter csharpNamespaceInserter;
        private ScriptMoverForBridgeIntegration scriptMoverForBridgeIntegration;

      
        internal UnityBridgeMainTab()
        {
            m_UserData = new BuildTabData();
            m_UserData.m_OnToggles = new List<string>();
            m_UserData.m_UseDefaultPath = true;

            csharpNamespaceInserter = new CsharpNamespaceInserter(
                //"/Users/msudhanshu/Tech/Comp/onegame/frontend/onegame-mobile/unity/unity-bridge/Assets/Unity-bridge-integration-tool/Tests/Dummyfile.cs",
                "Assets/Unity-bridge-integration-tool/Tests",
               
                "MyHelixGame");
            scriptMoverForBridgeIntegration = new ScriptMoverForBridgeIntegration(
                "Assets/Unity-bridge-integration-tool/Tests",
                "MyHelixGame");

        }

        internal void OnEnable(Rect pos)
        {
            if (m_UserData.m_UseDefaultPath)
            {
                ResetPathToDefault();
            }
        }

        internal void OnDisable()
        {
            var dataPath = System.IO.Path.GetFullPath(".");
            dataPath = dataPath.Replace("\\", "/");
            dataPath += "/Library/AssetBundleBrowserBuild.dat";

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(dataPath);

            bf.Serialize(file, m_UserData);
        }

        internal void OnGUI(Rect pos)
        {
            m_Position = pos;

            if (Application.isPlaying)
            {
                var style = new GUIStyle(GUI.skin.label);
                style.alignment = TextAnchor.MiddleCenter;
                style.wordWrap = true;
                GUI.Label(
                    new Rect(m_Position.x + 1f, m_Position.y + 1f, m_Position.width - 2f, m_Position.height - 2f),
                    new GUIContent("Inspector unavailable while in PLAY mode"),
                    style);
            }
            else
            {
                OnGUIEditor();
            }
        }

        private void OnGUIEditor()
        {
            EditorGUILayout.Space();
            GUILayout.BeginVertical();


            ////output path
            using (new EditorGUI.DisabledScope())
            {
                EditorGUILayout.Space();
                GUILayout.BeginHorizontal();
                var newPath = EditorGUILayout.TextField("Output Path", m_UserData.m_OutputPath);
                if (!System.String.IsNullOrEmpty(newPath) && newPath != m_UserData.m_OutputPath)
                {
                    m_UserData.m_UseDefaultPath = false;
                    m_UserData.m_OutputPath = newPath;
                    //EditorUserBuildSettings.SetPlatformSettings(EditorUserBuildSettings.activeBuildTarget.ToString(), "AssetBundleOutputPath", m_OutputPath);
                }
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("Browse", GUILayout.MaxWidth(75f)))
                    BrowseForFolder();
                if (GUILayout.Button("Reset", GUILayout.MaxWidth(75f)))
                    ResetPathToDefault();
                //if (string.IsNullOrEmpty(m_OutputPath))
                //    m_OutputPath = EditorUserBuildSettings.GetPlatformSettings(EditorUserBuildSettings.activeBuildTarget.ToString(), "AssetBundleOutputPath");
                GUILayout.EndHorizontal();
                EditorGUILayout.Space();
            }


            GUILayout.BeginHorizontal();
            var gameNamespaceNameTemp = EditorGUILayout.TextField("Game Namespace name", gameNamespaceName);
            if (!System.String.IsNullOrEmpty(gameNamespaceNameTemp) && gameNamespaceNameTemp != gameNamespaceName)
            {
                gameNamespaceName = gameNamespaceNameTemp;
            }
            GUILayout.EndHorizontal();

            EditorGUILayout.Space();
            GUILayout.BeginHorizontal();

            //if (GUILayout.Button("Add File", GUILayout.MaxWidth(75f)))
            //{
            //    BrowseForFile();
            //}
            //if (GUILayout.Button("Add Folder", GUILayout.MaxWidth(75f)))
            //{
            //    BrowseForFolder();
            //}
            if (GUILayout.Button("Export Tags", GUILayout.MaxWidth(175f)))
            {
                Debug.LogError("Export Tags Need to Implement.");
                 string[] projectContent = new string[] {"ProjectSettings/TagManager.asset"};
                AssetDatabase.ExportPackage(projectContent, "UnityBridgeIntegrationTagManager.unitypackage",ExportPackageOptions.Interactive | ExportPackageOptions.Recurse |ExportPackageOptions.IncludeDependencies);
                 Debug.Log("Tags Exported");
            }

            if (GUILayout.Button("Import Tags", GUILayout.MaxWidth(175f)))
            {
                Debug.LogError("Import Tags Need to Implement.");
            }

            if (GUILayout.Button("Insert Namespace", GUILayout.MaxWidth(175f)))
            {
                Debug.Log("Insert gameNamespaceName: " + gameNamespaceName.ToString());
                Debug.Log("Insert m_OutputPath: " + m_UserData.m_OutputPath.ToString());
                csharpNamespaceInserter.insertNamespaceInAllFile(m_UserData.m_OutputPath, gameNamespaceName);
            }
            if (GUILayout.Button("Move Scripts", GUILayout.MaxWidth(175f)))
            {
                Debug.LogError("Export Tags Need to Implement.");
                //scriptMoverForBridgeIntegration.moveAllFiles();
            }
            GUILayout.EndHorizontal();


            GUILayout.EndVertical();
            EditorGUILayout.Space();

        }

        internal void RefreshBundles()
        {

        }

        private void BrowseForFolder()
        {
            m_UserData.m_UseDefaultPath = false;
            var newPath = EditorUtility.OpenFolderPanel("Bundle Folder", m_UserData.m_OutputPath, string.Empty);
            if (!string.IsNullOrEmpty(newPath))
            {
                var gamePath = System.IO.Path.GetFullPath(".");
                gamePath = gamePath.Replace("\\", "/");
                if (newPath.StartsWith(gamePath) && newPath.Length > gamePath.Length)
                    newPath = newPath.Remove(0, gamePath.Length + 1);
                m_UserData.m_OutputPath = newPath;
                //EditorUserBuildSettings.SetPlatformSettings(EditorUserBuildSettings.activeBuildTarget.ToString(), "AssetBundleOutputPath", m_OutputPath);
            }
        }
        private void ResetPathToDefault()
        {
            m_UserData.m_UseDefaultPath = true;
            m_UserData.m_OutputPath = "AssetBundles/";
            m_UserData.m_OutputPath += m_UserData.m_BuildTarget.ToString();
            //EditorUserBuildSettings.SetPlatformSettings(EditorUserBuildSettings.activeBuildTarget.ToString(), "AssetBundleOutputPath", m_OutputPath);
        }





        internal enum CompressOptions
        {
            Uncompressed = 0,
            StandardCompression,
            ChunkBasedCompression,
        }
        GUIContent[] m_CompressionOptions =
        {
            new GUIContent("No Compression"),
            new GUIContent("Standard Compression (LZMA)"),
            new GUIContent("Chunk Based Compression (LZ4)")
        };
        int[] m_CompressionValues = { 0, 1, 2 };

        //Note: this is the provided BuildTarget enum with some entries removed as they are invalid in the dropdown
        internal enum ValidBuildTarget
        {
            //NoTarget = -2,        --doesn't make sense
            //iPhone = -1,          --deprecated
            //BB10 = -1,            --deprecated
            //MetroPlayer = -1,     --deprecated
            StandaloneOSXUniversal = 2,
            StandaloneOSXIntel = 4,
            StandaloneWindows = 5,
            WebPlayer = 6,
            WebPlayerStreamed = 7,
            iOS = 9,
            PS3 = 10,
            XBOX360 = 11,
            Android = 13,
            StandaloneLinux = 17,
            StandaloneWindows64 = 19,
            WebGL = 20,
            WSAPlayer = 21,
            StandaloneLinux64 = 24,
            StandaloneLinuxUniversal = 25,
            WP8Player = 26,
            StandaloneOSXIntel64 = 27,
            BlackBerry = 28,
            Tizen = 29,
            PSP2 = 30,
            PS4 = 31,
            PSM = 32,
            XboxOne = 33,
            SamsungTV = 34,
            N3DS = 35,
            WiiU = 36,
            tvOS = 37,
            Switch = 38
        }

        [System.Serializable]
        internal class BuildTabData
        {
            internal List<string> m_OnToggles;
            internal ValidBuildTarget m_BuildTarget = ValidBuildTarget.StandaloneWindows;
            internal CompressOptions m_Compression = CompressOptions.StandardCompression;
            internal string m_OutputPath = string.Empty;
            internal bool m_UseDefaultPath = true;
        }
    }
}