using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	// Use this for initialization

	public GameObject standByCamera;
	SpawnSpot[] spot;
	void Start ()
	{
		spot = GameObject.FindObjectsOfType<SpawnSpot>();
		Connect();
	}
	void Connect()
	{
		PhotonNetwork.ConnectUsingSettings("1.0.0");
	}
	void OnGUI()
	{
		GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString ());
	}
	void OnJoinedLobby()
	{
		PhotonNetwork.JoinRandomRoom ();
		Debug.Log ("JoinRandomRoom");
	}
	void OnPhotonRandomJoinFailed()
	{
		PhotonNetwork.CreateRoom (null);
		Debug.Log ("OnPhotonRandomJoinFailed");
	}
	void OnJoinedRoom()
	{
		Debug.Log ("OnJoinedRoom");
		SpawnMyPlayer ();
	}
	void SpawnMyPlayer()
	{
		SpawnSpot mySpawnSpots = spot[Random.Range(0, spot.Length)];
		GameObject myPlayerGo = (GameObject)PhotonNetwork.Instantiate("PlayerController", mySpawnSpots.transform.position, mySpawnSpots.transform.rotation, 0);
		standByCamera.gameObject.SetActive(true);
		((MonoBehaviour)myPlayerGo.GetComponent ("NetworkCharacter")).enabled = true;
		((MonoBehaviour)myPlayerGo.GetComponent ("FPSInputController")).enabled = true;
		((MonoBehaviour)myPlayerGo.GetComponent ("MouseLook")).enabled = true;
		((MonoBehaviour)myPlayerGo.GetComponent ("CharacterMotor")).enabled = true;
		myPlayerGo.transform.FindChild ("Main Camera").gameObject.SetActive(true);
	}
}
