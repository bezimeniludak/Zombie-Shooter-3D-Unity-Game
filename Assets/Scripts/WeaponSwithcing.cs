using System;
using UnityEngine;
using UnityEngine.Audio;

public class WeaponSwithcing : MonoBehaviour
{
    [SerializeField] private int _selectedWeapon = 0;

    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager._gameState != GameManager.GameState.running)
            return;
        int prevoiusSelectedWeapon = _selectedWeapon;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (_selectedWeapon >= transform.childCount - 1)
                _selectedWeapon = 0;
            else
                _selectedWeapon++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (_selectedWeapon <= 0)
                _selectedWeapon = transform.childCount - 1;
            else
                _selectedWeapon--;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _selectedWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _selectedWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _selectedWeapon = 2;
        }

        if (prevoiusSelectedWeapon != _selectedWeapon)
        {
            SelectWeapon();
        }
    }

    private void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == _selectedWeapon)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
            i++;

        }
    }
}
