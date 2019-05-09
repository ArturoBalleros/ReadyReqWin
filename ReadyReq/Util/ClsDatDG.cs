using System.Collections.ObjectModel;

namespace ReadyReq.Model
{
    public sealed class ClsDatDG
    {
        public string Descrip { get; set; }
    }
    public sealed class ClsDatDGCollection : ObservableCollection<ClsDatDG>
    {
        public ClsDatDGCollection() { }
    }
}