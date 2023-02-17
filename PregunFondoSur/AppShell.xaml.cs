using PregunFondoSur.Vistas;

namespace PregunFondoSur
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("PaginaEsperaUsuario", typeof(PaginaEsperaUsuario));
            Routing.RegisterRoute("PaginaEleccionCategoria", typeof(EleccionCategoria));
            Routing.RegisterRoute("PaginaPregunta", typeof(PaginaPregunta));
        }
    }
}