﻿using UnityEngine;

public class TalkClickStarter : MonoBehaviour
{
    public GameDirector game;

#if UNITY_ANDROID || UNITY_IOS
    private void Update()
    {
        if (!game.canControl)
            return;

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Ended)
            {
                game.SetSpeaker(gameObject.transform.parent.gameObject, gameObject);
                game.StartDialogue();
                return;
            }
        }
    }
#else
    private void OnMouseUp()
    {
        if (game.canControl)
        {
            game.SetSpeaker(gameObject.transform.parent.gameObject, gameObject);
            game.StartDialogue();
        }
    }
#endif
}
