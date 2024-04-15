using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BanqueButtonScript : MonoBehaviour
{
    public Canvas canvaJeuBase;
    public Canvas canvaMagasin; 

    public void FaireApparaitreMagasin()
    {
        canvaJeuBase.gameObject.SetActive(false);
        canvaMagasin.gameObject.SetActive(true); 
    } 
    public void FermerMagasin()
    {
        canvaJeuBase.gameObject.SetActive(true);
        canvaMagasin.gameObject.SetActive(false);
    }
}
