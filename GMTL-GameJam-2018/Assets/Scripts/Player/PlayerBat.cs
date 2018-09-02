using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerBat : PlayerSubComponent {

    private enum BatPosition { None, Left = -1, Right = 1}

    [Header("Refs")]
    public Transform batPivot;
    public Transform batGraphics;
    public ParticleSystem batHitParticles;

    [Header("hitbox")]

    public LayerMask hitMask;
    public Vector2 hitBoxSize;
    public Vector2 hitBoxOffset;

    [Header("Animation")]
    public AnimationCurve batSwing;

    private BatPosition _currentBatPos = BatPosition.Left;
    private float _swingTime = -1;
    private bool _isSwining = false;

    private float _currentBatAngle;

    private List<Collider2D> _hitObjects;
    private Collider2D[] _hitResults;
    private ContactFilter2D _contactFilter;

    private const float ANGLE_LEFT = 80;
    private const float ANGLE_RIGHT = -260;

    public override void Init(PlayerController playerController)
    {
        base.Init(playerController);

        _currentBatAngle = _currentBatPos == BatPosition.Left ? ANGLE_LEFT : ANGLE_RIGHT;
        SetBatGrahpicsRotation(_currentBatAngle);

        _hitObjects = new List<Collider2D>();
        _hitResults = new Collider2D[16];

        _contactFilter = new ContactFilter2D();
        _contactFilter.SetLayerMask(hitMask);

        batHitParticles.transform.parent = null;
    }

	// Update is called once per frame
	public void Tick (Vector2? aim = null)
    {
        if (aim.HasValue)
        {
            float angle = Mathf.Atan2(aim.Value.y, aim.Value.x) * Mathf.Rad2Deg;
            Quaternion batRotation = Quaternion.Euler(0, 0, angle);
            batPivot.localRotation = batRotation;
        }
	}

    public void FixedTick()
    {
        if (_swingTime >= 0)
        {
            _swingTime += Time.deltaTime;

            float from = _currentBatPos == BatPosition.Left ? ANGLE_LEFT : ANGLE_RIGHT;
            float to = _currentBatPos == BatPosition.Left ? ANGLE_RIGHT : ANGLE_LEFT;

            _currentBatAngle = Mathf.LerpUnclamped(from, to, batSwing.Evaluate(_swingTime));
            SetBatGrahpicsRotation(_currentBatAngle);

            Vector2 boxPos = batGraphics.TransformPoint(hitBoxOffset);
            debugPosition.transform.position = boxPos;
            int numbHits = Physics2D.OverlapBox(boxPos, hitBoxSize, _currentBatAngle, _contactFilter, _hitResults);
            bool hitSomething = false;
            for (int i = 0; i < numbHits; i++)
            {
                Collider2D hit = _hitResults[i];
                if (hit.attachedRigidbody != null && !_hitObjects.Contains(hit) && hit.gameObject != _playerController.gameObject)
                {
                    hit.attachedRigidbody.velocity = batPivot.right * 5f;
                    _hitObjects.Add(hit);
                    hitSomething = true;

                    batHitParticles.transform.position = hit.attachedRigidbody.transform.position - Vector3.forward *5f;
                    batHitParticles.Play();
                }
            }
            if (hitSomething)
            {
                CameraShakerController.CameraShake();
            }

            if (_swingTime >= batSwing.Duration())
            {
                _swingTime = -1f;
                _isSwining = false;

                _hitObjects.Clear();


                if(_currentBatPos == BatPosition.Right)
                {
                    _playerController.playerAnimation.BatWobbleLeft();
                }
                else
                {
                    _playerController.playerAnimation.BatWobbleRight();
                }

                _currentBatPos = (BatPosition)(((int)_currentBatPos) * -1);
            }
        }
    }

    private void SetBatGrahpicsRotation(float angle)
    {
        batGraphics.localRotation = Quaternion.Euler(0, 0, angle); ;
    }

    public void SwingBat()
    {
        if(_swingTime < 0)
        {
            _swingTime = 0;
            _isSwining = true;
        }
    }
}
