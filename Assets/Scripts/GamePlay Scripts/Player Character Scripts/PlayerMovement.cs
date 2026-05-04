//using UnityEngine;
//using UnityEngine.InputSystem;

//public class PlayerMovement : MonoBehaviour
//{
//    public float moveSpeed;
//    public Transform playerTransform;
//    private Vector2 v;

//    public void OnMove(InputValue value)
//    {
//        v = value.Get<Vector2>();
//    }
    
//    void Update()
//    {
//        playerTransform.position = new Vector3(playerTransform.position.x + (v.x * moveSpeed), playerTransform.position.y + (v.y * moveSpeed), playerTransform.position.z);
//    }
//}