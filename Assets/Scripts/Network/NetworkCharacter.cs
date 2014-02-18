using UnityEngine;
using System.Collections;

public class NetworkCharacter :  Photon.MonoBehaviour
{
	Vector3 realPosition = Vector3.zero;
	Quaternion realRotation = Quaternion.identity;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (photonView.isMine)
		{
		} 
		else
		{
			transform.position = Vector3.Lerp(transform.position, realPosition, 0.1f);
			transform.rotation = Quaternion.Lerp(transform.rotation, realRotation, 0.1f);
		}
	}
	public void OnPhotonSerializeView(PhotonStream steam, PhotonMessageInfo info)
	{
		if (steam.isWriting)
		{
			steam.SendNext(transform.position);
			steam.SendNext(transform.rotation);
		}
		else
		{
			realPosition = (Vector3)steam.ReceiveNext();
			realRotation = (Quaternion)steam.ReceiveNext();
		}
	}
}
