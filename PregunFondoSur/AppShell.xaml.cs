using PregunFondoSur.Vistas;

namespace PregunFondoSur
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("PaginaEspera", typeof(PaginaEsperaUsuario));
            Routing.RegisterRoute("PaginaEleccionCategoria", typeof(EleccionCategoria));
            Routing.RegisterRoute("PaginaPregunta", typeof(PaginaPregunta));
            Routing.RegisterRoute("PaginaFinalizacion", typeof(PaginaFinalizacion));
            Routing.RegisterRoute("PaginaListadoSalas", typeof(PaginaListadoSalas));
        }
    }
}