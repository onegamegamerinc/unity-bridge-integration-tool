using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[assembly: System.Runtime.CompilerServices.InternalsVisibleToAttribute("Unity.UnityBridgeIntegration.Editor.Tests")]

namespace UnityBridgeIntegration
{

    public class UnityBridgeIntegrationMain : EditorWindow, IHasCustomMenu, ISerializationCallbackReceiver
    {

        private static UnityBridgeIntegrationMain s_instance = null;
        internal static UnityBridgeIntegrationMain instance
        {
            get
            {
                if (s_instance == null)
                    s_instance = GetWindow<UnityBridgeIntegrationMain>();
                return s_instance;
            }
        }

        internal const float kButtonWidth = 150;

        enum Mode
        {
            Inspect,
            Browser,
            Builder,
        }
        [SerializeField]
        Mode m_Mode;

        //[SerializeField]
        //int m_DataSourceIndex;

        //[SerializeField]
        //internal AssetBundleManageTab m_ManageTab;

        //[SerializeField]
        //internal AssetBundleBuildTab m_BuildTab;

        //[SerializeField]
        //internal AssetBundleInspectTab m_InspectTab;

        [SerializeField]
        internal UnityBridgeMainTab m_unityBridgeMainTab;

        private Texture2D m_RefreshTexture;

        const float k_ToolbarPadding = 15;
        const float k_MenubarPadding = 32;

        [MenuItem("Window/Unity Bridge Integration Tool", priority = 2050)]
        static void ShowWindow()
        {
            s_instance = null;
            instance.titleContent = new GUIContent("UnityBridge");
            instance.Show();
        }

        [SerializeField]
        internal bool multiDataSource = false;
        //List<AssetBundleDataSource.ABDataSource> m_DataSourceList = null;
        public virtual void AddItemsToMenu(GenericMenu menu)
        {
            if(menu != null)
               menu.AddItem(new GUIContent("Custom Sources"), multiDataSource, FlipDataSource);
        }
        internal void FlipDataSource()
        {
            multiDataSource = !multiDataSource;
        }

        private void OnEnable()
        {

            Rect subPos = GetSubWindowArea();
            //if(m_ManageTab == null)
            //    m_ManageTab = new AssetBundleManageTab();
            //m_ManageTab.OnEnable(subPos, this);
            //if(m_BuildTab == null)
            //    m_BuildTab = new AssetBundleBuildTab();
            //m_BuildTab.OnEnable(this);

            if (m_unityBridgeMainTab == null)
                m_unityBridgeMainTab = new UnityBridgeMainTab();
            m_unityBridgeMainTab.OnEnable(subPos);

            m_RefreshTexture = EditorGUIUtility.FindTexture("Refresh");

            InitDataSources();
        } 
        private void InitDataSources()
        {
            //determine if we are "multi source" or not...
            multiDataSource = false;
        }
        private void OnDisable()
        {
            //if (m_BuildTab != null)
            //    m_BuildTab.OnDisable();
            if (m_unityBridgeMainTab != null)
                m_unityBridgeMainTab.OnDisable();
        }

        public void OnBeforeSerialize()
        {
        }
        public void OnAfterDeserialize()
        {
        }

        private Rect GetSubWindowArea()
        {
            float padding = k_MenubarPadding;
            if (multiDataSource)
                padding += k_MenubarPadding * 0.5f;
            Rect subPos = new Rect(0, padding, position.width, position.height - padding);
            return subPos;
        }

        private void Update()
        {
            switch (m_Mode)
            {
                case Mode.Builder:
                    break;
                case Mode.Inspect:
                    break;
                case Mode.Browser:
                default:
                   // m_ManageTab.Update();
                    break;
            }
        }

        private void OnGUI()
        {
            ModeToggle();

            switch(m_Mode)
            {
                case Mode.Builder:
                   // m_BuildTab.OnGUI();
                    break;
                case Mode.Inspect:
                    m_unityBridgeMainTab.OnGUI(GetSubWindowArea());
                    break;
                case Mode.Browser:
                default:
                   // m_ManageTab.OnGUI(GetSubWindowArea());
                    break;
            }
        }

        void ModeToggle()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(k_ToolbarPadding);
            bool clicked = false;
            switch(m_Mode)
            {
                case Mode.Browser:
                    clicked = GUILayout.Button(m_RefreshTexture);
                    //if (clicked)
                    //    m_ManageTab.ForceReloadData();
                    break;
                case Mode.Builder:
                    GUILayout.Space(m_RefreshTexture.width + k_ToolbarPadding);
                    break;
                case Mode.Inspect:
                    clicked = GUILayout.Button(m_RefreshTexture);
                    if (clicked)
                        m_unityBridgeMainTab.RefreshBundles();
                    break;
            }

            float toolbarWidth = position.width - k_ToolbarPadding * 4 - m_RefreshTexture.width;
            //string[] labels = new string[2] { "Configure", "Build"};
            //string[] labels = new string[3] { "Configure", "Build", "RefactorCode" };
            string[] labels = new string[1] {  "RefactorCode" };
            m_Mode = (Mode)GUILayout.Toolbar((int)m_Mode, labels, "LargeButton", GUILayout.Width(toolbarWidth) );
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            if(multiDataSource)
            {
                //GUILayout.BeginArea(r);
                GUILayout.BeginHorizontal();

                using (new EditorGUILayout.HorizontalScope(EditorStyles.toolbar))
                {
                    GUILayout.Label("Bundle Data Source:");
                    GUILayout.FlexibleSpace();
                  //  var c = new GUIContent(string.Format("{0} ({1})", AssetBundleModel.Model.DataSource.Name, AssetBundleModel.Model.DataSource.ProviderName), "Select Asset Bundle Set");
               //     if (GUILayout.Button(c , EditorStyles.toolbarPopup) )
                    {
                        GenericMenu menu = new GenericMenu();

                        menu.ShowAsContext();
                    }

                    GUILayout.FlexibleSpace();
                }

                GUILayout.EndHorizontal();
                //GUILayout.EndArea();
            }
        }


    }
}
