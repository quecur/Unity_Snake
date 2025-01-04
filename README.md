# Integrantes de la realización del trabajo opcional:
Carlos Tomás Quevedo Olivares
Ramón Valls Martin

Formato de la práctica: UNITY

# Descripción de la Tarea de la práctica final opcional:

Snake es un juego desarrollado en Unity. En esta versión, comenzamos con una simple esfera como cabeza de la serpiente, y el objetivo es recolectar la mayor cantidad de objetos posibles para hacer crecer su cuerpo, tanto animales como comida. Pero con cuidado, no todo lo que encontremos en el camino nos beneficiará, habrán productos tóxicos que lo que harán es restarnos tamaño al cuerpo de la serpiente o eliminarnos directamente, perderemos si nos quedamos sin cuerpo. A continuación, explicaremos paso a paso su desarrollo en Unity:

# 1. Diseño Visual:

El diseño fue desarrollado utilizando recursos 3D descargados de internet en formato FBX con sus respectivas texturas. Se buscó crear un entorno inmersivo y visualmente atractivo que complementara la jugabilidad.
El espacio principal está diseñado como una plaza, con una fuente en el centro y bancos colocados alrededor. Este espacio es como el área principal donde la serpiente puede moverse libremente y recolectar y esquivar objetos. A la izquierda de la plaza, un área de césped separada por arbustos donde también se incluye una pequeña villa decorativa y un árbol. Y por último, todo el escenario está rodeado por vallas que actúa como límite para el movimiento de la serpiente.
Para los elementos de comida de la serpiente usamos animales como ovejas y conejos, y frutas como la manzana entre otros. Y para los elementos tóxicos usamos barriles tóxicos de diferentes colores para dinamizar.


# 2. Desarrollo funcionalidad de inicio del juego:

El juego comienza con un menú inicial que permite al jugador decidir si desea iniciar la partida o salir del juego. Esta funcionalidad fue implementada utilizando un script en C# llamado MenuInicial. 
Al iniciar la aplicación, el usuario es recibido por una pantalla que contiene dos opciones:
Jugar: Inicia el juego. Cuando se selecciona esta opción, el método Jugar() se ejecuta. Este método utiliza SceneManager.LoadScene() para cargar la siguiente escena en la lista de construcción del proyecto.
Salir: Cierra la aplicación. Si se selecciona esta opción, se llama al método Salir(), ejecutando Application.Quit() para cerrar la aplicación.

![0 2025 01 04 A Las 18.54.00](../0%202025-01-04%20a%20las%2018.54.00.png)


# 3. Desarrollo del movimiento de cámara y seguimiento de la serpiente:

Con la implementación de esta funcionalidad hacemos que la cámara siga constantemente a la serpiente durante la partida. Esto lo hicimos con el cálculo de una posición, la cual combina la posición actual de la serpiente con un desplazamiento (offset) para determinar la distancia y el ángulo desde los que se observa. Y con este desplazamiento permitimos que la cámara se mantenga  por encima y detrás de la serpiente.

Con el método Vector3.Lerp aplicamos un suavizado en el movimiento de la cámara, facilitando una transición gradual hacia la nueva posición deseada. Y realizamos ajustes en los ejes vertical (y) y de profundidad (z) para garantizar una buena visualización del escenario.

Por último, el comportamiento de la cámara lo definimos dentro del método LateUpdate, que se ejecuta después de que pasen todas las demás actualizaciones de la escena, asegurando así que la cámara siempre reaccione a la posición más reciente de la serpiente, manteniéndola en el centro del encuadre y evitando cualquier retraso en el seguimiento.

# 4. Funcionalidad del juego: aparición de objetos, comer objetos y agrandar cuerpo de serpiente:

- Aparición de Objetos Aleatorios:
En el juego, los objetos aparecen de manera periódica y aleatoria en el escenario, generandose con un rango definido en los ejes X y Z, y una posición fija en el eje Y.
La función FoodGenerator, que es una coroutine, la ejecutamos en bucle infinito para crear objetos cada 10 segundos.
Los objetos pueden ser de tipo "comida" o "tóxicos", señalandose como etiquetas en cada uno de sus objetos fbx, y cada uno tiene un impacto diferente en la partida, la comida hará crecer a la serpiente y lo tóxico la hará más pequeña.

- Recolección de Objetos y Crecimiento:
La serpiente puede comer objetos etiquetados como "Comida", lo que provoca:
Que el objeto recolectado se destruye al detectarse una colisión (método OnCollisionEnter).
Se genera un nuevo segmento de la serpiente mediante la coroutine AddCubeToSnake, que:
Calcula la posición detrás del último segmento basado en la dirección de movimiento.
Instancia una nueva esfera, lo asocia a la lista de segmentos de la serpiente y lo agrega al juego.

- Objetos Tóxicos y Reducción de Tamaño:
Cuando la serpiente colisiona con un objeto etiquetado como "toxic":
El objeto tóxico se destruye.
La serpiente pierde tres esferas del cuerpo mediante la función RemoveCubesFromSnake. Esta función  destruye visualmente del escenario parte del cuerpo de la serpiente.

- Movimiento y Seguimiento del Cuerpo:
El movimiento de la serpiente se define en los ejes X y Z y es controlado por las flechas del teclado:

RightArrow y LeftArrow controlan el desplazamiento lateral.
UpArrow y DownArrow controlan el avance o retroceso. Cada segmento del cuerpo sigue al anterior mediante interpolación suave (Vector3.Lerp), simulando un movimiento cohesivo y orgánico.

- Condición de Derrota
El juego termina con estas condiciones:

La cabeza de la serpiente colisiona con su propio cuerpo, excepto en los segmentos más cercanos a la cabeza.
La función GameOver se llama, lo que típicamente recarga la escena actual, reiniciando el juego.
Chocamos contra un barril rojo.

- Lógica de Generación de Nuevo Contenido:
El juego mantiene un flujo constante de desafíos para el jugador:

Tras recolectar un objeto, se genera otro inmediatamente mediante CreateRandomCube.
Esto asegura que siempre haya contenido en el escenario, fomentando que se dificulte el camino de la serpiente esquivando elementos tóxicos y obteniendo mayor dinámica de juego.

- Desarrollo de la Experiencia de Juego

La posibilidad de perder segmentos o el juego añade un elemento de riesgo y recompensa.
El crecimiento de la serpiente dificulta el control, incrementando la complejidad a medida que avanza el jugador, ya que si se choca con su cuerpo pierde.


![0 2025 01 04 A Las 19.35.44](../0%202025-01-04%20a%20las%2019.35.44.png)



