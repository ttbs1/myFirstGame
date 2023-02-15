using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform Followplatform;
     public Transform Player;
 
 
     void Update ()
     {
         Followplatform.position = new Vector3(Player.position.x +3, 0, -1);
     }

}