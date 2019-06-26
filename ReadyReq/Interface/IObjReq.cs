/*
 * Autor: Arturo Balleros Albillo
 */
namespace ReadyReq.Interface
{
    public interface IObjReq : IObjEstandar
    {
        void Cargar(int id, int tipoReq);
        void CargarTablaReqRel(int tipoReq);
    }
}
