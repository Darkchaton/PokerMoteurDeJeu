using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static CardsInfo;

public class ScriptBoutonGenerer : MonoBehaviour
{
    public GameObject[] cartes; // Tableau d'objets carte à modifier
    public Carte[] toutesLesCartes; // Tableau de toutes les cartes disponibles

    public void GenererCartes()
    {
        // Pour chaque objet carte dans le tableau
        foreach (GameObject carteObjet in cartes)
        {
            // Sélectionne une carte au hasard
            Carte carteAleatoire = toutesLesCartes[Random.Range(0, toutesLesCartes.Length)];

            // Chercher le script de la carte
            CardsInfo scriptCarte = carteObjet.GetComponent<CardsInfo>();

            // Applique la valeur et la couleur de la carte aléatoire
            scriptCarte.AppliquerCarte(carteAleatoire);
        }
    }
}
