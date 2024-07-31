using BepInEx;
using DogBoard.Utils;
using System;
using System.IO;
using System.Reflection;
using UnityEngine;
using Utilla;

namespace DogBoard
{
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.6.14")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        //I'm not sorting ts
        private GameObject dogBoard;
        private GameObject Button;
        private GetAPI api;
        private Material screenMat;
        public static Plugin instance;

        void Start()
        {
            Utilla.Events.GameInitialized += OnGameInitialized;
        }

        //Enable Crap
        void OnEnable()
        {
            HarmonyPatches.ApplyHarmonyPatches();
        }
        //Disable Crap
        void OnDisable()
        {
            HarmonyPatches.RemoveHarmonyPatches();
        }

        void OnGameInitialized(object sender, EventArgs e)
        {
            setupBoard();
            instance = this;
        }

        //Setting crap up 😒
        void setupBoard()
        {
            var bundle = LoadAssetBundle("DogBoard.resources.dboard");
            var asset = bundle.LoadAsset<GameObject>("DogBoard");

            //Instanciates the Dog board
            dogBoard = Instantiate(asset);
            Button = GameObject.Find("Button");
            api = dogBoard.AddComponent<GetAPI>();
            screenMat = GameObject.Find("DBoard").GetComponent<Renderer>().materials[1];
            api.Initialize(screenMat);
            Button.AddComponent<Button>();
            Button.layer = 18;
            
        }

        //Asset Bundle Loading Crap 🤓🤓🤓🤓
        public AssetBundle LoadAssetBundle(string path)
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
            AssetBundle bundle = AssetBundle.LoadFromStream(stream);
            stream.Close();
            return bundle;
        }

        void Update()
        {
            
        }

        public void PressEvent()
        {
            api.doApi();
        }

    }
}
