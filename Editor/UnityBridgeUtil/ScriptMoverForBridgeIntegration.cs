﻿using UnityEngine;
using UnityEditor;
using System.IO;
using System;
using System.Text;

namespace UnityBridgeIntegration
{

    internal class ScriptMoverForBridgeIntegration
    {
        internal string path { get; private set; }
        internal string gameNamespaceName { get; private set; }

        internal ScriptMoverForBridgeIntegration(string path, string gameNamespaceName)
        {
            if (string.IsNullOrEmpty(path))
            {
                string msg = string.Format("AssetBundleRecord encountered invalid parameters path={0}, bundle={1}",
                    path,
                    path);

                throw new System.ArgumentException(msg);
            }

            this.path = path;
            this.gameNamespaceName = gameNamespaceName;
        }


        public void moveAllFiles()
        {
            moveAllFiles(path, gameNamespaceName);
        }

        public void moveAllFiles(string path, string gameNamespaceName)
        {
            Debug.Log("moveAllFiless in folder path : " + path);
            Debug.Log("******* Move files to Assets/unity-games-scripts/" +gameNamespaceName+ " manualy by dragging *******");
            try
            {
                //DirectoryInfo dir = new DirectoryInfo(path); //"Assets/Resources/Vehicles"
                //FileInfo[] info = dir.GetFiles("*.cs");
                //foreach (FileInfo f in info)
                //{
                //    insertNamespaceInFile(f.FullName, gameNamespaceName);
                //}

                EditorApplication.LockReloadAssemblies();
                var files = Directory.GetFiles(path, "*.cs", SearchOption.AllDirectories);
                foreach (var f in files)
                {
                    moveFile(path, f, gameNamespaceName);
                }
            } catch(Exception e)
            {

            }
            EditorApplication.UnlockReloadAssemblies();
            AssetDatabase.Refresh();
            EditorUtility.RequestScriptReload();

        }


        public void moveFile(string path, string filePath, string gameNamespaceName)
        {
            string filePathNameOnly = filePath.Replace(path + "/", "");
            //Debug.Log("Move from " + filePath + " to " + "Assets/GameScript/" + gameNamespaceName + "/" + filePathNameOnly);
             Debug.Log(": " + filePath);


            var filePathOnly = Path.GetDirectoryName(filePathNameOnly);// filePathNameOnly.Split("/");
            Directory.CreateDirectory("Assets/unity-games-scripts/" + gameNamespaceName + "/" + filePathOnly);



            //File.Move(filePath, "Assets/GameScript/" + gameNamespaceName + "/" + filePathNameOnly);
            //File.Move(filePath, "Assets/GameScript/" + gameNamespaceName + "/" + filePathNameOnly + ".meta");

            //FileUtil.CopyFileOrDirectory(filePath, "Assets/GameScript/"+ gameNamespaceName + "/" + filePath);
            //FileUtil.CopyFileOrDirectory(parrentPath, "Assets/GameScript/" + gameNamespaceName);
            //FileUtil.MoveFileOrDirectory(filePath, "Assets/GameScript/" + gameNamespaceName + "/" + filePathNameOnly);


        }

    }
}

