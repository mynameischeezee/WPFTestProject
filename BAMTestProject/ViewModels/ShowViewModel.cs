using Caliburn.Micro;

namespace BAMTestProject.ViewModels
{
    public class ShowViewModel : Screen
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ShowViewModel Create(int id, string name)
        {
            return new ShowViewModel()
            {
                Id = id,
                Name = name
            };
        }
    }
}
