using System;
using LovelyBytes.AssetVariables;
using UnityEngine;
using UnityEngine.UI;

internal class SampleMvcController : MvcController<SampleMvcModel, SampleMvcView>
{ }

[Serializable]
internal class SampleMvcView : IMvcView<SampleMvcModel>
{
    public Button BtnPvp, BtnPve;
    public Slider SldPlayerHealth;
    public void Bind(SampleMvcModel model)
    {
        GameMode initialGameMode = model.GameMode.Value;
        float initialHealth = model.PlayerHealth.Value;
        
        OnGameModeChanged(initialGameMode, initialGameMode);
        OnPlayerHealthChanged(initialHealth, initialHealth);
        
        model.GameMode.OnValueChanged += OnGameModeChanged;
        model.PlayerHealth.OnValueChanged += OnPlayerHealthChanged;
    }

    public void Release(SampleMvcModel model)
    {
        model.GameMode.OnValueChanged -= OnGameModeChanged;
        model.PlayerHealth.OnValueChanged -= OnPlayerHealthChanged;
    }

    private void OnGameModeChanged(GameMode oldValue, GameMode newValue)
    {
        BtnPvp.interactable = newValue != GameMode.Pvp;
        BtnPve.interactable = newValue != GameMode.Pve;
    }

    private void OnPlayerHealthChanged(float oldValue, float newValue)
    {
        ColorBlock colors = SldPlayerHealth.colors;
        colors.disabledColor = (oldValue > newValue) ? Color.red : Color.green;
        SldPlayerHealth.colors = colors;
        
        SldPlayerHealth.SetValueWithoutNotify(newValue);
    }
}

[Serializable]
internal class SampleMvcModel : IMvcModel<SampleMvcView>
{
    public GameModeVariable GameMode;
    public FloatRange PlayerHealth;
    public void Bind(SampleMvcView view)
    {
        view.BtnPvp.onClick.AddListener(OnBtnPvpPressed);
        view.BtnPve.onClick.AddListener(OnBtnPvePressed);
    }

    public void Release(SampleMvcView view)
    {
        view.BtnPvp.onClick.RemoveListener(OnBtnPvpPressed);
        view.BtnPve.onClick.RemoveListener(OnBtnPvePressed);
    }

    private void OnBtnPvpPressed()
    {
        GameMode.Value = global::GameMode.Pvp;
    }

    private void OnBtnPvePressed()
    {
        GameMode.Value = global::GameMode.Pve;
    }
}
