using UnityEngine;
using System.Collections;

namespace ColorSpace {

    public static class Converter {
        public static readonly Vector3 D50_XYZ = new Vector3(0.9642f, 1.0f, 0.8249f);
        public static readonly Vector3 D65_XYZ = new Vector3 (0.95046f, 1.0f, 1.08906f);

        public static readonly Matrix4x4 LinearRGB2XYZMat;
        public static readonly Matrix4x4 XYZ2LinearRGBMat;

        static Converter() {
            LinearRGB2XYZMat = new Matrix4x4 ();
            LinearRGB2XYZMat [0] = 0.412391f;
            LinearRGB2XYZMat [4] = 0.357584f;
            LinearRGB2XYZMat [8] = 0.180481f;

            LinearRGB2XYZMat [1] = 0.212639f;
            LinearRGB2XYZMat [5] = 0.715169f;
            LinearRGB2XYZMat [9] = 0.072192f;

            LinearRGB2XYZMat [2] = 0.019331f;
            LinearRGB2XYZMat [6] = 0.119195f;
            LinearRGB2XYZMat [10] = 0.950532f;

            XYZ2LinearRGBMat = new Matrix4x4 ();
            XYZ2LinearRGBMat [0] = 3.240970f;
            XYZ2LinearRGBMat [4] = -1.537383f;
            XYZ2LinearRGBMat [8] = -0.498611f;

            XYZ2LinearRGBMat [1] = -0.969244f;
            XYZ2LinearRGBMat [5] = 1.875968f;
            XYZ2LinearRGBMat [9] = 0.041555f;

            XYZ2LinearRGBMat [2] = 0.055630f;
            XYZ2LinearRGBMat [6] = -0.203977f;
            XYZ2LinearRGBMat [10] = 1.056972f;
        }

        public static float SRGB2LinearRGB(float r) {
            return r <= 0.040450f ? r / 12.92f : Mathf.Pow ((r + 0.055f) / 1.055f, 2.4f);
        }
        public static float LinearRGB2SRGB(float r) {
            return r <= 0.0031308f ? 12.92f * r : 1.055f * Mathf.Pow (r, 1 / 2.4f) - 0.055f;
        }
        public static readonly float THIRD_POWER_OF_29OVER3 = Mathf.Pow(29f/3, 3f);
        public static float LabCurve(float x) {
            return x > 0.00885645167f ? Mathf.Pow (x, 1f / 3) : (THIRD_POWER_OF_29OVER3 * x + 16f) / 116f;
        }

        public static Vector3 SRGB2LinearRGB(Vector3 srgb) {
            return new Vector3 (SRGB2LinearRGB (srgb.x), SRGB2LinearRGB (srgb.y), SRGB2LinearRGB (srgb.z));
        }
        public static Vector3 LinearRGB2SRGB(Vector3 rgb) {
            return new Vector3 (LinearRGB2SRGB (rgb.x), LinearRGB2SRGB (rgb.y), LinearRGB2SRGB (rgb.z));
        }
        public static Vector3 LinearRGB2XYZ(Vector3 rgb) {
            return LinearRGB2XYZMat.MultiplyPoint3x4 (rgb);
        }
        public static Vector3 XYZ2LinearRGB(Vector3 xyz) {
            return XYZ2LinearRGBMat.MultiplyPoint3x4 (xyz);
        }
        public static Vector3 XYZ2Lab(Vector3 xyz) {
            
        }
    }
}
