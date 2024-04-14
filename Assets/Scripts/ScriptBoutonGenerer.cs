using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static CardsInfo;

public class ScriptBoutonGenerer : MonoBehaviour
{
    public GameObject[] cartes; // Tableau d'objets carte à modifier
    public Carte[] toutesLesCartes; // Tableau de toutes les cartes disponibles
    public AudioSource audioSource;
    public Button generateButton;
    public TextMeshProUGUI premierTexte;
    public TextMeshProUGUI deuxiemeTexte;
    private int clickCount = 0;
    public int maxClicks = 3; 

    void Start()
    { 
        GenererToutesLesCartes();
    }

    public void GenererCartes()
    {
        if (clickCount < maxClicks)
        {
            //Son de brassage de cartes 
            audioSource.Play();

            clickCount++; 
             
            foreach (GameObject carteObjet in cartes)
            { 
                //Random
                Carte carteAleatoire = toutesLesCartes[Random.Range(0, toutesLesCartes.Length)];

                // Chercher le script de la carte
                CardsInfo scriptCarte = carteObjet.GetComponent<CardsInfo>();

                // Applique la valeur et la couleur de la carte aléatoire
                scriptCarte.AppliquerCarte(carteAleatoire);
            }
 
            if (clickCount >= maxClicks) //Désactiver Bouton Générer après 3 fois
            { 
                generateButton.enabled = false;

                premierTexte.gameObject.SetActive(false);
                deuxiemeTexte.gameObject.SetActive(true);
            }
        }
    }

    void GenererToutesLesCartes()
    {
        toutesLesCartes = new Carte[52];

        int index = 0;
        foreach (CardsInfo.ValeurCarte valeur in System.Enum.GetValues(typeof(CardsInfo.ValeurCarte)))
        {
            foreach (CardsInfo.CouleurCarte couleur in System.Enum.GetValues(typeof(CardsInfo.CouleurCarte)))
            { 
                toutesLesCartes[index] = new Carte(valeur, couleur);
                Debug.Log(valeur + "," + couleur); 
                index++;
            }
        } 
    } 
}
