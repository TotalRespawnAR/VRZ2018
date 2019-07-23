using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DetectedHandSubject : MonoBehaviour {

    public ARZroom RoomType;
    public bool IsGrabbeableItem;
    bool IsInHand;
    DetectedHand Hand;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Detected Collision with " + collision.gameObject.name);
    }

    public void DetachFromHand() {
        if (!IsInHand) return;
        if (IsGrabbeableItem)
        {
            this.transform.parent = null;
            IsInHand = false;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LoadyHand"))
        {
            if (IsGrabbeableItem) //then stick to it 
            {
               
                if (Hand == null)
                {
                    Hand = other.gameObject.GetComponentInParent<DetectedHand>();
                }
                if (!Hand.isHoldingSmthin)
                {
                    Hand.ObjInHand = this;
                    Hand.isHoldingSmthin = true;
                    this.transform.parent = other.gameObject.transform;
                    IsInHand = true;
                }
                else
                {
                    Debug.Log(" hand already has somthin in it");
                }
            }
            else {

                Debug.Log(" then get pushed by it the hand that is");
                GoToScene(RoomType);
            }
        }
    }
    void GoToScene(ARZroom argRoom)
    {
        if (IsGrabbeableItem) return;
        if (GameSettings.Instance == null)
        {
            Debug.LogError("no gamesettings");
        }
        else
        {
            GameSettings.Instance.Backroom = argRoom;
        }
        if (GameSettings.Instance.IsKingstonOn)
        {
            SceneManager.LoadScene("GameSoloKingston");
        }
        else
        {
            SceneManager.LoadScene("GameSolo");
        }
    }
}
