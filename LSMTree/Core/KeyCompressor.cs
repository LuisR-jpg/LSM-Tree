// DataProcessor.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class KeyCompressor
{
    private const int COMPRESSED_SIZE = 40960; // 4 KB

    /// <summary>
    /// Comprime la lista de objectos DataUnit
    /// Escribe la lista de objetos comprimidas a un SSTable
    /// Escribe el mapa comprimido
    /// Devuelve una lista de referencias al mapa comprimido
    /// <param name="data"> la listo de objetos DataUnit a procesar.</param>
    /// <param name="baseFilePath"> La ruta de archivos base.</param>
    /// <param name="mapWriter"> Map Writer.</param>
    /// <param name="compressedKeys"> La lista de objetos comprimida.</param>
    /// <param name="SSTablePath"> El path al SSTable escrito.</param>
    /// <param name="CheckpointPath"> El path al Checkpoint escrito.</param>
    public void CompressDataAndSave(
            List<DataUnit> data, string baseFilePath, MapWriter mapWriter,
            out SortedSet<KeyOffset> compressedKeys, out string sstPath,
            out string checkpointPath) {

        compressedKeys = new SortedSet<KeyOffset>();
        string filePathWithTime = MakePathWithTimestamp(baseFilePath);
        sstPath = GetLogFileName(filePathWithTime);
        try
        {
            // Crear el directorio si no existe.
            string directory = Path.GetDirectoryName(filePathWithTime);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Usar un StreamWriter para escribir en el archivo.
            // Esto asegura que el archivo se cierre correctamente.
            using (StreamWriter writer = new StreamWriter(sstPath))
            {
                long segmentSize = 0;
                long bytesWritten = 0;
                // Escribir cada KeyOffset en una nueva línea, con la clave y el offset separados por una coma.
                foreach (DataUnit dataUnit in data)
                {
                    string dataEncoded = $"{dataUnit.Key},{dataUnit.Data}" +
                        Environment.NewLine;
                    writer.Write(dataEncoded);
                    if (segmentSize == 0) {
                        KeyOffset compressedKey = new KeyOffset(
                                dataUnit.Key,
                                bytesWritten);
                        compressedKeys.Add(compressedKey);
                    }
                    segmentSize += GetByteCount(dataEncoded);
                    if (segmentSize > COMPRESSED_SIZE) {
                        segmentSize = 0;
                    }
                    bytesWritten += GetByteCount(dataEncoded);
                }
            }
        }
        catch (IOException ex)
        {
            // Envolver la excepción original con un mensaje más descriptivo.
            throw new IOException($"Error al escribir en el archivo de mapa: {sstPath}", ex);
        }

        // Write Checkpoint
        checkpointPath = GetMapFileName(filePathWithTime);
        mapWriter.WriteMapFile(compressedKeys, checkpointPath);
    }

    private string MakePathWithTimestamp(string baseFilePath) {
        DateTimeOffset nowOffset = DateTimeOffset.Now; // Gets the current local time with its offset
        long unixTimeSeconds = nowOffset.ToUnixTimeSeconds();
        return $"{baseFilePath}{unixTimeSeconds}";
    }

    private string GetLogFileName(string filePathWithTime) {
        return $"{filePathWithTime}.log";
    }

    private string GetMapFileName(string filePathWithTime) {
        return $"{filePathWithTime}.map";
    }
    private long GetByteCount(string inputString) {
        return Encoding.UTF8.GetByteCount(inputString);
    }
}
