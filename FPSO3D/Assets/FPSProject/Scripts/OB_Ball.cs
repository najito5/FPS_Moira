using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OB_Ball : MonoBehaviour
{
    PM_Player player;

    private void Awake()
    {
        transform.localScale = Vector3.zero;
        GetComponent<Collider>().enabled = false;
    }

    public void Init(PM_Player _playerRef) => player = _playerRef;

    public void Launch()
    {
        if (!player.GetSettings.IsMaxPower()) return;
        transform.parent = null;
        player = null;
    }

    private void Update()
    {
        if (player)
        {
            Oscillation();
            transform.localScale = new Vector3(player.GetSettings.Power, player.GetSettings.Power, player.GetSettings.Power) / 100;
        }
        else
        {
            GetComponent<Collider>().enabled = true;
            transform.position += transform.forward;
        }
    }

    void Oscillation() => transform.position += new Vector3(0, Mathf.Sin(Time.time) * -0.003f, 0);/**speed*/

    private void OnTriggerExit(Collider other)
    {
        if (!player) Destroy(gameObject);
    }
}

