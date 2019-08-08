using UnityEngine;
using System;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityOSC;

public class MocapOscHandler : MonoBehaviour {

    public enum ChannelFormat { BONE_NAME, ISADORA };
    public ChannelFormat channelFormat = ChannelFormat.BONE_NAME;

    public string outIP = "127.0.0.1";
    public int outPort = 9999;
    public List<MocapVisualizer> mocapVisualizers = new List<MocapVisualizer>();

    private OSCServer myServer;
    private string clientName = "myClient";

    private void Start() {
        OSCHandler.Instance.Init();
        OSCHandler.Instance.CreateClient(clientName, IPAddress.Parse(outIP), outPort);
    }

    private void Update() {
        int isadoraCounter = 1;
        for (int i=0; i<mocapVisualizers.Count; i++) { 
            if (mocapVisualizers[i].ready) {
                foreach (Transform child in mocapVisualizers[i].taggedTransforms) {
                    List<object> msg = new List<object>();
                    msg.Add(child.position.x);
                    msg.Add(child.position.y);
                    msg.Add(child.position.z);

                    string channelName;
                    switch (channelFormat) {
                        case (ChannelFormat.ISADORA):
                            channelName = "/isadora-multi/" + isadoraCounter;
                            isadoraCounter++;
                            break;
                        default:
                            channelName = "/" + child.name;
                            break;
                    }

                    OSCHandler.Instance.SendMessageToClient(clientName, channelName, msg);
                }
            }
        }
    }

}