using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuringMachine.Model
{
    public class VariableEntry : ObservableObject {
	    private string _name;
	    private string _values;
	    private bool _blacklist;
	    private bool _isNew;

		public VariableEntry() {
			_name = "";
			_values = "";
			_blacklist = false;
			_isNew = true;
		}
		public VariableEntry(string name, string values, bool blacklist) {
			_name = name;
			_values = values;
			_blacklist = blacklist;
			_isNew = false;
		}

		public string Name {
		    get => _name;
		    set => SetProperty(ref _name, value);
	    }

	    public string Values {
		    get => _values;
		    set => SetProperty(ref _values, value);
	    }

	    public bool Blacklist {
		    get => _blacklist;
		    set => SetProperty(ref _blacklist, value);
	    }
    }
}
