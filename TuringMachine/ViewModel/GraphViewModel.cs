using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace TuringMachine.ViewModel
{
    public class GraphViewModel : ObservableObject
    {
        private RelayCommand AddNodeCommand { get; set; }
        private RelayCommand RemoveNodeCommand { get; set; }

        public GraphViewModel()
        {
            AddNodeCommand = new RelayCommand(AddNode);
            RemoveNodeCommand = new RelayCommand(RemoveNode);
        }

        private void AddNode()
        {
            throw new NotImplementedException();
        }

        private void RemoveNode()
        {
            throw new NotImplementedException();
        }

        public void AddNode(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void RemoveNode(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void AddEdge(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void RemoveEdge(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void AddEdge()
        {
            throw new NotImplementedException();
        }

        public void RemoveEdge()
        {
            throw new NotImplementedException();
        }

    }
}
