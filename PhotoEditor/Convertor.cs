namespace PhotoEditor;

public class Convertor
{
    public static byte[] ConvertRGBToYCbCr(byte[] RGBbitSequence)
    {
        byte[] YCbCrByteSequence = new byte[RGBbitSequence.Length];
        for (int value = 0; value < RGBbitSequence.Length; value += 3)
        {
            byte BlueColor = RGBbitSequence[value + 0];
            byte GreenColor = RGBbitSequence[value + 1];
            byte RedColor = RGBbitSequence[value + 2];
           
            YCbCrByteSequence[value] = (byte)((0.257 * RedColor) + (0.504 * GreenColor) + (0.098 * BlueColor) + 16); //Y parametr
            YCbCrByteSequence[value + 1] = (byte)(-(0.148 * RedColor) - (0.291 * GreenColor) + (0.439 * BlueColor) + 128); //Cb parametr
            YCbCrByteSequence[value + 2] = (byte)((0.439 * RedColor) - (0.368 * GreenColor) - (0.071 * BlueColor) + 128); //Cr parametr
        }
        return YCbCrByteSequence;
    }
    public static byte[] ConvertRGBToCMyk(byte[] RGBbitSequence)
    {
        byte[] YCbCrByteSequence = new byte[RGBbitSequence.Length];
        for (int value = 0; value < RGBbitSequence.Length; value += 3)
        {
            byte BlueColor = RGBbitSequence[value + 0];
            byte GreenColor = RGBbitSequence[value + 1];
            byte RedColor = RGBbitSequence[value + 2];
           
            YCbCrByteSequence[value] = (byte)((0.257 * RedColor) + (0.504 * GreenColor) + (0.098 * BlueColor) + 16); //Y parametr
            YCbCrByteSequence[value + 1] = (byte)(-(0.148 * RedColor) - (0.291 * GreenColor) + (0.439 * BlueColor) + 128); //Cb parametr
            YCbCrByteSequence[value + 2] = (byte)((0.439 * RedColor) - (0.368 * GreenColor) - (0.071 * BlueColor) + 128); //Cr parametr
        }
        return YCbCrByteSequence;
    }
}