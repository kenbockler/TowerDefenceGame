using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kontrollime, kas kokkup�rge toimus WaypointFollower'iga
        WaypointFollower follower = collision.GetComponent<WaypointFollower>();

        if (follower != null)
        {
            // H�vitame WaypointFollower'i objekti
            Destroy(follower.gameObject);

            // V�hendame elusid ja uuendame neid m�ngus
            int currentLives = Events.RequestLives(); // P�rib hetkel m�ngus olevate elude arvu
            currentLives--;
            // if (currentLives < 0) currentLives = 0;
            Events.SetLives(currentLives); // Uuendab elude arvu
        }
    }
}
