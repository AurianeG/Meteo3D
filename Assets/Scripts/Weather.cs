using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class SimpleWeather : MonoBehaviour
{
    public TMPro.TextMeshProUGUI temperatureText;
    //public GameObject sun;

    public GameObject rain;

    public GameObject tree;
    public GameObject snowTree;

    public GameObject fog;

    public GameObject cloud;

    public GameObject thunder;
    

    void Start()
    {
        StartCoroutine(GetWeather());
    }

    IEnumerator GetWeather()
    {
        string url =
            "https://api.open-meteo.com/v1/forecast?latitude=48.4196&longitude=-71.0637&current_weather=true";
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.Success)
        {
            WeatherData data = JsonUtility.FromJson<WeatherData>(request.downloadHandler.text);
            float temp = data.current_weather.temperature;
            int code = data.current_weather.weathercode;
            temperatureText.text = temp + "°C";


            //sun.SetActive(code <= 2);
            if((code >= 51 && code <= 67) || (code >= 80 && code <= 82))
            {
                rain.SetActive(true);
            }
            else if((code >= 71 && code <= 77) || (code >= 85 && code <= 86))
            {
                tree.SetActive(false);
                snowTree.SetActive(true);
            }
            else if(code >= 45 && code <= 48)            {
                fog.SetActive(true);
            }
            else if(code == 3 || (code >= 20 && code <= 40))
            {
            cloud.SetActive(true);
            }
            else if(code >= 95 && code <= 99)
            {
                thunder.SetActive(true);
            }
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
