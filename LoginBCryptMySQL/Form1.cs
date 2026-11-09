using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using BCryptNet = BCrypt.Net.BCrypt;

namespace LoginBCryptMySQL
{
    public partial class Form1 : Form
    {
        private string connectionString = "Server=localhost; Database=SistemaLoginDB; Uid=root; Pwd=Aqrt.234#@;";
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtLoginUsuario.Text.Trim();
            string password = txtLoginPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Por favor, ingrese su usuario y contraseña.", "Campos vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "SELECT PasswordHash FROM Usuarios WHERE Username = @user";
            string hashAlmacenado = null;

            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {
                using (MySqlCommand comando = new MySqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@user", username);
                    try
                    {
                        conexion.Open();
                        object resultado = comando.ExecuteScalar();
                        if (resultado != null)
                        {
                            hashAlmacenado = resultado.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error de conexión a la base de datos: {ex.Message}", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            bool accesoValido = false;

            // Si el usuario existe en la BD, se procede a verificar con la contraseña escrita
            if (hashAlmacenado != null)
            {
                // BCrypt descifra de manera segura y compara el texto plano con el hash guardado
                accesoValido = BCryptNet.Verify(password, hashAlmacenado);
            }

            if (accesoValido)
            {
                
                MessageBox.Show($"¡Bienvenido al sistema, {username}!", "Acceso Confirmado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                RegistrarAuditoria(username, "Exitoso");

                // Limpiar los controles de login tras el éxito
                txtLoginUsuario.Clear();
                txtLoginPassword.Clear();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.", "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Error);

                RegistrarAuditoria(username, "Fallido");
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            string username = txtRegUsuario.Text.Trim();
            string password = txtRegPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Por favor, rellene todos los campos de registro.", "Campos vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Generación del hash seguro con el algoritmo BCrypt
            string hashComprimido = BCryptNet.HashPassword(password);

            string query = "INSERT INTO Usuarios (Username, PasswordHash) VALUES (@user, @hash)";

            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {
                using (MySqlCommand comando = new MySqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@user", username);
                    comando.Parameters.AddWithValue("@hash", hashComprimido);

                    try
                    {
                        conexion.Open();
                        comando.ExecuteNonQuery();

                        MessageBox.Show($"¡Usuario registrado con éxito!\n\nHash almacenado en la BD:\n{hashComprimido}", "Registro Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Limpiar campos de registro
                        txtRegUsuario.Clear();
                        txtRegPassword.Clear();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al registrar (el usuario podría ya existir): {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void RegistrarAuditoria(string username, string estado)
        {
            // NOW() registra la fecha y la hora exacta del evento en MySQL automáticamente
            string query = "INSERT INTO ControlAccesos (Username, Estado, FechaRegistro) VALUES (@user, @estado, NOW())";

            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {
                using (MySqlCommand comando = new MySqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@user", string.IsNullOrEmpty(username) ? "Desconocido" : username);
                    comando.Parameters.AddWithValue("@estado", estado);

                    try
                    {
                        conexion.Open();
                        comando.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        // Se imprime discretamente en la consola de depuración interna de Visual Studio
                        System.Diagnostics.Debug.WriteLine($"Error al guardar auditoría: {ex.Message}");
                    }
                }
            }
        }
    }
}
