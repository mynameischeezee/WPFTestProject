using System;
using Caliburn.Micro;

namespace BAMTestProject.ViewModels
{
    public class DayOfWeekViewModel : Screen
    {
        private bool _isSelected;
        private DayOfWeek _dayOfWeek;

        public DayOfWeek DayOfWeek
        {
            get => _dayOfWeek;
            set => Set(ref _dayOfWeek, value, nameof(DayOfWeek));
        }

        public bool IsSelected
        {
            get => _isSelected;
            set => Set(ref _isSelected, value, nameof(IsSelected));
        }
    }
}
