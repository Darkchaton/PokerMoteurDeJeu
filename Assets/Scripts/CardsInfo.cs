using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class CardsInfo : MonoBehaviour 
{ 
    public SpriteRenderer spriteRenderer; // Ref au SpriteRenderer de la carte
    public TextMeshProUGUI garderText; //Ref au texte Changer

    public enum ValeurCarte
    {
        As,
        Deux,
        Trois,
        Quatre,
        Cinq,
        Six,
        Sept,
        Huit,
        Neuf,
        Dix,
        Valet,
        Dame,
        Roi
    }
    public enum CouleurCarte
    {
        Pique,
        Coeur,
        Trefle,
        Carreau
    }

    public class Carte  
    {
        public ValeurCarte valeur;
        public CouleurCarte couleur; 
        public Carte(ValeurCarte valeur, CouleurCarte couleur) //Constructeur
        {
            this.valeur = valeur;
            this.couleur = couleur;
        }
    }

    public void AppliquerCarte(Carte carte)
    {   
        string cheminImage = carte.valeur.ToString() + "_" + carte.couleur.ToString();
         
        spriteRenderer.sprite = Resources.Load<Sprite>(cheminImage); 
    } 

    private void OnMouseEnter()
    { 
        garderText.gameObject.SetActive(true);
    } 
    private void OnMouseExit()
    { 
        garderText.gameObject.SetActive(false);
    }

    //private void OnMouseUp()
    //{
    //    Debug.Log("Carte gardée");
    //    garderText.gameObject.SetActive(true);
    //}

    public void Clique()
    {
        Debug.Log("Carte gardée");
        garderText.gameObject.SetActive(true);
    }
}
