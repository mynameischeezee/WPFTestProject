using Caliburn.Micro;

namespace BAMTestProject.ViewModels
{
    public class MarketViewModel : Screen
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public MarketViewModel Create(int id, string name)
        {
            return new MarketViewModel()
            {
                Id = id,
                Name = name
            };
        }
    }
}
