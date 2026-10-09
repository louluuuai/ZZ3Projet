using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SettingsController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject settingsPanel;

    // 分辨率下拉框
    [SerializeField] private TMP_Dropdown resolutionDropdown;

    // 保存每个选项对应的宽度、高度
    private readonly List<Vector2Int> resolutions =
        new List<Vector2Int>();

    private void Start()
    {
        InitializeResolutions();
        CloseSettings();
    }

    private void InitializeResolutions()
    {
        resolutions.Clear();
        List<string> options = new List<string>();

        // 获取显示器支持的分辨率
        foreach (Resolution resolution in Screen.resolutions)
        {
            Vector2Int size = new Vector2Int(
                resolution.width,
                resolution.height
            );

            // 不重复添加相同的宽度、高度
            if (!resolutions.Contains(size))
            {
                resolutions.Add(size);
                options.Add(size.x + " x " + size.y);
            }
        }

        // 确保列表包含当前游戏窗口的尺寸
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
    }

    public void ApplyResolution()
    {
        int index = resolutionDropdown.value;
        Vector2Int size = resolutions[index];

        Screen.SetResolution(
            size.x,
            size.y,
            Screen.fullScreenMode
        );
    }
}