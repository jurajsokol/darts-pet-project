namespace Darts.Avalonia;

public static class ConvertExtensions
{
    public static Models.Player ToModel(this DAL.Entities.Player data)
    {
        return new Models.Player()
        {
            ID = data.ID,
            Name = data.Name,
        };
    }
}
