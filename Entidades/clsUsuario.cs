namespace Entidades
{
    public class clsUsuario
    {

        #region Atributos
        public String userName { get; set; }
        public String password {get; set;}
        public String imagen { get; set;}
        #endregion

        #region Constructor con parámetros
        public clsUsuario(string userName, string password, string imagen)
        {
            this.userName = userName;
            this.password = password;
            this.imagen = imagen;   
        }
        #endregion

        #region Constructor por defecto
        public clsUsuario()
        {

        }
        #endregion
    }

}