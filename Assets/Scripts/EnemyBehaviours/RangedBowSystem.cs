using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedBowSystem : MonoBehaviour
{
    [SerializeField] private GameObject _arrowPrefab;

    private RangedBehaviour _rangedBehaviour;

    private Transform _arrowsParent;
    
    [SerializeField] private List<GameObject> _allArrows = new List<GameObject>();

    [SerializeField] private float _timeToDestroyArrow = 1f;
    [SerializeField] private AudioSource _fireAudio;
    
    private void Start()
    {
        _rangedBehaviour = gameObject.transform.parent.GetComponent<RangedBehaviour>();
        _arrowsParent = GameObject.Find("Arrows").transform;
    }

    private void Update()
    {
        if (_rangedBehaviour.isAtacking)
        {
            GameObject arrow;
            arrow = Instantiate(_arrowPrefab, transform.position, Quaternion.identity, _arrowsParent);
            _fireAudio.Play();
            _allArrows.Add(arrow);
            _rangedBehaviour.isAtacking = false;
            StartCoroutine(Destroy());
        }
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(_timeToDestroyArrow);
        Destroy(_allArrows[0]);
        _allArrows.RemoveAt(0);
    }

    private void OnDisable()
    {
        while(_allArrows.Count != 0)
        {
            Destroy(_allArrows[0]);
            _allArrows.RemoveAt(0);
        }
    }
}
