# Parcial #3 - Polimorfismo, Serialización JSON y Sistema de Autenticación Segura

**Fecha:** 28/09/2026

## Asignatura

**Herramientas de Programación Aplicadas III (.NET)**

---

# Contenido del Repositorio

Este repositorio contiene los tres proyectos desarrollados para el **Parcial #3**, enfocados en la aplicación de conceptos avanzados de Programación Orientada a Objetos, intercambio de datos mediante JSON y seguridad informática utilizando autenticación basada en hashes BCrypt y bases de datos MySQL.

Los proyectos desarrollados fueron:

- Sistema de geometría utilizando polimorfismo.
- Serialización de objetos a formato JSON.
- Sistema de autenticación segura con BCrypt y MySQL.

---

# Tecnologías Utilizadas

- C#
- .NET
- Windows Forms
- Aplicaciones de Consola
- MySQL
- MySQL Connector/NET
- BCrypt.Net
- Newtonsoft.Json
- Visual Studio
- Git
- GitHub

---

# Proyecto #1 - Sistema de Geometría utilizando Polimorfismo

## Descripción

Este proyecto implementa los principios fundamentales de la Programación Orientada a Objetos mediante el uso de:

- Clases abstractas.
- Herencia.
- Polimorfismo.
- Encapsulación.
- Arreglos de objetos.

El sistema permite calcular automáticamente el área y el perímetro de diferentes figuras geométricas utilizando una única referencia del tipo padre.

---

## Clase Abstracta Figura

La clase `Figura` funciona como una plantilla general para todas las figuras geométricas.

```csharp
public abstract class Figura
{
    public abstract double CalcularArea();
    public abstract double CalcularPerimetro();
}
```

Los métodos abstractos obligan a las clases derivadas a implementar sus propias reglas de cálculo.

---

## Clases Derivadas

### Rectángulo

Propiedades:

```text
Base
Altura
```

Fórmulas:

```text
Área = Base × Altura

Perímetro = 2(Base + Altura)
```

---

### Cuadrado

Propiedades:

```text
Lado
```

Fórmulas:

```text
Área = Lado²

Perímetro = 4 × Lado
```

---

### Triángulo

Propiedades:

```text
Base
Altura
LadoA
LadoB
LadoC
```

Fórmulas:

```text
Área = (Base × Altura) / 2

Perímetro = LadoA + LadoB + LadoC
```

---

## Implementación del Polimorfismo

Se utiliza un arreglo de tipo padre:

```csharp
Figura[] figuras = new Figura[3];
```

Posteriormente se almacenan distintas clases hijas:

```csharp
figuras[0] = new Rectangulo(...);
figuras[1] = new Cuadrado(...);
figuras[2] = new Triangulo(...);
```

Finalmente, mediante un único recorrido:

```csharp
foreach (Figura fig in figuras)
```

el programa ejecuta dinámicamente el método correspondiente según el tipo real del objeto.

---

## Conceptos Aplicados

- Abstracción.
- Herencia.
- Polimorfismo.
- Sobrescritura de métodos (`override`).
- Encapsulación.
- Arreglos de objetos.

---

## Resultado Esperado

```text
REPORTE DE GEOMETRÍA POLIMÓRFICA

Clase: Rectangulo | Área: 15.00 | Perímetro: 16.00

Clase: Cuadrado   | Área: 16.00 | Perímetro: 16.00

Clase: Triangulo  | Área: 6.00  | Perímetro: 12.00
```

---

## Captura de Pantalla

<img width="716" height="196" alt="image" src="https://github.com/user-attachments/assets/2c84c8a7-4262-4e8f-aa26-2fb2cb276004" />


---

# Proyecto #2 - Serialización de Objetos JSON

## Descripción

Este proyecto demuestra cómo convertir objetos de C# a formato JSON utilizando la librería Newtonsoft.Json.

La serialización permite intercambiar información entre sistemas, APIs, aplicaciones web y bases de datos de forma estructurada.

---

## Objeto Utilizado

```csharp
var persona = new
{
    Nombre = "Johandry González",
    Edad = 25,
    Carrera = "Sistemas y computación"
};
```

---

## Serialización

La conversión se realiza mediante:

```csharp
JsonConvert.SerializeObject(persona);
```

---

## Resultado Generado

```json
{
  "Nombre":"Johandry González",
  "Edad":25,
  "Carrera":"Sistemas y computación"
}
```

---

## Beneficios del Uso de JSON

- Interoperabilidad entre sistemas.
- Formato ligero.
- Fácil lectura.
- Amplia compatibilidad.
- Estándar utilizado en APIs REST.

---

## Conceptos Aplicados

- Objetos anónimos.
- Serialización.
- Formato JSON.
- Intercambio de datos.
- Biblioteca Newtonsoft.Json.

---

## Captura de Pantalla

<img width="956" height="135" alt="image" src="https://github.com/user-attachments/assets/8bf54acb-6c2f-41fe-b585-abd2090ce5a7" />


---

# Proyecto #3 - Sistema de Autenticación Segura con BCrypt y MySQL

## Descripción

Este proyecto implementa un sistema completo de registro y autenticación de usuarios utilizando:

- Windows Forms.
- MySQL.
- BCrypt.
- Auditoría de accesos.

El propósito principal es aplicar buenas prácticas de seguridad en aplicaciones empresariales.

---

# Funcionalidades Implementadas

## Registro de Usuarios

El sistema permite registrar nuevos usuarios.

Proceso:

1. Ingreso del nombre de usuario.
2. Ingreso de contraseña.
3. Generación automática del hash.
4. Almacenamiento seguro en MySQL.

---

## Hash Seguro de Contraseñas

La contraseña nunca se almacena en texto plano.

Se utiliza:

```csharp
BCrypt.HashPassword()
```

para generar el hash correspondiente.

Ejemplo:

```text
Contraseña original:
MiPassword123

Hash almacenado:
$2a$11$EjA7u...
```

---

## Inicio de Sesión Seguro

Proceso:

1. El usuario introduce sus credenciales.
2. Se consulta la base de datos.
3. Se recupera el hash almacenado.
4. BCrypt verifica la coincidencia.

```csharp
BCrypt.Verify(
    password,
    hashAlmacenado
);
```

---

## Consultas Parametrizadas

La consulta de autenticación utiliza parámetros:

```sql
SELECT PasswordHash
FROM Usuarios
WHERE Username = @user
```

Esto reduce significativamente el riesgo de SQL Injection.

---

## Validación de Campos

Antes de ejecutar cualquier operación:

```text
Usuario obligatorio.
Contraseña obligatoria.
```

---

# Auditoría de Accesos

Cada intento de autenticación es almacenado en la base de datos.

Información registrada:

```text
Usuario
Estado
Fecha
Hora
```

---

## Estados Registrados

### Exitoso

```text
Acceso confirmado correctamente.
```

### Fallido

```text
Credenciales incorrectas.
```

---

## Tabla de Auditoría

```text
ControlAccesos
```

Campos utilizados:

```text
Username
Estado
FechaRegistro
```

---

# Arquitectura de Seguridad Aplicada

## Protección de Contraseñas

Implementada mediante:

```text
BCrypt
```

---

## Prevención de SQL Injection

Implementada mediante:

```text
Consultas parametrizadas
```

---

## Auditoría

Implementada mediante:

```text
Registro de eventos
```

---

## Validación de Entradas

Implementada mediante:

```text
Validación de formularios
```

---

# Base de Datos Utilizada

## Base de Datos

```text
SistemaLoginDB
```

---

## Tabla Usuarios

Campos principales:

```text
Username
PasswordHash
```

---

## Tabla ControlAccesos

Campos principales:

```text
Username
Estado
FechaRegistro
```

---

# Conceptos Aplicados

## Programación Orientada a Objetos

- Clases.
- Objetos.
- Encapsulación.
- Herencia.
- Polimorfismo.
- Abstracción.

---

## Seguridad Informática

- Hashing de contraseñas.
- BCrypt.
- Validación de entradas.
- SQL parametrizado.
- Auditoría de accesos.

---

## Persistencia de Datos

- MySQL.
- Consultas SQL.
- Conectividad mediante MySQL Connector.

---

## Captura de Pantalla

### Registro de Usuario

<img width="753" height="496" alt="image" src="https://github.com/user-attachments/assets/731d15d9-4865-4f6e-8be8-cb5506b9f4d7" />
<img width="526" height="222" alt="image" src="https://github.com/user-attachments/assets/8d4a9d83-4acb-4874-9650-da29fd628534" />


### Inicio de Sesión

<img width="976" height="482" alt="image" src="https://github.com/user-attachments/assets/7c1b39ca-522d-4d15-99d4-6cea69591a14" />


### Registro de Auditoría

<img width="450" height="258" alt="image" src="https://github.com/user-attachments/assets/135927fb-d063-4f4f-b640-9848c9cb9223" />

<img width="1280" height="149" alt="image" src="https://github.com/user-attachments/assets/22d22663-6e28-4207-b1b2-5c8ccaf1119f" />


---

# Comparación de los Proyectos

| Proyecto | Tema Principal |
|-----------|---------------|
| Proyecto 1 | Polimorfismo y Herencia |
| Proyecto 2 | Serialización JSON |
| Proyecto 3 | Seguridad y Autenticación |

---

# Estructura del Repositorio

```plaintext
Parcial3/
│
├── Proyecto1_Polimorfismo/
│   └── Program.cs
│
├── Proyecto2_JSON/
│   └── Program.cs
│
├── Proyecto3_LoginBCryptMySQL/
│   ├── Form1.cs
│   ├── Form1.Designer.cs
│   └── Program.cs
│
├── images/
│   ├── proyecto1-polimorfismo.png
│   ├── proyecto2-json.png
│   ├── proyecto3-registro.png
│   ├── proyecto3-login.png
│   └── proyecto3-auditoria.png
│
└── README.md
```

---

# Instrucciones de Ejecución

## 1. Clonar el repositorio

```bash
git clone [URL_DEL_REPOSITORIO]
```

## 2. Abrir la solución

Abrir el proyecto deseado desde Visual Studio.

## 3. Restaurar paquetes NuGet

```text
Newtonsoft.Json
MySql.Data
BCrypt.Net
```

## 4. Configurar MySQL

Crear las bases de datos y tablas necesarias.

## 5. Compilar

```text
Build > Build Solution
```

## 6. Ejecutar

```text
F5
```

---

# Aprendizajes Obtenidos

Durante el desarrollo del Parcial #3 se reforzaron los siguientes conocimientos:

- Clases abstractas.
- Polimorfismo.
- Herencia.
- Sobrescritura de métodos.
- Serialización JSON.
- Intercambio de datos.
- Windows Forms.
- MySQL.
- BCrypt.
- Seguridad informática.
- Auditoría de accesos.
- Consultas parametrizadas.
- Desarrollo de aplicaciones empresariales.

---

# Autor y Contexto

- Nombre: Johandry González
- Asignatura: Herramientas de Programación Aplicadas III (.NET)
- Evaluación: Parcial #3
- Fecha: 09/11/2026

---

# Referencias
- Material proporcionado por el docente
