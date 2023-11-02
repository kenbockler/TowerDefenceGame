using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerBuilder : MonoBehaviour
{

    public Color AllowColor;
    public Color BlockColor;

    private TowerData CurrentTowerData;

    private void Awake()
    {
        Events.OnTowerSelected += TowerSelected;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        Events.OnTowerSelected -= TowerSelected;
    }

    void Update()
    {
        //Reposition the gameobject to mouse coordinates. 
        //Round the coordinates to make it snap to a grid.

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector3(
            Mathf.Round(mousePos.x - 0.5f) + 0.5f, 
            Mathf.Round(mousePos.y - 0.5f) + 0.5f, 
            0);
        transform.position = mousePos;

        //Verify that building area is free of other towers. 
        //By using a static overlap method from Physics2D class we can make this work without collider and a 2d rigidbody.

        bool free = IsFree(transform.position);

        //Tint the sprite to green or red accordingly.
        if (free)
        {
            TintSprite(AllowColor);
        }
        else
        {
            TintSprite(BlockColor);
        }   


        //Call the build method when the player presses left mouse button 

        if (Input.GetMouseButtonDown(0))
        {
            Build();
        }
        if (Input.GetMouseButtonDown(1))
        {
            gameObject.SetActive(false);
        }

    }

    void TintSprite(Color color)
    {
        SpriteRenderer[] renderers = gameObject.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer renderer in renderers)
        {
            renderer.color = color;
        }
    }

    bool IsFree(Vector3 pos)
    {
        Collider2D[] overlaps = Physics2D.OverlapCircleAll(pos, 0.45f);

        if (Events.RequestGold() < CurrentTowerData.Cost) return false;

        foreach (Collider2D overlap in overlaps)
        {
            if (!overlap.isTrigger) return false;

        }
        return true;
    }

    private void TowerSelected(TowerData data)
    {
        CurrentTowerData = data;
        gameObject.SetActive(true);
    }

    void Build()
    {
        // Kontrolli, kas ala on vaba
        if (!IsFree(transform.position)) return;

        // Kontrolli, kas hiirekursor on UI elemendi peal
        if (EventSystem.current.IsPointerOverGameObject()) return;

        // Küsi mängija praegust raha
        int currentMoney = Events.RequestGold();

        // Kontrolli, kas mängijal on piisavalt raha
        if (currentMoney < CurrentTowerData.Cost) return;

        // Vähenda mängija raha ja uuenda seda
        currentMoney -= CurrentTowerData.Cost;
        Events.SetGold(currentMoney);

        // Ehita torn
        GameObject towerGameObject = GameObject.Instantiate(CurrentTowerData.TowerPrefab, transform.position, Quaternion.identity, null);
        Tower tower = towerGameObject.GetComponent<Tower>();
        if (tower != null)
        {
            tower.TowerData = CurrentTowerData;
        }
        else
        {
            Debug.LogError("Tower komponenti ei leitud!");
        }

        // Peida ehitaja
        gameObject.SetActive(false);
    }

}
