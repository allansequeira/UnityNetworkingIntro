using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	/// <summary>
	/// Name of game when registering with the server.
	/// </summary>
	private const string gameTypeName = "UniqueGameName";

	/// <summary>
	/// Name of room.
	/// </summary>
	private const string gameName = "RoomName";

	private HostData[] hostList;

	/// <summary>
	/// Create / Start a server
	/// </summary>
	private void StartServer() {
		// initialize a server on the network and register it with the master server
		Network.InitializeServer (5, 25000, !Network.HavePublicAddress ());
		MasterServer.RegisterHost (gameTypeName, gameName);
		Debug.Log ("MasterServer.ipAddress/port: " + MasterServer.ipAddress 
		           + ":" + MasterServer.port + ", MasterServer.dedicatedServer: " 
		           + MasterServer.dedicatedServer);

	}

	/// <summary>
	/// Refreshs the host list.
	/// </summary>
	private void RefreshHostList() {
		MasterServer.RequestHostList (gameTypeName);
	}


	// Callbacks / messages

	void OnServerInitialized() {
		Debug.Log ("Server initialized");
		Debug.Log (">>> MasterServer.ipAddress/port: " + MasterServer.ipAddress 
		           + ":" + MasterServer.port + ", MasterServer.dedicatedServer: " 
		           + MasterServer.dedicatedServer);

	}

	void OnMasterServerEvent(MasterServerEvent masterServerEvent) {
		Debug.Log ("master server event message received: " + masterServerEvent);

		if (masterServerEvent == MasterServerEvent.RegistrationSucceeded) {
			Debug.Log ("Registered server");
		}

		if (masterServerEvent == MasterServerEvent.HostListReceived) {
//			yield return new WaitForSeconds(1.5f);
			hostList = MasterServer.PollHostList();
			//hostList = Invoke("MasterServer.PollHostList", 2f);
		}
	}


	// GUI

	void OnGUI() {
		if (!Network.isClient && !Network.isServer) {
			if (GUI.Button(new Rect(100, 100, 250, 100), "Start Server")) {
				Debug.Log ("Starting Server...");
				StartServer();
			}

			if (GUI.Button(new Rect(100, 250, 250, 100), "Refresh Hosts")) {
				Debug.Log ("Refreshing host list...");
				RefreshHostList();
			}

			if (hostList != null && hostList.Length > 0) {
				//Debug.Log ("HostData list: " + hostList);
				for (int i = 0; i < hostList.Length; i++) {
					Debug.Log(hostList[i]);
				}
			}
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
