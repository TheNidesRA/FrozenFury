using System;
using System.Collections;
using System.Collections.Generic;
using Enemies;
using GridSystem;
using TMPro;
using UnityEngine;

public class ComunicacionGridCanvas : MonoBehaviour
{
    public GameObject BuildButton;

    public GameObject EditBuild;
    private RectTransform _rectTransformEditBuild;
    public GameObject buildPlaceVisual;

    private Transform build;
    public TextMeshProUGUI _texto;

    public Vector3 insidePosition;
    public Vector3 outsidePosition;
    public Vector3 outOfCanvasPosition;

    public float transitionTime;
    public SideBarButtonActions BuildButtonActions;
    private LTDescr tween;


    /////////////////////////////////////////////////////////////////////////////

    public TextMeshProUGUI _buildUpdateText;
    public GameObject _buildUpdateObjectPlace;
    public GameObject _buildUpdateConteiner;
    public BuildManagement buildManagement;
    private Transform buildUpdate;
    private PlacedBuild _placedBuild;
    private RectTransform _rectTransformBuildUpdate;

    public UpdateUIStats UpdateStatsSettingBuild;
    public UpdateUIStats UpdateStatsEditBuild;
    public UpgradePlayer UpdateStatsPlayer;

    public RectTransform playerStatsRect;

    private bool rondaComenzada = false;


    private Vector3 insidePositionStats;
    private Vector3 outsidePositionStats;

    public PlacedBuild ExposedPlacedBuild
    {
        get { return _placedBuild; }
    }

    public void OnDestroy()
    {
        GridBuildingSystem.Instance.OnSelectedChanged -= Instance_OnSelectedChanged;
        GridBuildingSystem.Instance.OnObjectRemovePosition -= Instance_OnObjectRemovePosition;
        GridBuildingSystem.Instance.OnObjectPlaced -= Instance_OnObjectPlaced;
        GridBuildingSystem.Instance.OnObjectSetPosition -= Instance_OnObjectSetPosition;
        GridBuildingSystem.Instance.OnBuildSelected -= Instance_OnBuildSelected;
        WaveController._instance.OnRoundActive -= Instance_OnRoundActive;
    }

    private void Start()
    {
        _rectTransformEditBuild = EditBuild.GetComponent<RectTransform>();
        _rectTransformBuildUpdate = _buildUpdateConteiner.GetComponent<RectTransform>();
        RefreshVisual();
        GridBuildingSystem.Instance.OnSelectedChanged += Instance_OnSelectedChanged;
        GridBuildingSystem.Instance.OnObjectRemovePosition += Instance_OnObjectRemovePosition;
        GridBuildingSystem.Instance.OnObjectPlaced += Instance_OnObjectPlaced;
        GridBuildingSystem.Instance.OnObjectSetPosition += Instance_OnObjectSetPosition;
        GridBuildingSystem.Instance.OnBuildSelected += Instance_OnBuildSelected;
        WaveController._instance.OnRoundActive += Instance_OnRoundActive;
        insidePositionStats = insidePosition;
        insidePositionStats.z = -400f;
        outsidePositionStats = outsidePosition;
        outsidePositionStats.z = -400f;
    }


    private void Instance_OnSelectedChanged(object sender, EventArgs eventArgs)
    {
        RefreshVisual();
    }

    private void Instance_OnObjectRemovePosition(object sender, EventArgs eventArgs)
    {
        EnableBuildMoving();
    }

    private void Instance_OnObjectPlaced(object sender, EventArgs eventArgs)
    {
        FinishBuilding();
    }

    private void Instance_OnObjectSetPosition(object sender, EventArgs eventArgs)
    {
        SetBuildPosition();
    }


    private void Instance_OnBuildSelected(object sender, PlacedBuild eventArgs)
    {
        if (rondaComenzada)
        {
            Debug.Log("No lo sacamos ya ha empezado");
            return;
        }

       
        RefreshVisualUpdateBuild(eventArgs);
        _buildUpdateConteiner.SetActive(true);
        buildManagement.CheckIfInteractable();
        tween = LeanTween.move(_rectTransformBuildUpdate, insidePosition, transitionTime).setEaseInCubic();
        BuildButtonActions.outBuildPosition();
    }


    private void Instance_OnRoundActive(object sender, bool eventArgs)
    {
        rondaComenzada = eventArgs;
        if (eventArgs)
            RoundStarted();
        
        else
        {
            RoundEnded();
        }
    }


    public void FinishBuilding()
    {
        // BuildButton.SetActive(true);
        BuildButtonActions.returnInside();
        tween = LeanTween.move(_rectTransformEditBuild, outsidePosition, transitionTime).setEaseOutCubic()
            .setOnComplete(() => { EditBuild.SetActive(false); });
    }


    public void SetBuildPosition()
    {
        // BuildButton.SetActive(false);
        BuildButtonActions.outBuildPosition();
        EditBuild.SetActive(true);

        tween = LeanTween.moveX(_rectTransformEditBuild.GetComponent<RectTransform>(), insidePosition.x, transitionTime)
            .setEaseInCubic(); //.setOnComplete(() => {EditBuild.SetActive(true); });
    }

    public void EnableBuildMoving()
    {
        tween = LeanTween.move(_rectTransformEditBuild, outsidePosition, transitionTime).setEaseOutCubic()
            .setOnComplete(() => { EditBuild.SetActive(false); });
    }


    public void RemoveBuild()
    {
        if (_placedBuild == null) return;
        GridBuildingSystem.Instance.RemoveBuild(_placedBuild);
        RefreshVisualUpdateBuild(null);
    }

    public void HideUpdateBuild()
    {
        tween = LeanTween.move(_rectTransformBuildUpdate, outsidePosition, transitionTime).setEaseOutCubic()
            .setOnComplete(() => { _buildUpdateConteiner.SetActive(false); });
        // BuildButton.SetActive(true);
        _placedBuild.HideArea();
        BuildButtonActions.returnInside();
    }


    private void RefreshVisual()
    {
        if (build != null)
        {
            Destroy(build.gameObject);
            build = null;
        }

        BuildingSO placedObjectTypeSO = GridBuildingSystem.Instance.buildingSo;
        if (placedObjectTypeSO != null)
        {
            build = Instantiate(placedObjectTypeSO.canvasVisual, Vector3.zero, Quaternion.identity,
                buildPlaceVisual.transform);

            UpdateStatsSettingBuild.UpdateStatsText(placedObjectTypeSO);


            build.localPosition = new Vector3(0, 0, -150);
            build.localEulerAngles = placedObjectTypeSO.canvasVisual.rotation.eulerAngles;
            SetLayerRecursive(build.gameObject, 5);
            _texto.text = placedObjectTypeSO.name;
        }
        else
        {
            tween = LeanTween.move(_rectTransformEditBuild, outsidePosition, transitionTime).setEaseOutCubic()
                .setOnComplete(() => { EditBuild.SetActive(false); });
            // BuildButton.SetActive(true);
            if (!GridBuildingSystem.Instance.buildMenu)
                BuildButtonActions.returnInside();
        }
    }


    private void RefreshVisualUpdateBuild(PlacedBuild placedBuild)
    {
        if (buildUpdate != null)
        {
            _placedBuild.HideArea();
            Destroy(buildUpdate.gameObject);
            buildUpdate = null;
            _placedBuild = null;
        }


        if (placedBuild != null)
        {
            _placedBuild = placedBuild;
            buildUpdate = Instantiate(placedBuild.BuildingSo.canvasVisual, Vector3.zero, Quaternion.identity,
                _buildUpdateObjectPlace.transform);


            UpdateStatsEditBuild.UpdateStatsText(placedBuild);


            if (_placedBuild.Area != null)
            {
                _placedBuild.ShowArea();
            }
            
            buildUpdate.localPosition = new Vector3(0, 0, -60);
            buildUpdate.localEulerAngles = placedBuild.BuildingSo.canvasVisual.rotation.eulerAngles;
            ;
            SetLayerRecursive(buildUpdate.gameObject, 5);
            _buildUpdateText.text = placedBuild.BuildingSo.name;
        }
        else
        {
            _placedBuild.HideArea();
            tween = LeanTween.move(_rectTransformBuildUpdate, outsidePosition, transitionTime).setEaseOutCubic()
                .setOnComplete(() => { _buildUpdateConteiner.SetActive(false); });
            //BuildButton.SetActive(true);
            if (!GridBuildingSystem.Instance.buildMenu)
                BuildButtonActions.returnInside();
        }
    }


    private void RoundStarted()
    {
        _placedBuild.HideArea();
        if (EditBuild.activeSelf)
        {
            Debug.Log("Hello");
            return;
        }

        HidePlayerStats();
        BuildButtonActions.outBuildPosition();
        LeanTween.move(_rectTransformBuildUpdate, outsidePosition, transitionTime).setEaseOutCubic()
            .setOnComplete(() => { _buildUpdateConteiner.SetActive(false); });
        // BuildButton.SetActive(true);
        // BuildButton.SetActive(false);
        // EditBuild.SetActive(false);
    }

    private void RoundEnded()
    {
        if (EditBuild.activeSelf) return;
        BuildButtonActions.returnInside();
    }


    private void SetLayerRecursive(GameObject targetGameObject, int layer)
    {
        targetGameObject.layer = layer;
        foreach (Transform child in targetGameObject.transform)
        {
            SetLayerRecursive(child.gameObject, layer);
        }
    }

    private void HidePlayerStatsTween()
    {
        LeanTween.move(playerStatsRect, outsidePositionStats, transitionTime).setEaseOutCubic().setOnComplete(
            () => { playerStatsRect.gameObject.SetActive(false); });
    }

    private void ShowPlayerStatsTween()
    {
        LeanTween.move(playerStatsRect, insidePositionStats, transitionTime).setEaseInCubic();
    }

    public void ShowPlayerStats()
    {
        if (rondaComenzada) return;
        playerStatsRect.gameObject.SetActive(true);
        UpdateStatsPlayer.UpdateStatsPlayer();
        ShowPlayerStatsTween();
    }

    public void HidePlayerStats()
    {
        HidePlayerStatsTween();
    }
}