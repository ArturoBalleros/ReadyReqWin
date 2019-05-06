using System.Collections.ObjectModel;

namespace ReadyReq.Model
{
    public class ClsDatDG
    {
        public string Descrip { get; set; }
    }
    public class ClsDatDGCollection : ObservableCollection<ClsDatDG>
    {
        public ClsDatDGCollection() { }
    }
}