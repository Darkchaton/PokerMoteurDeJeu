using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MagasinItemScript : MonoBehaviour
{
    public SoumettreButtonScript soumettreButtonScript; 
    public TextMeshProUGUI textePiecesOr;
    public TextMeshProUGUI[] texteNbrItemsReliques;

    bool ItemUnClique = false;
    bool ItemDeuxClique = false;
    bool ReliqueUnClique = false;
    bool ReliqueDeuxClique = false;

    bool ItemUnChoisi = false;
    bool ItemDeuxChoisi = false;
    bool ReliqueUnChoisi = false;
    bool ReliqueDeuxChoisi = false;

    public void ItemUn()
    {
        ItemUnClique = true;
        ItemDeuxClique = false;
        ReliqueUnClique = false;
        ReliqueDeuxClique = false;

        Debug.Log("Item un choisi");
    }
    public void ItemDeux()
    {
        ItemUnClique = false;
        ItemDeuxClique = true;
        ReliqueUnClique = false;
        ReliqueDeuxClique = false;

        Debug.Log("Item deux choisi");
    }
    public void ReliqueUn()
    {
        ItemUnClique = false;
        ItemDeuxClique = false;
        ReliqueUnClique = true;
        ReliqueDeuxClique = false;

        Debug.Log("Relique un choisi");
    }
    public void ReliqueDeux()
    {
        ItemUnClique = false;
        ItemDeuxClique = false;
        ReliqueUnClique = false;
        ReliqueDeuxClique = true;

        Debug.Log("Relique deux choisi");
    }

    public void Acheter()
    {
        int coutItem = 0;
        int typeItem = 0;

        if (ItemUnClique == true)
        {
            coutItem = 5;
            typeItem = 1; 
            ItemUnChoisi = true;
        } 
        if (ItemDeuxClique == true)
        {
            coutItem = 50;
            typeItem = 2;
            ItemDeuxChoisi = true;
        } 
        if (ReliqueUnClique == true)
        {
            coutItem = 130;
            typeItem = 3;
            ReliqueUnChoisi = true;
        } 
        if (ReliqueDeuxClique == true)
        {
            coutItem = 150;
            typeItem = 4;
            ReliqueDeuxChoisi = true;
        }

        if (soumettreButtonScript.piecesOr >= coutItem)
        {
            soumettreButtonScript.piecesOr -= coutItem;
            textePiecesOr.text = soumettreButtonScript.piecesOr.ToString();
            soumettreButtonScript.MettreAJourNbrItems(typeItem); 

            if(ItemUnChoisi == true)
            { 
                texteNbrItemsReliques[0].text = soumettreButtonScript.nbrItemsItemUn.ToString();
            }
            if (ItemDeuxChoisi == true)
            {
                texteNbrItemsReliques[1].text = soumettreButtonScript.nbrItemsItemDeux.ToString();
            }
            if (ReliqueUnChoisi == true)
            {
                texteNbrItemsReliques[2].text = soumettreButtonScript.nbrItemsReliqueUn.ToString();
            }
            if (ReliqueDeuxChoisi == true)
            {
                texteNbrItemsReliques[3].text = soumettreButtonScript.nbrItemsReliqueDeux.ToString();
            }
        }
        else
        {
            Debug.Log("Vous n'avez pas assez d'argent pour acheter cet article !");
        } 
    }

}
