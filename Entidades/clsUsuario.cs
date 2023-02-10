namespace Entidades
{
    public class clsUsuario
    {

        #region Atributos
        String userName { get; set; }
        String password {get; set;}

        String imagen { get; set;}
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