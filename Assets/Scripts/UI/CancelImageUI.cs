using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelImageUI : MonoBehaviour
{
    [SerializeField] private GameObject _uiParticle;
    [SerializeField] private GameObject _uiParticleEndPos;
    [SerializeField] private GameObject _cancelImage;
    [SerializeField] private GameObject _cancelImageBigRect;
    public GameObject UiParticle => _uiParticle;
    public GameObject CancelImage => _cancelImage;
    public GameObject CancelImageBigRect => _cancelImageBigRect;
    public GameObject UiParticleEndPos => _uiParticleEndPos;
}
