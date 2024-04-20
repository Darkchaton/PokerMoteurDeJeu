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

    private bool carteActive = false;
    private static int nombreCartesGardees = 0;
    private const int nombreCartesMaxGardees = 3; 

    public Carte carte;
    private ScriptBoutonGenerer scriptBoutonGenerer;

    void Start()
    {
        nombreCartesGardees = 0;
    }
    private void Awake()
    {
        scriptBoutonGenerer = FindObjectOfType<ScriptBoutonGenerer>();
    }

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

    public void AppliquerCarteWithoutAffectingCard(Carte carte)
    {
        AppliquerCarte(carte);
    }

        private void OnMouseEnter()
    {
        if (nombreCartesGardees < nombreCartesMaxGardees)
        {  
            garderText.gameObject.SetActive(true);
        }
    } 

    private void OnMouseDown()
    {  
        if (carteActive)
        {
            if (nombreCartesGardees < nombreCartesMaxGardees)
            {
                carteActive = false;
                nombreCartesGardees--;
                garderText.gameObject.SetActive(false); 
            }
        }
        else
        {
            if (nombreCartesGardees < nombreCartesMaxGardees)
            {
                carteActive = true;
                nombreCartesGardees++;
                garderText.gameObject.SetActive(true);
            }
        }
    } 

    private void OnMouseExit()
    {
        if (!carteActive)
        {
            garderText.gameObject.SetActive(false);
        } 
    }
    
}
