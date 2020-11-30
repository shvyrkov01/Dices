[System.Serializable]
public class GameModeSettings 
{
    /// <summary>
    /// Игрвой режим
    /// </summary>
    public GameModeType GameModeType;

    /// <summary>
    /// Нужное количество энергии для входа в режим
    /// </summary>
    public int EnergyRequired;


    public int MinRate;


    /// <summary>
    /// Коэффициент выигрыша
    /// </summary>
    public float WinCoefficient;
}
