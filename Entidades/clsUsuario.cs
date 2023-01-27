namespace Entidades
{
    public class clsUsuario
    {

        #region Atributos
        String userName { get; set; }
        String password {get; set;}
        #endregion

        #region Constructor con parámetros
        public clsUsuario(string userName, string password)
        {
            this.userName = userName;
            this.password = password;
        }
        #endregion

        #region Constructor por defecto
        public clsUsuario()
        {

        }
        #endregion
    }

}