namespace GrayScale
{
    public interface ISwitcheable
    {
        void Switch(bool isOn);

        void Switch();

        bool IsOn { get; }
    }
}