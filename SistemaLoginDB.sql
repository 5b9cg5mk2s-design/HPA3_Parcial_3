

-- 1. CREACIÓN DE LA BASE DE DATOS
CREATE DATABASE IF NOT EXISTS SistemaLoginDB;
USE SistemaLoginDB;

-- 2. TABLA DE USUARIOS (Criterio: Almacenar el hash de BCrypt)
CREATE TABLE IF NOT EXISTS Usuarios (
    UsuarioID INT AUTO_INCREMENT PRIMARY KEY,
    Username VARCHAR(50) UNIQUE NOT NULL,
    PasswordHash VARCHAR(100) NOT NULL
) ENGINE=InnoDB;

-- 3. TABLA DE AUDITORÍA/LOGS (Criterio: Almacenar intentos, usuario y fecha)
CREATE TABLE IF NOT EXISTS ControlAccesos (
    AccesoID INT AUTO_INCREMENT PRIMARY KEY,
    Username VARCHAR(50) NOT NULL,
    Estado VARCHAR(20) NOT NULL, -- Valores esperados: 'Exitoso' o 'Fallido'
    FechaRegistro DATETIME DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB;


