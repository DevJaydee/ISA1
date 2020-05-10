using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
	[SerializeField] private Slider cameraSensitivitySlider = default;      // Reference to the Camera Sensitivity Slider.
	[SerializeField] private TextMeshProUGUI cameraSensitivitySliderValueTextMesh = default;    // Reference to the Camera Sensitivity Slider Value Text Mesh.
	[Space]
	[SerializeField] private Slider audioVolumeSlider = default;      // Reference to the Audio Volume Slider.
	[SerializeField] private TextMeshProUGUI audioVolumeSliderValueTextMesh = default;    // Reference to the Audio Volume Slider Value Text Mesh.
	[SerializeField] private AudioMixer mixer = default;        // Reference to the Audio Mixer.

	private void Start()
	{
		cameraSensitivitySlider.value = PlayerPrefs.GetFloat("CameraSensitivity");
		audioVolumeSlider.value = PlayerPrefs.GetFloat("AudioVolume");
	}

	public void UpdateCamSensitivity()
	{
		PlayerPrefs.SetFloat("CameraSensitivity", cameraSensitivitySlider.value);
		cameraSensitivitySliderValueTextMesh.text = "Sensitivity: " + cameraSensitivitySlider.value.ToString("F2");
	}

	public void UpdateAudioVolume()
	{
		PlayerPrefs.SetFloat("AudioVolume", audioVolumeSlider.value);
		audioVolumeSliderValueTextMesh.text = "Volume: " + audioVolumeSlider.value.ToString("F0");
		mixer.SetFloat("Volume", (audioVolumeSlider.value - 50) / 4);
	}
}
