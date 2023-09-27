
namespace StrikeNeck.Imaging
{
    using Microsoft.ML.OnnxRuntime;
    using Microsoft.ML.OnnxRuntime.Tensors;
    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.Processing;

    internal enum KeyPointName
    {
        Nose,
        LeftEye,
        RightEye,
        LeftEar,
        RightEar,
        LeftShoulder,
        RightShoulder,
        LeftElbow,
        RightElbow,
        LeftWrist,
        RightWrist,
        LeftHip,
        RightHip,
        LeftKnee,
        RightKnee,
        LeftAnkle,
        RightAnkle
    }

    internal struct KeyPoint
    {
        public float X { get; }
        public float Y { get; }

        public KeyPoint(float x, float y)
        {
            X = x;
            Y = y;
        }
    }
    internal class AttitudeEsimator
    {
        static internal Dictionary<KeyPointName, KeyPoint> estimate(FileInfo fileInfo)
        {


            //PoseNetの読み込み
            string modelFilePath = "../../../../../../Imaging/mlModel/posenet.onnx";

            //画像の読み込み
            using var image = Image.Load<Rgb24>(fileInfo.FullName);


            //画像のリサイズ
            using Stream imageStream = new MemoryStream();
            image.Mutate(x =>
            {
                x.Resize(new ResizeOptions
                {
                    Size = new Size(257, 257),
                    Mode = ResizeMode.Crop
                });
            });


            //画像の前処理
            Tensor<float> input = new DenseTensor<float>(new[] { 1, 257, 257, 3 });
            var mean = new[] { 0.485f, 0.456f, 0.406f };
            var stddev = new[] { 0.229f, 0.224f, 0.225f };


            image.ProcessPixelRows(accessor =>
            {
                for (int y = 0; y < accessor.Height; y++)
                {
                    Span<Rgb24> pixelSpan = accessor.GetRowSpan(y);
                    for (int x = 0; x < accessor.Width; x++)
                    {
                        input[0, y, x, 0] = ((pixelSpan[x].R / 255f) - mean[0]) / stddev[0];
                        input[0, y, x, 1] = ((pixelSpan[x].G / 255f) - mean[1]) / stddev[1];
                        input[0, y, x, 2] = ((pixelSpan[x].B / 255f) - mean[2]) / stddev[2];
                    }
                }
            });

            var inputs = new List<NamedOnnxValue>
        {
            NamedOnnxValue.CreateFromTensor("sub_2", input)
        };
            using var session = new InferenceSession(modelFilePath);
            using IDisposableReadOnlyCollection<DisposableNamedOnnxValue> results = session.Run(inputs);

            var resultsList = new List<Tensor<float>>();

            foreach (var r in results) resultsList.Add(r.AsTensor<float>());

            var heatmaps = resultsList[0];
            var offsets = resultsList[1];
            var displacementFwd = resultsList[2];
            var displacementBwd = resultsList[3];

            //heatmapの各要素にシグモイド関数を適用する

            var heatmapsMax = new float[17, 3];

            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    for (int keyID = 0; keyID < 17; keyID++)
                    {
                        heatmaps[0, y, x, keyID] = Sigmoid(heatmaps[0, y, x, keyID]);
                        if (heatmaps[0, y, x, keyID] > heatmapsMax[keyID, 2])
                        {
                            heatmapsMax[keyID, 0] = y;
                            heatmapsMax[keyID, 1] = x;
                            heatmapsMax[keyID, 2] = heatmaps[0, y, x, keyID];
                        }

                    }
                }
            }

            var offsetVectors = new List<float[]>();

            for (int k = 0; k < 17; k++)
            {
                float[] offsetVector = { offsets[0, (int)heatmapsMax[k, 0], (int)heatmapsMax[k, 1], k], offsets[0, (int)heatmapsMax[k, 0], (int)heatmapsMax[k, 1], k + 17] };
                offsetVectors.Add(offsetVector);
            }

            var keyPoints = new List<float[]>();

            for (int k = 0; k < 17; k++)
            {
                float[] keyPoint = { heatmapsMax[k, 1] * 32 + offsetVectors[k][1], heatmapsMax[k, 0] * 32 + offsetVectors[k][0] };
                keyPoints.Add(keyPoint);
            }

            var keyPointsDict = new Dictionary<KeyPointName, KeyPoint>();
            foreach(var value in Enum.GetValues<KeyPointName>())
            {
                keyPointsDict.Add(value, new KeyPoint(keyPoints[(int)value][0], keyPoints[(int)value][1]));
            }

            return keyPointsDict;
        }

        //シグモイド関数
        static float Sigmoid(float x)
        {
            return 1.0f / (1.0f + MathF.Exp(-x));
        }
    }
}