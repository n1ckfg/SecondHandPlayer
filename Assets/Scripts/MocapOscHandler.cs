using UnityEngine;
using System;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityOSC;

public class MocapOscHandler : MonoBehaviour {

    public string outIP = "127.0.0.1";
    public int outPort = 9999;
    public MocapVisualizer[] mocapVisualizers;

    private OSCServer myServer;

    private void Start() {
        OSCHandler.Instance.Init();
        OSCHandler.Instance.CreateClient("myClient", IPAddress.Parse(outIP), outPort);
    }

    private void Update() {
        for (int i=0; i<mocapVisualizers.Length; i++) { 
            if (mocapVisualizers[i].ready) {
                foreach (Transform child in mocapVisualizers[i].allTransforms) {
                    OSCHandler.Instance.SendMessageToClient("myClient", "/" + child.name, child.position.x);
                }
            }
        }
    }

}