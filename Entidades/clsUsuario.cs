namespace Entidades
{
    public class clsUsuario
    {

        #region Atributos
        public String userName { get; set; }
        public String imagen { get; set;}
        public String nombreSala {get; set;}
        public bool tuTurno { get; set;}
        #endregion

        #region Constructor con parámetros
        public clsUsuario(string userName, string imagen, string nombreSala)
        {
            this.userName = userName;
            this.imagen = imagen;   
            this.nombreSala= nombreSala;
        }
        #endregion

        #region Constructor por defecto
        public clsUsuario()
        {

        }
        #endregion
    }

}