using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VariantCardsScript : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; // Ref au SpriteRenderer de la carte 

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

    public class Carte  // Classe représentant une carte
    {
        public ValeurCarte valeur;
        public CouleurCarte couleur;
         
        public Carte(ValeurCarte valeur, CouleurCarte couleur)
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
}
