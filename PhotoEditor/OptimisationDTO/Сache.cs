using System;
using System.Collections.Generic;
using System.IO;

namespace PhotoEditor.OptimisationDTO;

public static class Сache
{
    private static List<Pixel> pixelMap = new List<Pixel>();
    private static byte[] fileBytesMassive = null!;
    private static byte[] originalMassive = null!;
    private static byte[] editedBytesMassive = null!;
    private static byte[] BMPfileHeaders = new byte[54];
    
    public static void FillMap(string FilePath)
    {
        fileBytesMassive = File.ReadAllBytes(FilePath);
        editedBytesMassive = new byte[fileBytesMassive.Length-54];
        originalMassive = new byte[fileBytesMassive.Length - 54];
        Array.Copy(fileBytesMassive, 54, editedBytesMassive, 0, (fileBytesMassive.Length-54));
        Array.Copy(fileBytesMassive, 54, originalMassive, 0, (fileBytesMassive.Length-54));
        Array.Copy(fileBytesMassive, BMPfileHeaders, 54);
        pixelMap.Clear();
        for (int i = 54; i < fileBytesMassive.Length; i+=3)
        {
            pixelMap.Add(new Pixel(fileBytesMassive[i+2], fileBytesMassive[i+1], fileBytesMassive[i]));
        }
    }

    public static List<Pixel> getTempPixelMap()
    {
        List<Pixel> resultTempMap = new List<Pixel>();
        for (int index = 0; index < editedBytesMassive.Length; index += 3)
        {
            resultTempMap.Add(new Pixel(editedBytesMassive[index + 2], 
                editedBytesMassive[index + 1], 
                editedBytesMassive[index]));
        }

        return resultTempMap;
    }
    public static List<Pixel> getPixelMap() { return pixelMap; }
    public static byte[] getBMPHeaders() { return BMPfileHeaders; }
    public static byte[] getOriginalBytesMassive() { return originalMassive; }
    public static byte[] getEditedBytesMassive() { return editedBytesMassive; }
    public static void FillEditedMap(byte[] bitmap) { editedBytesMassive = bitmap; }
}