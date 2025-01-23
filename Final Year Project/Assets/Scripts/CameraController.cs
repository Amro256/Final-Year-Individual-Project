
using UnityEngine;
using Cinemachine;
using Unity.Mathematics;

public static class CameraController // Static class so it can be accessed by the player script
{
    private static float CurrentCamHeight; //This will track the camer's vertical position
    private static float SmoothVelocity;


    //Create a new method to initalise the camera's height based on the player's position

    public static void initaliseCamera(CinemachineVirtualCamera virtualCamera, Transform player)
    {
        //Check if the virtual cam is empty or not

        if(virtualCamera != null)
        {
            //if it's not empty, set the current cam Y-position to the Player's Y-position
            CurrentCamHeight = player.position.y;
        }
    }


    //Method to lock the camer's position during jumps
    public static void UpdateCamera(CinemachineVirtualCamera virtualCamera, Transform player, bool isGrounded, float SmoothTime) 
    {

        if(virtualCamera == null || player == null) return;

        var transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();

        //Check if grounded

        if(isGrounded)
        {
            float targetHeight = player.position.y;
            CurrentCamHeight = Mathf.SmoothDamp(CurrentCamHeight, targetHeight, ref SmoothVelocity, SmoothTime);
        }

        //Lock camera postion
        var offset = transposer.m_FollowOffset;
        offset.y = CurrentCamHeight;
        transposer.m_FollowOffset = offset;
    }
   
}
