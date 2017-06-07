//=============================================================================================================================
//
// Copyright (c) 2015-2017 VisionStar Information Technology (Shanghai) Co., Ltd. All Rights Reserved.
// EasyAR is the registered trademark or trademark of VisionStar Information Technology (Shanghai) Co., Ltd in China
// and other countries for the augmented reality technology developed by VisionStar Information Technology (Shanghai) Co., Ltd.
//
//=============================================================================================================================

using UnityEngine;
using System.Collections;
using EasyAR;

namespace EasyARSample
{
    public class HelloARSLAM : MonoBehaviour
    {
        private const string title = "Please enter KEY first!";
        private const string boxtitle = "===PLEASE ENTER YOUR KEY HERE===";
        private const string keyMessage = "mYHDvYMSkZulyXETH3iIcafZsvlhBcpqNwNind3ZRInz1IQ77ZABfRqPg5J6TQqc11DDQjSsNZPBT1M0dUMIwWjfqTCneK5DW8shcQ0JAh01FYe3sNQBeEDR2TM6MqJ1AAzy03vG2kv4T9v2Fi03gQMCg7KBTPq5LU5jdxd0SuTv9qqLWOw5olklHH04hcjCNFCVCCyL";

        public GUISkin skin;
        GameObject arSceneTracker;
        private ARSceneTrackerBehaviour arSceneBaseBehaviour;

        private void Awake()
        {
            var EasyARBehaviour = FindObjectOfType<EasyARBehaviour>();
            if (EasyARBehaviour.Key.Contains(boxtitle))
            {
#if UNITY_EDITOR
                UnityEditor.EditorUtility.DisplayDialog(title, keyMessage, "OK");
#endif
                Debug.LogError(title + " " + keyMessage);
            }
            EasyARBehaviour.Initialize();
            arSceneTracker = new GameObject("ARSceneTracker");
            arSceneTracker.transform.parent = transform;
        }

        void StartSLAM()
        {
            arSceneBaseBehaviour = arSceneTracker.AddComponent<ARSceneTrackerBehaviour>();
            arSceneBaseBehaviour.Bind(ARBuilder.Instance.CameraDeviceBehaviours[0]);
            arSceneBaseBehaviour.StartTrack();
        }

        void StopSLAM()
        {
            arSceneBaseBehaviour.StopTrack();
            DestroyImmediate(arSceneBaseBehaviour);
        }

        void OnGUI()
        {
            if (!arSceneBaseBehaviour)
            {
                if (GUI.Button(new Rect(Screen.width / 2 - 80, Screen.height - 85, 160, 80), "Start", skin.GetStyle("Button")))
                    StartSLAM();
            }
            else
            {
                if (GUI.Button(new Rect(Screen.width / 2 - 80, Screen.height - 85, 160, 80), "Stop", skin.GetStyle("Button")))
                    StopSLAM();
            }
        }
    }
}
