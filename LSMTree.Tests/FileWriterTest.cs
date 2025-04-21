using System;
using System.Collections.Generic;
using System.IO;

public class FileWriterTest
{
    [Fact]
    public void MapFormatTest()
    {
        // Crear un conjunto ordenado de KeyOffsets para probar.
        SortedSet<KeyOffset> keyOffsets = new SortedSet<KeyOffset>
        {
            new KeyOffset(1, 100),
            new KeyOffset(2, 200),
            new KeyOffset(3, 300),
            new KeyOffset(1, 150), // Esto NO se agregará, ya que SortedSet no permite duplicados basados en el comparador.
        };

        // Crear una instancia de MapWriter.
        MapWriter mapWriter = new MapWriter();

        // Especificar una ruta de archivo base para la prueba.
        string baseFilePath = "map_test";

        try
        {
            // Escribir el archivo de mapa.
            mapWriter.WriteMapFile(keyOffsets, baseFilePath);
            Console.WriteLine($"Archivo de mapa escrito exitosamente en {baseFilePath + ".mp"}");

            // Leer el archivo y verificar el contenido.
            Console.WriteLine("\nContenido del archivo:");
            string filePath = baseFilePath + ".mp";
            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                    }
                }
            }
            else
            {
                Console.WriteLine($"El archivo {filePath} no existe.");
            }

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                Console.WriteLine($"Archivo {filePath} eliminado.");
            }
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Ocurrió un error de E/S: {ex.Message}");
        }
        catch (ArgumentNullException ex)
        {
            Console.WriteLine($"Ocurrió un error de argumento: {ex.Message}");
        }
    }
}
