# <u>Proyecto Final: Calabozos y Lenguajes</u>

## <u>Tabla de Contenidos</u>

1. [Descripción del Proyecto](#descripción-del-proyecto)
2. [Tecnologías Utilizadas](#tecnologías-utilizadas)
3. [Instructores](#instructores)
4. [Información del Alumno](#información-del-alumno)
5. [Evaluación](#evaluación)
6. [Libertades Creativas](#libertades-creativas)
7. [Sobre el codigo y los archivos](#sobre-el-codigo-y-los-archivos)
8. [Detalles del ordenador](#detalles-del-ordenador)

---

## <u>Descripción del Proyecto</u>

Este proyecto es el proyecto final de la materia "Taller de Lenguajes 1" de las carreras informaticas de la "Facultad de Ciencias Exactas y Tecnológicas de la Universidad Nacional de Tucumán". Como requisito final para la aprobacion de la materia se debe terminar y defender este proyecto ante los docentes de la catedra.

### Objetivo

Crear un videojuego de combates por turnos que comparte características con juegos de rol por turnos *TT-RPG*, pero mucho más sencillo ya que todo se mostrará a través de texto mediante la consola de Windows, y no se evaluan caracteristicas relacionadas al arte, unicamente que el usuario sea capaz de facilmente entender la informacion presentada.

### Funcionalidades

- **Creación de personajes**: Se debe permitir la creacion de personajes con atributos fisicos y personales, tales como su nombre, fuerza y velocidad.
- **Combate**: Los personajes creados deben ser capaces de combatir entre ellos, utilizando los atributos mencionados en el item superior para determinar, en conjunto con un cierto nivel de azar, los ganadores de la pelea.
- **Persistencia de datos**: Se debe almacenar toda la información en archivos `.json` para su reutilización en distintas ejecuciones del programa. La informacion a guardar incluye a los personajes, sus atributos, y su historial de combate.

---

## <u>Tecnologías Utilizadas</u>

1. **Framework**: .NET Core
    - Version: 8.0.303
    - Arquitectura: x64
	
2. **IDE**: Visual Studio Code
    - Version: 1.88.1
    - Arquitectura: x64
    - Fecha de Instalacion: 10/04/2024
    - Extensiones Notables:
        - .NET Install Toll v2.1.1
        - C# v2.39.29
        - CMake Tools v1.18.44
		
3. **Lenguaje**: C# (C Sharp)
4. **Manejo de Versiones**: Git v2.44.0.windows.1 & GitHub

## <u>Instructores</u>

- **Jefe de la cátedra**: Ingeniero Javier Graña.
- **Docentes de práctica**: Ingeniero Agustín Décima y Tecnico Sergio Guardia.
- **Ayudantes estudiantiles**: Estudiantes Mariano Girbau y Javier Nacchio.

## <u>Información del Alumno</u>

- **Nombre**: Ignacio Arturo de la Vega.
- **DNI**: 44.742.681
- **Carrera**: Licenciatura en Informática.
- **Email de contacto**: [123ignaciodelavega@gmail.com](mailto:123ignaciodelavega@gmail.com)

## <u>Evaluación</u>

**El proyecto debe cumplir con las siguientes rúbricas de evaluación, que se encuentran en el archivo adjunto [`Rubricas del Proyecto Final.pdf`](Rubricas%20del%20Proyecto%20Final.pdf):**

1. **Puntos solicitados en el proyecto**: Completar adecuadamente todos los puntos solicitados en el proyecto.
2. **Robustez**: El código debe funcionar correctamente bajo todas las condiciones esperadas y entradas permitidas.
3. **Modularización**: El código debe estar estructurado en módulos lógicos y coherentes.
4. **Buenas prácticas**: Usar nombres correctos y descriptivos según las convenciones.
5. **Interfaz de usuario**: Crear una interfaz fácil de usar con opciones claras.
6. **Uso de archivos**: Manejar correctamente la persistencia y recuperación de información.
7. **Uso de servicios web**: Integrar correctamente los servicios web necesarios.
8. **Innovación**: Proponer mejoras o extensiones al sistema original.
9. **Uso de Git/Github**: Utilizar el repositorio periódicamente con múltiples commits.
10. **Documentación**: Describir brevemente el proyecto y los recursos utilizados.

## <u>Libertades Creativas</u>

En esta seccion detallo todos los cambios, contenido añadido o eliminado, y cualquier detalle que no se presente a priori en los requisitos minimos de la consigna asignada para el projecto final. De esta manera quedan de forma explicitas las ***particularidades*** de mi proyecto.
Cabe aclarar que muchas decisiones fueron tomadas con una gran inspiracion en el juego de mesa *"Dungeons and Dragons"*, ya que soy un gran fan; sin embargo la implementacion a codigo es completamente propia.

---

### <u>Sub-Indice</u>

1. [Sobre los personajes](#Sobre-los-personajes)
2. [Sobre el combate](#Sobre-el-combate)

---

### Sobre los personajes

- <u>**CLASES**:</u> La consigna no da ningun detalle sobre las clases, y bajo su mirada tan solo es un dato arbitrario y sin uso. Por esto mismo he armado *4 clases distintivas*, y cada una tiene sus particularidades, reflejadas en sus *estadisticas* y su *subida de nivel*. La clase puede ser elegida directamente en la creacion de personaje, o asignada automaticamente dependiendo de los atributos seleccionados. Las clases que se han añadido son:
    1. **El Barbaro**: Fuerza bruta con poca o nada durabilidad. Es la epitomia del dicho "La mejor defensa es una buena ofensa".
    2. **El Caballero**: Con gran armadura y buena habilidad con las armas, es un peleador resistente y formidable, pero su armadura lo hace lento y poco flexible.
    3. **El explorador**: Rapido y elusivo, el explorador tratara de atacar sin ser visto, y constantemente se movera alrededor de su enemigo, pero cuidado, un solo golpe podria ser letal.
    4. **El picaro**: El picaro es una clase impredecible, ya que puede crecer en cualquier area de experiencia que le plazca, y de manera muy eficiente. Un picaro entrenado es capaz de derrotar a cualquier enemigo.
	
> **PD**: *Tener una propiedad llamada "clase", en ingles "class", complica la syntaxis en un lenguaje de programacion donde "class" es una palabra reservada y usada frecuentemente.*

- <u>**NIVELES**</u>: La consigna unicamente menciona a los niveles como una manera para calcular el daño y nada mas, ni siquiera se menciona una manera de modificar este valor, por lo que he cambiado esta mecanica para darle sentido.
    - **Como subir de nivel**: Ganar una pelea aumenta la experiencia en 1 punto, perder una pelea lo reduce en 1 *(estos valores pueden ser modificados independientemente)*. Al llegar a cierta cantidad de experiencia, el personaje sube de nivel, y aumenta la cantidad necesaria de experiencia para el proximo nivel.
    - **Al subir de nivel**: Cuando un personaje sube de nivel aumenta 2 *(valor modificable)* estadisticas de las siguientes: **Velocidad-Destreza-Fuerza-Armadura**, y tambien aumenta su vida. Tanto las cantidades como la probabilidad de elegir entre una u otra estadistica depende directamente de la clase elegida o asignada *(todos los valores pueden ser modificados)*.

### Sobre el combate

- <u>**Iniciativa**</u>: En vez de dejar al azar quien va primero en un turno, he implementado un sistema por el cual la velocidad y destreza tienen un impacto considerable en esto, aunque aun permite una pequeña cantidad de azar. Antes de iniciar un combate, los personajes comparan iniciativa, y aquel que tenga un mayor numero atacara primero. En caso de tener iniciativas similares, entra en juego el azar.

## Sobre el codigo y los archivos

Aqui se detallan las caracteristicas del codigo producido, las normas seguidas, y la logica que se uso para ciertas decisiones de diseño. No es una lista extensiva, pero cubre los aspectos mas notables del codigo.

---

### <u>Sub-Indice</u>

1. [Datos Generales](#datos-generales)
2. [Archivo: tl1-proyectofinal2024-ArturoDLV.csproj](#archivo-tl1-proyectofinal2024-arturodlvcsproj)
3. [Archivo: Program.cs](#archivo-programcs)
4. [Archivo: global.cs](#archivo-globalcs)
5. [Archivo: character.cs](#archivo-charactercs)
6. [Archivos para la muestra de los textos](#archivos-para-el-texto)

---

### Datos Generales

- **Nombres**: En primer lugar, como se puede notar en cualquier archivo de codigo, todos los nombres de variables, clases, metodos, etcetera; estan en ingles. Esto es debido a que me siento mas comodo programando asi, ya que toda mi vida lo he hecho en ingles, y ademas de esto, creo que es ventajoso por lo descriptivo en pocas letras que es el ingles, y porque toda empresa de software se maneja de esta manera.

- **Logica de diseño**: Las decisiones sobre nombres, logica y modulacion, fueron guiadas por el libro "Clean Code", ya que luego de haberlo leido, siento que propone reglas y consejos muy utiles para producir un buen codigo. Si bien es probable que no he seguido a *raja tabla* cada item que se menciona en dicho libro, hice un esfuerzo consciente en intentarlo.

- **Modularizacion**: He decidido modularizar lo mas posible el proyecto, de forma tal que sea facil de leer y mantenible a futuro. Esto incluye multiples archivos '.cs' cada uno cumpliendo sus funciones particulares y no irrumpiendo en el funcionamiento de otros modulos. Ademas utilice la funcionalidad de las "Regiones" para delimitar puntos importantes en la logica.

- **Texto**: El programa permite la facil edicion de los textos, aun cuando la aplicacion ya fue compilada y publicada, mediante los archivos `.json` de lenguaje. Todo texto *(con excepcion de los mensajes de error o debug)* puede ser modficiado de esta manera.

### Archivo: tl1-proyectofinal2024-ArturoDLV.csproj

[Archivo](tl1-proyectofinal2024-ArturoDLV.csproj)

Este es el archivo general del proyecto. Añadi una pequeña seccion para que ciertos archivos siempre esten disponibles, aun cuando se hace una compilacion y wrap de la aplicacion.

### Archivo: Program.cs

[Archivo](Program.cs)

Este es el archivo principal del programa.

### Archivo: global.cs

[Archivo](global.cs)

En este archivo se encuentra una clase estatica que provee a todo el codigo de variables, constantes y metodos que se usan en cualquier sector del programa. Esto incluye aquellas variables de balance de la jugabilidad, para que, de ser necesario, se puedan modificar con facilidad y tengan efecto global.

### Archivo: character.cs

[Archivo](character.cs)

En este archivo se encuentra la clase usada para todos los personajes, y sus metodos e interacciones.
Puntos notables:
- <u>**Daño**:</u> Hacer y recibir daño son metodos separados, esta decision de diseño la tome ya que considero que son acciones que deben ser manejadas por su respectivo objeto; es decir, no creo que sea adecuado que una instancia cambie directamente los valores de otra. Ademas, esto permite implementaciones como ataques en area, o multiples enemigos atacando a un solo objetivo.

### Archivos para la muestra de los textos

[Carpeta de lenguajes](Localization)
[texts.cs](texts.cs)
[interface.cs](interface.cs)

Todo el texto esta almacenado en archivos `.json`, se muestran dos lenguajes *(a priori Ingles y Español aunque el texto puede ser cualquiera)* en la carpeta de lenguajes. Cualquier texto modificado allí afectara al texto mostrado en pantalla.

Cuando el programa inicia, lo primero que hace es buscar estos archivos y guardarlos en un objeto estatico y global que se encuentra en `texts.cs`. Si falla en abrirlos, encontrarlos, o cargarlos, el programa mostrara un error y se cerrara. Una vez cargado el archivo de lenguajes en su objeto, solamente se volvera a repetir esta accion si en las opciones el jugador cambia el lenguaje.

Por ultimo, el archivo `interface.cs` es el encargado de *renderizar* todo el texto que se requiere, permitiendo facilidad de cambiar la estetica y orden del texto, sin necesidad de modificar la logica del programa principal.


## <u>Detalles del ordenador</u>

**En caso de ser necesario para solucionar algun problema particular o por otra circunstancia, aqui se detalla informacion varia del ordenador en el cual fue desarrollado el proyecto:**

- **Procesador**: Intel(R) Core(TM) i5-9400F CPU @ 2.90GHz   2.90 GHz
- **Sistema Operativo**: Windows 10 Pro Licencia Activa | Sistema x64 bits