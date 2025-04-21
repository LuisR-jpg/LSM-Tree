using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// TODO: Agregar la última key para tener el offset final
// TODO: Escritura es tan rápida que intenta repetir timestamp

public static class MapWriter
{
    /// <summary>
    /// Recibe un conjunto ordenado de KeyOffsets y una ruta de archivo base,
    /// y escribe un archivo con (key, offset) por línea.
    /// </summary>
    /// <param name="keyOffsets">El conjunto ordenado de KeyOffsets.</param>
    /// <param name="filePath">La ruta de archivo base.</param>
    /// <exception cref="ArgumentNullException">Se lanza si keyOffsets o baseFilePath es null.</exception>
    /// <exception cref="IOException">Se lanza si hay un error al escribir el archivo.</exception>
    public static void WriteMapFile(SortedSet<KeyOffset> keyOffsets, string filePath)
    {
        // Validación de argumentos
        if (keyOffsets == null)
        {
            throw new ArgumentNullException(nameof(keyOffsets), "El conjunto de KeyOffsets no puede ser null.");
        }
        if (filePath == null)
        {
            throw new ArgumentNullException(nameof(filePath), "La ruta de archivo base no puede ser null.");
        }

        try
        {
            // Crear el directorio si no existe.
            string? directory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Usar un StreamWriter para escribir en el archivo.  Esto asegura que el archivo se cierre
            // correctamente, incluso si ocurren excepciones.
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                // Escribir cada KeyOffset en una nueva línea, con la clave y el offset separados por una coma.
                foreach (KeyOffset keyOffset in keyOffsets)
                {
                    writer.WriteLine($"{keyOffset.Id},{keyOffset.Offset}");
                }
            }
        }
        catch (IOException ex)
        {
            // Envolver la excepción original con un mensaje más descriptivo.
            throw new IOException($"Error al escribir en el archivo de mapa: {filePath}", ex);
        }
    }
}
