using Microsoft.ML;
using Tensorflow;

namespace strikeneck.Imaging
{
    //姿勢推定器を表すクラス
    internal class AttitudeEstimator
    {
        private readonly String ONNX_FILE_PATH = "TFModel/posenet.onnx";
        public AttitudeEstimator()
        {
            //ONNXモデルを読み込む
            var mlContext = new MLContext();
            var model = mlContext.Model.LoadTensorFlowModel(ONNX_FILE_PATH);
        }
    }
}
