using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager Instance { get; private set; }

    #region enemyDeathParticles

    public GameObject particleEnemyDeath;
    public Vector3 scaleParticleEnemyDeath = new Vector3(1, 1, 1);
    private Vector3 _positionParticleEnemyDeath;
    public float particleEnemyXPosition = -1f;
    public float particleEnemyYPosition = -1f;
    public float particleEnemyZPosition = -1f;

    #endregion

    #region buildPlacedParticles

    public GameObject particleBuildPlaced;
    public Vector3 scaleParticleBuildPlaced = new Vector3(1, 1, 1);
    private Vector3 _positionParticleBuildPlaced;
    public float particleBuildXPosition = -1f;
    public float particleBuildYPosition = -1f;
    public float particleBuildZPosition = -1f;

    #endregion

    #region buildDestroyedParticles

    public GameObject particleBuildDestroyed;
    public Vector3 scaleParticleBuildDestroyed = new Vector3(1, 1, 1);
    private Vector3 _positionParticleBuildDestroyed;
    public float particleBuildDestroyedXPosition = -1f;
    public float particleBuildDestroyedYPosition = -1f;
    public float particleBuildDestroyedZPosition = -1f;

    #endregion

    #region boomfinExplodeParticles

    public GameObject boomfinParticle;
    public Vector3 boomfinScale;
    private Vector3 _positionBoomfin;
    public float boomfinParticleX;
    public float boomfinParticleY;
    public float boomfinParticleZ;

    #endregion

    #region healPlayerParticles

    public GameObject healParticle;
    public Vector3 healParticleScale;
    private Vector3 _positionPlayer;
    public float healParticleX;
    public float healParticleY;
    public float healParticleZ;

    #endregion

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        if (PlayerPrefs.GetInt("particlesActivated") == 1)
        {
            PlayDestroyedBuildParticles(new Vector3(0, 0, 0));
            PlayEnemyDeathParticles(new Vector3(0, 0, 0));
            PlayPlacedBuildParticles(new Vector3(0, 0, 0));
        }
    }

    public void PlayEnemyDeathParticles(Vector3 enemyPos)
    {
        _positionParticleEnemyDeath = enemyPos;

        if (particleEnemyXPosition != -1f)
        {
            _positionParticleEnemyDeath.x = particleEnemyXPosition;
        }

        if (particleEnemyYPosition != -1f)
        {
            _positionParticleEnemyDeath.y = particleEnemyYPosition;
        }

        if (particleEnemyZPosition != -1f)
        {
            _positionParticleEnemyDeath.z = particleEnemyZPosition;
        }

        var particle = Instantiate(particleEnemyDeath, _positionParticleEnemyDeath, Quaternion.identity);
        particle.transform.localScale = scaleParticleEnemyDeath;
    }

    public void PlayPlacedBuildParticles(Vector3 buildPos)
    {
        _positionParticleBuildPlaced = buildPos;
        if (particleBuildXPosition != -1f)
        {
            _positionParticleBuildPlaced.x = particleBuildXPosition;
        }

        if (particleBuildYPosition != -1f)
        {
            _positionParticleBuildPlaced.y = particleBuildYPosition;
        }

        if (particleBuildZPosition != -1f)
        {
            _positionParticleBuildPlaced.z = particleBuildZPosition;
        }

        var particle = Instantiate(particleBuildPlaced, _positionParticleBuildPlaced, Quaternion.identity);
        particle.transform.localScale = scaleParticleBuildPlaced;
    }

    public void PlayDestroyedBuildParticles(Vector3 buildPos)
    {
        _positionParticleBuildDestroyed = buildPos;
        if (particleBuildDestroyedXPosition != -1f)
        {
            _positionParticleBuildDestroyed.x = particleBuildDestroyedXPosition;
        }

        if (particleBuildDestroyedYPosition != -1f)
        {
            _positionParticleBuildDestroyed.y = particleBuildDestroyedYPosition;
        }

        if (particleBuildDestroyedZPosition != -1f)
        {
            _positionParticleBuildDestroyed.z = particleBuildDestroyedZPosition;
        }

        var particle = Instantiate(particleBuildDestroyed, _positionParticleBuildDestroyed, Quaternion.identity);
        particle.transform.localScale = scaleParticleBuildDestroyed;
    }

    public void PlayBoomfinExplode(Vector3 boomfinPos)
    {
        _positionBoomfin = boomfinPos;
        if (boomfinParticleX != -1f)
        {
            _positionBoomfin.x = boomfinParticleX;
        }

        if (boomfinParticleY != -1f)
        {
            _positionBoomfin.y = boomfinParticleY;
        }

        if (boomfinParticleZ != -1f)
        {
            _positionBoomfin.z = boomfinParticleZ;
        }

        var particle = Instantiate(boomfinParticle, _positionBoomfin, Quaternion.identity);
        particle.transform.localScale = boomfinScale;
    }

    public void PlayHealParticle(Vector3 healPos)
    {
        _positionPlayer = healPos;

        if (healParticleX != -1f)
        {
            _positionPlayer.x = healParticleX;
        }

        if (healParticleY != -1f)
        {
            _positionPlayer.y = healParticleY;
        }

        if (healParticleZ != -1f)
        {
            _positionPlayer.z = healParticleZ;
        }

        var particle = Instantiate(healParticle, _positionPlayer, Quaternion.identity);
        particle.transform.localScale = healParticleScale;
    }
}