// @Author Nabil Lamriben ©2017
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MistMover : MonoBehaviour {
    /// <summary>
    /// this should be attached to GameMistObj not EditorMist
    /// set game time on start
    /// waits to be activated
    /// </summary>

   public Transform mistDestination;
    Vector3 _MistDestinationPosition;
    float originalHeight=0f;
 
    float _gameTime = 60f;
    //public void SetMistEnd(GameObject argMistEnd)
    //{
    //    this._mistEndObj = argMistEnd;
    //    _MistDestinationPosition = new Vector3(
    //        _mistEndObj.transform.position.x,
    //        this.gameObject.transform.position.y,
    //        _mistEndObj.transform.position.z);
    //}

    public void StartMistMove(Vector3 argTohere) {
            if (_gameTime<=0.1) {
            DebugConsole.print("Gametime is Wrong!!!");
        }
        else {
            DebugConsole.print("Mist Is Moving to");

            _MistDestinationPosition = new Vector3(
                argTohere.x,
                originalHeight,
               argTohere.z);

            DebugConsole.print("here x=  " + _MistDestinationPosition.x);
            DebugConsole.print("here y =  " + _MistDestinationPosition.y);
            DebugConsole.print("here z=  " + _MistDestinationPosition.z);

            StartCoroutine(MoveOverSeconds(_gameTime));

        }
    }

    void Start () {

        //// originalHeight = gameObject.transform.position.y;
        // Logger.Debug("Mist move time =  "+ _gameTime);
        // Logger.Debug("in start mistx =  " + transform.position.x);
        // Logger.Debug("in start misty =  " + transform.position.y);
        // Logger.Debug("in start mistz =  " + transform.position.z);

        StartCoroutine(MoveMistOverSeconds(100));
    }


    public IEnumerator MoveMistOverSeconds(float seconds)
    {
        float elapsedTime = 0;
        Vector3 startingPos = this.gameObject.transform.position;
        while (elapsedTime < seconds)
        {
            transform.position = Vector3.Lerp(startingPos, mistDestination.position, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = mistDestination.position;
    }
    public IEnumerator MoveOverSeconds(float seconds)
    {
        float elapsedTime = 0;
        Vector3 startingPos = this.gameObject.transform.position;
        while (elapsedTime < seconds)
        {
            transform.position = Vector3.Lerp(startingPos, _MistDestinationPosition, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = _MistDestinationPosition;
    }
}
