using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventItemArtifact1 : MonoBehaviour, IInteraction
{
    public bool Interaction()
    {
        GameManager.Instance.UIManager.ShowInfo("Ë«±¶±¬ÂÊàË·­Ìì£¡");
        return false;
    }
}
