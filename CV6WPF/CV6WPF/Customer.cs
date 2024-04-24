using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CV6WPF
{
    public class Customer : INotifyPropertyChanged
    {
        private string firstName;
        private string lastName;

        public int Id { get; set; }
        public string FirstName { get 
            {
                return firstName;
            }
            set 
            {
                SetValue(ref firstName, value);
            }
        }
        private void SetValue<T>(ref T prop, T val,[CallerMemberName]string name = null)
        {
            prop = val;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public string LastName { get => lastName; set { SetValue(ref lastName, value); } }
        public int Age { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
