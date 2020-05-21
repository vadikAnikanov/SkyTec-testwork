using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPoolModel : MonoBehaviour
{
    private List<Transform> _bulletList;

    private int _currentBulletInList = 0;
    [SerializeField]
    private int _numberBulletsInOneShot;
   

    void Awake()
    {
        _bulletList = new List<Transform>();
        _bulletList.AddRange(GetComponentsInChildren<Transform>());
        _bulletList.RemoveAt(0);
        foreach (Transform bullet in _bulletList)
            bullet.gameObject.SetActive(false);
    }

    public void Shoot()
    {
        float startAngle = -45;

        int counterBulletsInThisShot = 0;

        for(int i =_currentBulletInList;i<_bulletList.Count-1;i++)
        {


            counterBulletsInThisShot++;

            if (counterBulletsInThisShot >= _numberBulletsInOneShot)
            {
                _currentBulletInList = i;
                break;
            }

            _bulletList[i].gameObject.SetActive(true);

            _bulletList[i].localPosition = new Vector3(0, 0, 0);
            _bulletList[i].localRotation = new Quaternion(0, 0, 0, 1);
            _bulletList[i].transform.Rotate(transform.up, startAngle);
            startAngle += 11;

            Vector3 velocity = _bulletList[i].transform.forward * 4f;
            velocity.y = 0;
            _bulletList[i].GetComponent<Rigidbody>().velocity = velocity;

            _bulletList[i].parent = null;

        }

        if (_currentBulletInList > (_bulletList.Count-1) - _numberBulletsInOneShot)
        {
            _currentBulletInList = 0;
        }
    }
}
