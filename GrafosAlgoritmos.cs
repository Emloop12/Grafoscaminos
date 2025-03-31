using System;
using System.Collections.Generic;

public class GrafoPonderado
{
    // Diccionario anidado que almacena las listas de adyacencia del grafo ponderado.
    // La clave externa es el nodo origen, la clave interna es el nodo destino, y el valor es el peso de la arista.
    private Dictionary<int, Dictionary<int, int>> listaAdyacencia;

    // Constructor de la clase GrafoPonderado.
    // Inicializa el diccionario de listas de adyacencia.
    public GrafoPonderado()
    {
        listaAdyacencia = new Dictionary<int, Dictionary<int, int>>();
    }

    // Método para agregar un nodo al grafo.
    public void AgregarNodo(int nodo)
    {
        // Si el nodo no existe en el diccionario, lo agrega con un diccionario de vecinos vacío.
        if (!listaAdyacencia.ContainsKey(nodo))
        {
            listaAdyacencia[nodo] = new Dictionary<int, int>();
        }
    }

    // Método para agregar una arista ponderada al grafo.
    public void AgregarArista(int nodoOrigen, int nodoDestino, int peso)
    {
        // Asegura que ambos nodos existan en el grafo.
        AgregarNodo(nodoOrigen);
        AgregarNodo(nodoDestino);

        // Agrega la arista ponderada al diccionario de vecinos del nodo origen.
        listaAdyacencia[nodoOrigen][nodoDestino] = peso;
    }

    // Método que implementa el algoritmo de Dijkstra para encontrar los caminos mínimos desde un nodo origen.
    public Dictionary<int, int> Dijkstra(int nodoOrigen)
    {
        // Diccionario para almacenar las distancias mínimas desde el nodo origen a cada nodo.
        Dictionary<int, int> distancias = new Dictionary<int, int>();
        // Conjunto para almacenar los nodos visitados.
        HashSet<int> visitados = new HashSet<int>();
        // Cola de prioridad para seleccionar el nodo con la distancia mínima en cada iteración.
        PriorityQueue<int, int> colaPrioridad = new PriorityQueue<int, int>();

        // Inicializa las distancias a infinito para todos los nodos, excepto el nodo origen.
        foreach (var nodo in listaAdyacencia.Keys)
        {
            distancias[nodo] = int.MaxValue;
        }

        // La distancia desde el nodo origen a sí mismo es 0.
        distancias[nodoOrigen] = 0;
        // Agrega el nodo origen a la cola de prioridad con distancia 0.
        colaPrioridad.Enqueue(nodoOrigen, 0);

        // Mientras la cola de prioridad no esté vacía, procesa los nodos.
        while (colaPrioridad.Count > 0)
        {
            // Obtiene el nodo con la distancia mínima de la cola de prioridad.
            int nodoActual = colaPrioridad.Dequeue();

            // Si el nodo ya ha sido visitado, lo salta.
            if (visitados.Contains(nodoActual))
            {
                continue;
            }

            // Marca el nodo actual como visitado.
            visitados.Add(nodoActual);

            // Recorre los vecinos del nodo actual.
            foreach (var vecino in listaAdyacencia[nodoActual])
            {
                // Calcula la nueva distancia desde el nodo origen al vecino.
                int distanciaNueva = distancias[nodoActual] + vecino.Value;

                // Si la nueva distancia es menor que la distancia actual al vecino, actualiza la distancia.
                if (distanciaNueva < distancias[vecino.Key])
                {
                    distancias[vecino.Key] = distanciaNueva;
                    // Agrega el vecino a la cola de prioridad con la nueva distancia.
                    colaPrioridad.Enqueue(vecino.Key, distanciaNueva);
                }
            }
        }

        // Retorna el diccionario de distancias mínimas.
        return distancias;
    }

    // Método para mostrar la matriz de adyacencia del grafo.
    public void MostrarMatrizAdyacencia()
    {
        // Imprime los encabezados de las columnas.
        Console.Write("  ");
        foreach (var nodo in listaAdyacencia.Keys)
        {
            Console.Write($"{nodo} ");
        }
        Console.WriteLine();

        // Imprime las filas de la matriz de adyacencia.
        foreach (var nodoOrigen in listaAdyacencia)
        {
            Console.Write($"{nodoOrigen.Key} ");
            foreach (var nodoDestino in listaAdyacencia.Keys)
            {
                // Si hay una arista entre el nodo origen y el nodo destino, imprime el peso de la arista.
                if (nodoOrigen.Value.ContainsKey(nodoDestino))
                {
                    Console.Write($"{nodoOrigen.Value[nodoDestino]} ");
                }
                // Si no hay una arista, imprime 0.
                else
                {
                    Console.Write("0 ");
                }
            }
            Console.WriteLine();
        }
    }
}

public class Programa
{
    public static void Main(string[] args)
    {
        // Crea una instancia de la clase GrafoPonderado.
        GrafoPonderado grafo = new GrafoPonderado();

        // Bucle principal del programa.
        while (true)
        {
            // Imprime el menú de opciones.
            Console.WriteLine("\nMenú:");
            Console.WriteLine("1. Agregar nodo");
            Console.WriteLine("2. Agregar arista");
            Console.WriteLine("3. Mostrar matriz de adyacencia");
            Console.WriteLine("4. Dijkstra");
            Console.WriteLine("5. Salir");

            // Lee la opción seleccionada por el usuario.
            Console.Write("Seleccione una opción: ");
            int opcion = int.Parse(Console.ReadLine());

            // Realiza la operación correspondiente según la opción seleccionada.
            switch (opcion)
            {
                case 1:
                    Console.Write("Ingrese el valor del nodo: ");
                    int nodo = int.Parse(Console.ReadLine());
                    grafo.AgregarNodo(nodo);
                    break;
                case 2:
                    Console.Write("Ingrese el nodo origen: ");
                    int origen = int.Parse(Console.ReadLine());
                    Console.Write("Ingrese el nodo destino: ");
                    int destino = int.Parse(Console.ReadLine());
                    Console.Write("Ingrese el peso de la arista: ");
                    int peso = int.Parse(Console.ReadLine());
                    grafo.AgregarArista(origen, destino, peso);
                    break;
                case 3:
                    grafo.MostrarMatrizAdyacencia();
                    break;
                case 4:
                    Console.Write("Ingrese el nodo de inicio para Dijkstra: ");
                    int inicioDijkstra = int.Parse(Console.ReadLine());
                    Dictionary<int, int> resultadoDijkstra = grafo.Dijkstra(inicioDijkstra);
                    Console.WriteLine("Distancias desde el nodo origen:");
                    foreach (var distancia in resultadoDijkstra)
                    {
                        Console.WriteLine($"Nodo {distancia.Key}: {distancia.Value}");
                    }
                    break;
                case 5:
                    return;
                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }
        }
    }
}
