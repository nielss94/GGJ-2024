using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SnackType
{
    Frikandel,
    Kaassouffle,
    Bamischijf,
    Mexicano,
    Bitterballen,
    Friet
}

public class SnackStack : MonoBehaviour
{
    [SerializeField] private SnackType snackType;
    
    public SnackType SnackType => snackType;
}
