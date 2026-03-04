using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class SimpleWeather : MonoBehaviour
{
    public TMPro.TextMeshProUGUI temperatureText;
    public GameObject sun;
    public GameObject sunAudio;

    public GameObject rain;
    public GameObject rainAudio;

    public GameObject snow;
    public GameObject snowAudio;

    public GameObject fog;
    public GameObject fogAudio;

    public GameObject cloud;
    public GameObject cloudAudio;

    public GameObject thunder;
    public GameObject thunderAudio;
    

    void Start()
    {
        StartCoroutine(GetWeather());
    }

    IEnumerator GetWeather()
    {
        string url =
            "https://api.open-meteo.com/v1/forecast?latitude=45.50&longitude=-73.56&current_weather=true";
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.Success)
        {
            WeatherData data = JsonUtility.FromJson<WeatherData>(request.downloadHandler.text);
            float temp = data.current_weather.temperature;
            int code = data.current_weather.weathercode;
            temperatureText.text = temp + "°C";
            // Activation simple selon le code
            sun.SetActive(code <= 2);
            rain.SetActive((code >= 51 && code <= 67) || (code >= 80 && code <= 82));
            snow.SetActive((code >= 71 && code <= 77) || (code >= 85 && code <= 86));
            fog.SetActive(code >= 45 && code <= 48);
            cloud.SetActive(code == 3 || (code >= 20 && code <= 40));
            thunder.SetActive(code >= 95 && code <= 99);
        }
        else
        {
            Debug.LogError(request.error);
        }
    }
}

[System.Serializable]
public class WeatherData
{
    public CurrentWeather current_weather;
}

[System.Serializable]
public class CurrentWeather
{
    public float temperature;
    public int weathercode;
}
