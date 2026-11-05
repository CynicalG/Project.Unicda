using System.Collections.Generic;
using VideoFormulario.Modelo;

namespace VideoFormulario.Datos
{
    public class Usuario
    {
        private List<UsuarioModel> listaUsuarios = new List<UsuarioModel>();

        // Guardar un usuario
        public void Guardar(UsuarioModel modelo)
        {
            listaUsuarios.Add(modelo);
        }

        // Consultar todos los usuarios
        public List<UsuarioModel> Consultar()
        {
            return listaUsuarios;
        }

        // Limpiar la lista de usuarios
        public void Limpiar()
        {
            listaUsuarios.Clear();
        }
    }
}