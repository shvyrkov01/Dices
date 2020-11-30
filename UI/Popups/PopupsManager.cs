using UnityEngine;

public class PopupsManager : MonoBehaviour
{
    public T CreatePopup<T>(string additionFolder = "") where T : Popup
    {
        var targetPopup = (T)GetPopup<T>(additionFolder);
        return InstantiatePopup(targetPopup);
    }


    private T InstantiatePopup<T>(T popupReferense) where T : Popup
    {
        Transform canvas = null;

        var mainCanvas = FindObjectOfType<MainCanvas>();

        if(!ReferenceEquals(mainCanvas, null))
            canvas = mainCanvas.transform;
        else
            canvas = FindObjectOfType<Canvas>().transform;

        return Instantiate(popupReferense, canvas);
    }


    private Popup GetPopup<T>(string additionFolder = "") where T : Popup
    {
        if (additionFolder != "")
            additionFolder += "/";

        return Resources.Load<T>($"Popups/{additionFolder}{typeof(T).ToString()}");
    }
}
