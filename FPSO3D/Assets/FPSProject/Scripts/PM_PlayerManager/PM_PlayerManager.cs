using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PM_PlayerManager : Singleton<PM_PlayerManager>
{
    [SerializeField, Header("Main Player")] PM_Player playerOne = null;
    Dictionary<int, PM_Player> allPlayers = new Dictionary<int, PM_Player>();

    public PM_Player PlayerOne => playerOne;
    bool PlayerExist(int _id) => allPlayers.ContainsKey(_id);

    protected override void Awake() => base.Awake();
    public PM_Player GetPlayer(int _id)
    {
        if (PlayerExist(_id))
            return allPlayers[_id];
        return null;
    }

    public void AddPlayer(PM_Player _player) => HandlePlayer(true, _player);
    public void RemovePlayer(PM_Player _player) => HandlePlayer(false, _player);

    void HandlePlayer(bool _addPlayer, PM_Player _player)
    {
        if (_addPlayer && !PlayerExist(_player.ID)) allPlayers.Add(_player.ID, _player);
        else if (!_addPlayer && PlayerExist(_player.ID)) allPlayers.Remove(_player.ID);
    }

}
