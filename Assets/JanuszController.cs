using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JanuszController : MonoBehaviour, IPointerDownHandler
{

    [SerializeField] private BoxCollider2D JanuszBox;
    [SerializeField] private Animator JanuszAnimator;

    public void OnPointerDown(PointerEventData eventData)
    {
        JanuszAnimator.SetTrigger("Outplayed");
        JanuszBox.enabled = false;
    }

}
