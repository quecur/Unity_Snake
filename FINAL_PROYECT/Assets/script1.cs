using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class script1 : MonoBehaviour
{
    public Transform cubeTransform;
    public GameObject cubePrefab; // Prefab del cubo a instanciar
    public GameObject cubePrefab2; // Prefab del cubo a instanciar
    public GameObject[] foodPrefabs;
    private Vector3 position;
    public Transform parentObject; // El objeto contenedor donde los cubos se van a crear
    public float rangeX = 70f; // Rango para la creaci?n aleatoria en el eje X
    public float rangeZ = 35f; // Rango para la creaci?n aleatoria en el eje Z
    public float fixedY = 2f; // Altura fija para los cubos
    private Vector3 lastMovementDirection; // Direcci?n del ?ltimo movimiento del cubo
    private List<GameObject> snakeCubes = new List<GameObject>(); // Lista para mantener todos los cubos de la serpiente


    private float moveDirectionX = 1f; // Direcci?n de movimiento en el eje X, 1 para avanzar, -1 para retroceder
    private float moveDirectionZ = 0f; // Direcci?n de movimiento en el eje Z, 1 para avanzar, -1 para retroceder

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hola Mundo");
        cubeTransform = GetComponent<Transform>();
        position = cubeTransform.position;
        lastMovementDirection = Vector3.zero; // Inicializa la direcci?n de movimiento
        snakeCubes.Add(gameObject); // A?adimos el cubo actual a la lista de cubos de la serpiente
        StartCoroutine(FoodGenerator());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Guarda la direcci?n del movimiento
        Vector3 previousPosition = cubeTransform.position;

        // Movimiento en el eje X
        position = cubeTransform.position;
        position.x += moveDirectionX * 8f * Time.fixedDeltaTime; // Movimiento constante en el eje X
        // Movimiento en el eje Z
        position.z += moveDirectionZ * 8f * Time.fixedDeltaTime; // Movimiento en el eje Z

        cubeTransform.position = position;

        // Calcula la direcci?n del movimiento en base a la diferencia de posiciones
        lastMovementDirection = cubeTransform.position - previousPosition;

        // Mover los cubos de la serpiente
        // Cambiar direcci?n en el eje X con las flechas izquierda y derecha
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveDirectionX = 1f; // Mover hacia la derecha
            
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveDirectionX = -1f; // Mover hacia la izquierda
          
        }

        // Cambiar direcci?n en el eje Z con las flechas arriba y abajo
        if (Input.GetKey(KeyCode.UpArrow))
        {
            
            moveDirectionZ = 1f; // Mover hacia adelante
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            moveDirectionZ = -1f; // Mover hacia atr?s
        }else if (Input.GetKey(KeyCode.Escape))
        {
            // Aquí defines la acción que quieres realizar al presionar Escape
            Debug.Log("Se presionó Escape");
            // Por ejemplo, salir del juego
            GameOver();
        }
        MoveSnake();
    }

    private void MoveSnake()
    {
        // Si la lista tiene m?s de un cubo
        if (snakeCubes.Count > 1)
        {
            // Mueve los cubos en la cola hacia la posici?n del cubo anterior
            for (int i = 1; i < snakeCubes.Count; i++)
            {
                // Mueve cada cubo a la posici?n del cubo anterior, pero manteniendo un desfase
                Vector3 targetPosition = snakeCubes[i - 1].transform.position;
                snakeCubes[i].transform.position = Vector3.Lerp(snakeCubes[i].transform.position, targetPosition, 0.05f);
            }
    }
}

    // Detectar colisiones
    void OnCollisionEnter(Collision collision)
    {
        // Comprueba si colisiona con un objeto que tenga el tag "Comida"
        if (collision.gameObject.CompareTag("Comida"))
        {
            // Destruye el cubo con el que colisionaste
            Destroy(collision.gameObject);
            Debug.Log("Cubo destruido!");

            // Llama a la coroutine para agregar un nuevo cubo a la serpiente
            StartCoroutine(AddCubeToSnake());
        }
        // Comprueba si colisiona con un objeto que tenga el tag "toxic"
        else if (collision.gameObject.CompareTag("toxic"))
        {
            // Destruye el objeto t?xico
            Destroy(collision.gameObject);
            Debug.Log("Objeto t?xico comido!");

            // Resta 3 cubos de la serpiente
            RemoveCubesFromSnake(3);

            // Genera un nuevo objeto en el escenario
            StartCoroutine(CreateRandomCube());
        }else if (collision.gameObject.CompareTag("toxic2"))
        {
            GameOver();
        }
        else if (snakeCubes.Contains(collision.gameObject))
        {
            int index = snakeCubes.IndexOf(collision.gameObject); // Obtiene el índice del objeto en la lista
            if (index != 1 && index != 2)
            {
                GameOver();
            }
        }
    }


    // Coroutine para agregar un nuevo cubo a la serpiente
    private IEnumerator AddCubeToSnake()
    {
        // Espera un poco para simular la creaci?n del cubo
        yield return new WaitForSeconds(0.1f);

        Vector3 newCubePosition;

        GameObject lastCube = snakeCubes[snakeCubes.Count - 1];
        
        // Calcula la posici?n detr?s del ?ltimo cubo usando la direcci?n de movimiento
        newCubePosition = lastCube.transform.position - lastMovementDirection.normalized * 2f;
       
        // Crea el nuevo cubo
        GameObject newCube = Instantiate(cubePrefab, newCubePosition, Quaternion.identity);
        Renderer foodRenderer = newCube.GetComponent<Renderer>();
        foodRenderer.material.color = new Color(0.5f, 1f, 0.5f); // Verde claro
        // Asigna el nuevo cubo al contenedor 'parentObject'
        newCube.transform.SetParent(parentObject);

        // A?ade el nuevo cubo a la lista de la serpiente
        snakeCubes.Add(newCube);

        Debug.Log("Nuevo cubo agregado a la serpiente!");

        // Crea la "comida" aleatoria despu?s de 2 segundos
        StartCoroutine(CreateRandomCube());
    }

    private IEnumerator FoodGenerator(){
        while (true) // Bucle infinito para repetir continuamente
        {
            StartCoroutine(CreateRandomCube());
            yield return new WaitForSeconds(10f); // Espera 10 segundos
        }
    }
    // Eliminar cubos de la serpiente
    private void RemoveCubesFromSnake(int count)
    {
        // Aseg?rate de no intentar eliminar m?s cubos de los que hay en la serpiente
        count = Mathf.Min(count, snakeCubes.Count - 1); // No eliminamos el primer cubo (la cabeza)

        for (int i = 0; i < 3; i++)
        {
            if(snakeCubes.Count>0){
            // Obt?n el ?ltimo cubo de la lista
            GameObject lastCube = snakeCubes[snakeCubes.Count - 1];

            // Elim?nalo de la lista
            snakeCubes.RemoveAt(snakeCubes.Count - 1);

            // Destruye el objeto f?sicamente en la escena
            Destroy(lastCube);
            }
            if(snakeCubes.Count==0){
                GameOver();
            }
        }

        Debug.Log($"{count} cubos eliminados de la serpiente!");
    }
    Vector3 GenerateNonOverlappingPosition(float rangeX, float rangeZ, float fixedY)
    {
        Vector3 randomPosition;
        bool isOverlapping;

        do
        {
            // Generar una posición aleatoria
            randomPosition = new Vector3(
                Random.Range(-rangeX, rangeX), 
                fixedY, 
                Random.Range(-rangeZ, rangeZ)
            );

            // Comprobar si coincide con algún objeto con el tag "Decoracion"
            isOverlapping = false;
            GameObject[] decorations = GameObject.FindGameObjectsWithTag("Decoracion");

            foreach (GameObject decoration in decorations)
            {
                if (Vector3.Distance(randomPosition, decoration.transform.position) < 1f) // Tolerancia de proximidad
                {
                    isOverlapping = true;
                    break;
                }
            }
        }
        while (isOverlapping);

        return randomPosition;
    }

    // Coroutine para esperar un tiempo y luego crear un cubo de comida aleatorio
    private IEnumerator CreateRandomCube()
    {
        // Espera 2 segundos antes de crear la comida
        yield return new WaitForSeconds(2f);

        // Genera una posici?n aleatoria dentro del rango especificado en los ejes X y Z
        // Generar una posición válida que no coincida con decoraciones
        Vector3 randomPosition = GenerateNonOverlappingPosition(rangeX, rangeZ, fixedY);

        //prefab aleatorio del array:
        GameObject randomFoodPrefab = foodPrefabs[Random.Range(0, foodPrefabs.Length)];


        // Crea un nuevo cubo de comida en esa posici?n aleatoria
        GameObject food = Instantiate(randomFoodPrefab, randomPosition, randomFoodPrefab.transform.rotation);
        //Renderer foodRenderer = food.GetComponent<Renderer>();
        //foodRenderer.material.color = new Color(0.5f, 1f, 0.5f); // Verde claro
        // Asigna el cubo de comida al contenedor 'parentObject'
        food.transform.SetParent(parentObject);

        Debug.Log("Nuevo cubo de comida creado!");
    }

    public void GameOver(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
    }
}
