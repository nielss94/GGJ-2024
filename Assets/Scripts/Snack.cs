using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SnackType
{
    Frikandel,
    Kaassouffle,
    Bamischijf,
    Mexicano,
    Bitterbal,
    Friet
}

public class Snack : MonoBehaviour
{
    [SerializeField] private SnackType snackType;
    
    public SnackType SnackType => snackType;
}
