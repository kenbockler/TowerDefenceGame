using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kontrollime, kas kokkupõrge toimus WaypointFollower'iga
        WaypointFollower follower = collision.GetComponent<WaypointFollower>();

        if (follower != null)
        {
            // Hävitame WaypointFollower'i objekti
            Destroy(follower.gameObject);

            // Vähendame elusid ja uuendame neid mängus
            int currentLives = Events.RequestLives(); // Pärib hetkel mängus olevate elude arvu
            currentLives--;
            // if (currentLives < 0) currentLives = 0;
            Events.SetLives(currentLives); // Uuendab elude arvu
        }
    }
}
