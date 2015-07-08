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


	private void StartServer() {
		// initialize a server on the network and register it with the master server
		Network.InitializeServer (5, 25000, !Network.HavePublicAddress ());
		MasterServer.RegisterHost (gameTypeName, gameName);
	}

	void OnServerInitialized() {
		Debug.Log ("Server initialized");
	}

	void OnGUI() {
		if (!Network.isClient && !Network.isServer) {
			if (GUI.Button(new Rect(100, 100, 250, 100), "Start Server")) {
				StartServer();
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
