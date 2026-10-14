using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private Toggle fullscreenToggle;

    // Dropdown reference for resolution selection
    [SerializeField] private TMP_Dropdown resolutionDropdown;

    // save the supported resolutions in a list
    private readonly List<Vector2Int> resolutions =
        new List<Vector2Int>();

    private void Start()
    {
        InitializeResolutions();
        fullscreenToggle.SetIsOnWithoutNotify(Screen.fullScreen);
        CloseSettings();
    }

    private void InitializeResolutions()
    {
        resolutions.Clear();
        List<string> options = new List<string>();

        // get all supported resolutions and add them to the list
        foreach (Resolution resolution in Screen.resolutions)
        {
            Vector2Int size = new Vector2Int(
                resolution.width,
                resolution.height
            );

            // don't add duplicate resolutions
            if (!resolutions.Contains(size))
            {
                resolutions.Add(size);
                options.Add(size.x + " x " + size.y);
            }
        }

        // ensure the current resolution is included in the list
        Vector2Int currentSize =
            new Vector2Int(Screen.width, Screen.height);

        if (!resolutions.Contains(currentSize))
        {
            resolutions.Add(currentSize);
            options.Add(currentSize.x + " x " + currentSize.y);
        }

        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(options);

        int currentIndex = resolutions.IndexOf(currentSize);

        resolutionDropdown.SetValueWithoutNotify(currentIndex);
        resolutionDropdown.RefreshShownValue();
    }

    public void OpenSettings()
    {
        mainMenuPanel.SetActive(false);

        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
        fullscreenToggle.SetIsOnWithoutNotify(Screen.fullScreen);
    }

    public void ApplyResolution()
    {
        int index = resolutionDropdown.value;
        Vector2Int size = resolutions[index];

        // toggle fullscreen mode based on the fullscreen toggle state
        FullScreenMode mode = fullscreenToggle.isOn
            ? FullScreenMode.FullScreenWindow
            : FullScreenMode.Windowed;

        Screen.SetResolution(size.x, size.y, mode);
    }
}