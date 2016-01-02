using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class Chat : NetworkBehaviour {

    public SyncListString chatHistory = new SyncListString();
    private string currentMessage = string.Empty;
    
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
    }
    private void OnGUI()
    {
        GUILayout.BeginHorizontal(GUILayout.Width(250));
        currentMessage = GUILayout.TextField(currentMessage);
        if (GUILayout.Button("Send"))
        {
            if (!string.IsNullOrEmpty(currentMessage.Trim()))
            {
                // enviar msg
                if (isLocalPlayer)
                {
                    RpcChatMessage(currentMessage);
                }
                else
                {
                    CmdSendMessage(currentMessage);
                }                
                currentMessage = string.Empty;
            }
        }
        GUILayout.EndHorizontal();
        foreach (string m in chatHistory)
        {
            GUILayout.Label(m);
        }
    }
    [Command]
    public void CmdSendMessage(string message)
    {
        chatHistory.Add(message);
    }
    [ClientRpc]
    public void RpcChatMessage(string message)
    {
        chatHistory.Add(message);
    }
}
