using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace UnityBridgeIntegration
{

    internal class CsharpNamespaceInserter
    {
        /// <summary>
        /// Full path of the asset bundle.
        /// </summary>
        internal string path { get; private set; }
        internal string gameNamespaceName { get; private set; }

        internal CsharpNamespaceInserter(string path, string gameNamespaceName)
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


        public void insertNamespaceInAllFile()
        {
            insertNamespaceInAllFile(path, gameNamespaceName);
        }

        public void insertNamespaceInAllFile(string filePath, string gameNamespaceName)
        {
            Debug.Log("Inserting namepaces in folder path : " + filePath);

            //DirectoryInfo dir = new DirectoryInfo(path); //"Assets/Resources/Vehicles"
            //FileInfo[] info = dir.GetFiles("*.cs");
            //foreach (FileInfo f in info)
            //{
            //    insertNamespaceInFile(f.FullName, gameNamespaceName);
            //}


            var files = Directory.GetFiles(filePath, "*.cs", SearchOption.AllDirectories);
            foreach (var f in files)
            {
                insertNamespaceInFile(f, gameNamespaceName);
            }

            // Directory.GetFiles


        }


        public void insertNamespaceInFile(string filePath, string gameNamespaceName)
        {
            Debug.Log("Inserting namepaces in file path : " + filePath);
            string currentContent = String.Empty;
            string finalContent = "";
            if (File.Exists(filePath))
            {
                //currentContent = File.ReadAllText(filePath);
                //finalContent = "namespace " + gameNamespaceName + " {\n" + currentContent + "\n}";

                var lines = File.ReadLines(filePath);
                bool firstTime = false;
                finalContent += "#if " + gameNamespaceName + " \n";
                foreach (var line in lines)
                {

                    var tline = line.Trim();
                    // Debug.Log(tline);

                    //todo handle multiline comment
                    if (tline.Length == 0 || tline.StartsWith("//"))
                    {
                        finalContent += line + "\n";
                    }
                    else if (!(tline.StartsWith("using ") && tline.EndsWith(";")) && !firstTime)
                    {
                        //check if already inserted:
                        if(tline.StartsWith("#if " + gameNamespaceName)) {
                             Debug.LogWarning("Insertion is already done before for : " + filePath);
                             return;
                        }
                        //Debug.Log(firstTime);
                        firstTime = true;
                        finalContent += "namespace " + gameNamespaceName + " {\n";
                        finalContent += line + "\n";
                    }
                    else
                    {
                        finalContent += line + "\n";
                    }

                }
                finalContent += "\n}";
                finalContent += "\n#endif";

                File.WriteAllText(filePath, finalContent);

            }
            //using (var file = File.Open("yourtext.txt", FileMode.Open, FileAccess.ReadWrite))
            //{
            //    PrependString("Text you want to write.", file);
            //}
        }

        public void PrependString(string value, FileStream file)
        {
            var buffer = new byte[file.Length];

            while (file.Read(buffer, 0, buffer.Length) != 0)
            {
            }

            if (!file.CanWrite)
                throw new ArgumentException("The specified file cannot be written.", "file");

            file.Position = 0;
            var data = Encoding.Unicode.GetBytes(value);
            file.SetLength(buffer.Length + data.Length);
            file.Write(data, 0, data.Length);
            file.Write(buffer, 0, buffer.Length);
        }

    }
}