﻿using UnityEngine;
using System.Collections;

/* MainMenu Script
 * functions
 * - Server Initialize and Connect to MasterServer
 */

public class MainMenu : MonoBehaviour {

	/* master server settings */
	public bool   useMyMasterServer          = false;	// manual switch master server. mine or public
	public const string myMasterServerIP     = "127.0.0.1";
	public const int    myMasterServerPort   = 23466;
	public const bool   myMasterServerUseNat = false;

	/* room const values */
	public const string gameTypeName = "heroes_and_daemons";
	public const string gameName     = "heroes_and_daemons";
	public string gameComment        = "sorry, room comment is not implemented.";		// room comment
	public const int roomUserLimit   = 1;
	public const int roomPort        = 25002;

	/* menus */
	private Rect networkMenu;
	private Rect serverListMenu;
	private Rect settingMenu;
	private const int networkMenuID    = 0;
	private const int serverListMenuID = 1;
	private const int settingMenuID    = 2;

	/* const variables */
	private const string kLoadTargetLevelName       = "NetworkTestField";
	private const string kCharacterSettingLevelName = "CharacterSetting";


	/* debug inforamation */
	void OnFailedToConnectToMasterServer(NetworkConnectionError info) {
		Debug.Log("OnFailedToConnectToMasterServer : " + info);
	}

	void OnFailedToConnect(NetworkConnectionError info) {
		Debug.Log("OnFailedToConnect : " + info);
	}

	/* window make methods */
	private void makeNetworkMenu(int id) {
		GUILayout.Space(10);

		if (Network.peerType == NetworkPeerType.Disconnected) {
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);

            if (GUILayout.Button ("Create Room")) {
                Network.InitializeServer(roomUserLimit, roomPort, myMasterServerUseNat);
                MasterServer.RegisterHost(gameTypeName, gameName, gameComment);
            }

            if (GUILayout.Button("Refresh")){
                MasterServer.RequestHostList(gameTypeName);
            }

            GUILayout.FlexibleSpace ();
            GUILayout.EndHorizontal ();
			return;
        } else {
            if (GUILayout.Button("Disconnect")) {
                Network.Disconnect();
                MasterServer.UnregisterHost();
            }
            GUILayout.FlexibleSpace();
        }
        GUI.DragWindow(new Rect(0, 0, 1000, 1000));
	}

	private void makeServerListMenu(int id) {
		GUILayout.Space(5);

		HostData[] rooms = MasterServer.PollHostList();

		foreach (HostData room in rooms) {
			GUILayout.BeginHorizontal();

			makeLabelWithSpace(room.gameName);

			string userLabel = room.connectedPlayers + "/" + room.playerLimit;
			makeLabelWithSpace(userLabel);

			string ipInfo = "";
			foreach (string ip in room.ip)
				ipInfo += ip + ":" + room.port + " ";
			makeLabelWithSpace(ipInfo);

			makeLabelWithSpace(room.comment);

			GUILayout.FlexibleSpace();
			if (GUILayout.Button("Connect")) {
				Network.Connect(room);
			}
			GUILayout.EndHorizontal();
		}
	}

	private void makeSettingMenu(int id) {
		/* TODO: show character status in main manu */
		if (GUILayout.Button("edit")) {
			Application.LoadLevel(kCharacterSettingLevelName);
		}
	}

	/* utils */

	private void makeLabelWithSpace(string str) {
		GUILayout.Label(str);
		GUILayout.Space(5);
	}

	/* methods */

	void Awake() {
		/* initialize variables */
		networkMenu    = new Rect(Screen.width - 220, 0, 200, 50);
		serverListMenu = new Rect(0, 70, Screen.width, 100);
		settingMenu    = new Rect(20, 0, 200, 50);

		if (useMyMasterServer) {
			MasterServer.ipAddress = myMasterServerIP;
			MasterServer.port      = myMasterServerPort;
		}

		MasterServer.dedicatedServer = true;	// count host user in number to room joined user
	}

	void OnGUI() {
		networkMenu = GUILayout.Window(networkMenuID, networkMenu, makeNetworkMenu, "server console");
		if (Network.peerType == NetworkPeerType.Disconnected && MasterServer.PollHostList ().Length != 0) {
			serverListMenu = GUILayout.Window(serverListMenuID, serverListMenu, makeServerListMenu, "server list");
		}
		settingMenu = GUILayout.Window(settingMenuID, settingMenu, makeSettingMenu, "Character Settings");
	}

	/* network callbacks */

	void OnConnectedToServer() {
		// client side : for only two user room
		Debug.Log("connected server on client");
		Application.LoadLevel(kLoadTargetLevelName);
	}

 	void OnPlayerConnected() {
		// server side : for only two user room
		Debug.Log("Detect client connection");
		Application.LoadLevel(kLoadTargetLevelName);

	}
}
