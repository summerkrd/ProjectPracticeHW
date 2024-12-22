using Assets.ProjectHomeWork.Develop.CommonServices.LoadingScreen;
using UnityEngine;

public class StandartLoadingCurtain : MonoBehaviour, ILoadingCurtain
{
    public bool IsShown => gameObject.activeSelf;

    private void Awake()
    {
        Hide();

        DontDestroyOnLoad(this);
    }

    public void Hide() => gameObject.SetActive(false);

    public void Show() => gameObject.SetActive(true);
}
