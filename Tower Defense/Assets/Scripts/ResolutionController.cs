using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class ResolutionController : MonoBehaviour
{

    [SerializeField] private TMP_Dropdown resolutionDropDown;

    private Resolution[] _resolutions;
    private List<Resolution> _filteredResolution;

    private int _currentResIndex = 0;
    private bool _isFullscreen = true;
    void Start()
    {
        _resolutions = Screen.resolutions;
        _filteredResolution = new List<Resolution>();

        resolutionDropDown.ClearOptions();
        
        for (int i = 0; i < _resolutions.Length; i++)
        {
            _filteredResolution.Add(_resolutions[i]);
        }

        List<string> options = new List<string>();
        for (int i = 0; i < _filteredResolution.Count; i++)
        {
            string resolutionOption = _filteredResolution[i].width + "x" + _filteredResolution[i].height + " " +
                                      _filteredResolution[i].refreshRateRatio.value.ToString("##") + "Hz";
            options.Add(resolutionOption);
            if (_filteredResolution[i].width == Screen.width && _filteredResolution[i].height == Screen.height)
            {
                _currentResIndex = i;
            }
        }

        resolutionDropDown.AddOptions(options);
        resolutionDropDown.value = _filteredResolution.Count - 1;
        resolutionDropDown.RefreshShownValue();
    }

    public void SetResolution(int res)
    {
        Resolution resolution = _filteredResolution[res];
        Screen.SetResolution(resolution.width,resolution.height,_isFullscreen);
    }

    public void FullscreenMode(bool fullscreen)
    {
        _isFullscreen = fullscreen;
        SetResolution(_currentResIndex);
    }
}