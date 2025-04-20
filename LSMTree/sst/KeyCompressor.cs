/* DataProcessor.cs
using System;
using System.Collections.Generic;
using System.IO;

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
    /// <param name="processedData"> La lista de objetos comprimida.</param>
    /// <param name="SSTablePath"> El path al SSTable escrito.</param>
    /// <param name="CheckpointPath"> El path al Checkpoint escrito.</param>
    public CompressDataAndSave(
            List<DataUnit> data, string baseFilePath,  

}
*/
