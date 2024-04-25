namespace Darts.WinUI
{
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

        public static DAL.Entities.Player ToDalPLayer(this Models.Player data)
        {
            return new DAL.Entities.Player()
            {
                ID = data.ID,
                Name = data.Name,
            };
        }
    }
}
