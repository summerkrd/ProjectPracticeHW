namespace Assets.ProjectHomeWork.Develop.CommonServices.LoadingScreen
{
    public interface ILoadingCurtain
    {
        bool IsShown { get; }

        void Show();

        void Hide();
    }
}
