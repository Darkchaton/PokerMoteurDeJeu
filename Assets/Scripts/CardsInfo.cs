using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardsInfo : MonoBehaviour
{ 
    public SpriteRenderer spriteRenderer; // R�f�rence au SpriteRenderer pour l'image de la carte

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

    public class Carte  // Classe repr�sentant une carte
    {
        public ValeurCarte valeur;
        public CouleurCarte couleur;

        // Constructeur = initialiser les valeurs de la carte en elle-m�me
        public Carte(ValeurCarte valeur, CouleurCarte couleur)
        {
            this.valeur = valeur;
            this.couleur = couleur;
        }
    }

    public void AppliquerCarte(Carte carte)
    {  
        // Charge l'image de la carte appropri�e en fonction de sa valeur et sa couleur
        string cheminImage = carte.valeur.ToString() + "_" + carte.couleur.ToString();
         
        Debug.Log(cheminImage);

        spriteRenderer.sprite = Resources.Load<Sprite>(cheminImage); 
    }
}
