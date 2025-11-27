using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerObject : MonoBehaviour
{
    [SerializeField] private GameObject _mineralShard;
    private Transform _startPos;
    private Transform _endPos;

    private bool _hasMineral = false;

    private void Awake()
    {
        _mineralShard.SetActive(false);
    }

    public void SetRoute(Transform p_start, Transform p_end)
    {
        _startPos = p_start;
        _endPos = p_end;
    }


    public void RefreshVisual(float p_progress)
    {
        HandleMineralShard(p_progress);
        HandleMove(p_progress);
    }

    private void HandleMineralShard(float p_progress)
    {
        if (p_progress >= 0.5f && !_hasMineral)
        {
            _hasMineral = true;
            _mineralShard.SetActive(true);
        }
        else if (p_progress < 0.5f && _hasMineral)
        {
            _hasMineral = false;
            _mineralShard.SetActive(false);
        }
    }

    private void HandleMove(float p_progress)
    {
        if (p_progress <= 0.5f)
        {
            float t = p_progress / 0.5f;
            transform.position = Vector3.Lerp(_startPos.position, _endPos.position, t);
        }
        else
        {
            float t = (p_progress - 0.5f) / 0.5f;
            transform.position = Vector3.Lerp(_endPos.position, _startPos.position, t);
        }
    }
}
